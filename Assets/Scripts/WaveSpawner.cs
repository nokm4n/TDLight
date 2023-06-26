using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UI;
//using GameAnalyticsSDK;
public class WaveSpawner : MonoBehaviour {

	public static int EnemiesAlive = 0;
	private int enemiesinWave = 0;
	public static WaveSpawner instance;

	public Transform spawnPoint;

	public int reward = 40;
	private int waveIndex = 0;
	private int waveIndexCur = 0;

	private ChangeSetting settingsController;
	[SerializeField, NotNull] private WaveList waveList;
	[SerializeField, NotNull] private Transform wayTarget;
	[SerializeField, NotNull] private Image waveBar;

	[SerializeField, NotNull] private Tutorial tutorial;
	[SerializeField, NotNull] private Tutorial tutorial2;
	[SerializeField, NotNull] private Tutorial tutorial3;

	public bool startNextWave = true;
	public bool menuOpened = false;

	private float spawnRange = 1.3f;
	private bool isLoaded = false;
	private float enemyBaseHP = 1;

	private int loseInARow = 0;
	private int winInARow = 0;

	private float enemyHpModif = 1;
	private async void Awake()
	{
		EnemiesAlive = 0;


		if (instance != null)
		{
			Debug.LogError("More than one WaveSpawner in scene!");
			return;
		}
		instance = this;

		StartCoroutine(StartGame());
		
		/*await Task.Delay(200);
		//await Task.Yield();

		isLoaded = true;
		UIController.instance.SetWaveText("Wave: " + waveIndex);
		settingsController = GetComponent<ChangeSetting>();
		ChangeSetting();


		waveIndexCur = waveIndex % (waveList.waves.Count - 1);*/
		//waveText.text = new string("Wave: " + waveIndex);
	}
	IEnumerator StartGame()
    {
		yield return new WaitForSeconds(0.3f);

		isLoaded = true;

		UIController.instance.SetWaveText("Wave: " + waveIndex);
		settingsController = GetComponent<ChangeSetting>();
		ChangeSetting();

		waveIndexCur = waveIndex % (waveList.waves.Count - 1);
	}


	async void Update()
	{
		TutorialKostil();

		if (!isLoaded || LevelManager.instance.turretsOnScene.Count <= 0) return;

		if (EnemiesAlive > 0 && !menuOpened)
		{
			waveBar.fillAmount = 1 - (float)EnemiesAlive / enemiesinWave;
			return;
		}

		if (!startNextWave && !menuOpened) // win
		{
			//GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level_" + waveIndex + "_started");

			menuOpened = true;
			Grabber.instance.isActive = false;

			CoinCountAnimator.instance.AnimateCoinCollect(transform.position);
			for (int i = 0; i < 10; i++)
			{
				await Task.Delay(50);
				CoinCountAnimator.instance.AnimateDiamondCollect(transform.position);
				PlayerStats.instance.AddDiamonds(10);
			}
			//Debug.Log("cur wave " + waveIndexCur + " index " + waveIndex);
			AudioController.instance.PlaySound("win");

			UIController.instance.OpenMenu(true);

			winInARow++;
			if (loseInARow > 0)
				loseInARow--; ;
			return;
		}

		if (!startNextWave) return;

		if (waveIndex >= waveList.waves.Count - 1)
		{
			waveIndexCur = waveIndex % (waveList.waves.Count - 1);
		}

		ChangeSetting();
		LevelManager.instance.Boost(UpgradeStats.instance.damageModif, UpgradeStats.instance.speedModif);

		StartCoroutine(SpawnWave());
		//countdown = timeBetweenWaves;			
		return;
	}

	IEnumerator SpawnWave()
	{
		startNextWave = false;

		if (winInARow >= 5)
		{
			enemyHpModif *= 2;
		}
		if (loseInARow >= 2)
		{
			enemyHpModif /= loseInARow;
		}
		if(enemyHpModif <=0) enemyHpModif = .2f;

		enemyBaseHP = (LevelManager.instance.GetAllDamage() / 4 * 4 - 1) * enemyHpModif;
		if (enemyBaseHP <= 0) enemyBaseHP = 1;

		waveIndexCur++;
		waveIndex++;

		//GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level_" + waveIndex + "_started");
		


		enemiesinWave = 0;
		EnemiesAlive = 0;
		for (int i = 0; i < waveList.waves[waveIndexCur ].waveStruct.Count; i++)
		{
			Waves wave = waveList.waves[waveIndexCur ].waveStruct[i]; //текущая волна в одной точке спавна
			//Waves wave2 = waveList.waves[WaveSpawn[waveIndex]].waveStruct[1]; //текущая волна в одной точке спавна

			EnemiesAlive += wave.count; // количество живых врагов 
			enemiesinWave += wave.count;
		}
		for (int i = 0; i < waveList.waves[waveIndexCur ].waveStruct.Count; i++)
		{ 
			Waves wave = waveList.waves[waveIndexCur].waveStruct[i];
			for (int j = 0; j < wave.count; j++)
			{
				if (menuOpened)
				{
					StopCoroutine(SpawnWave());
					yield break;
				}

				if (wave.enemy.Length > 1)
				{
					SpawnEnemy(wave.enemy[Random.Range(0, wave.enemy.Length)]);
				}
				else
				{
					yield return new WaitForSeconds(3f);
					SpawnEnemy(wave.enemy[0]);
				}
				yield return new WaitForSeconds(0.5f);
			}
		}
		
	}

	void SpawnEnemy (Enemy enemy)
	{
		if (enemy.isBoss)
		{
			var p = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
			p.GetComponent<EnemyMovement>().SetTarget(wayTarget);
			p.GetComponent<Enemy>().SetHP(enemyBaseHP);
		}
		else
		{
			var p = Instantiate(enemy, spawnPoint.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, 0), spawnPoint.rotation);
			p.GetComponent<EnemyMovement>().SetTarget(wayTarget);
			p.GetComponent<Enemy>().SetHP(enemyBaseHP);
		}
	}

	private void TutorialKostil()
    {
		if (LevelManager.instance.priceForTurret == 5)
		{
			tutorial.MoveUpDown();
			return;
		}
		else
		{
			tutorial.StopMove();
		}

		if (LevelManager.instance.priceForTurret == 6 && PlayerStats.instance.coins >= 6)
		{
			tutorial2.MoveUpDown();
			//return;
		}
		else
		{
			tutorial2.StopMove();
		}

		if (LevelManager.instance.priceForTurret == 7 && LevelManager.instance.turretsOnScene.Count == 2)
		{
			tutorial3.MoveTowers(LevelManager.instance.turretsOnScene[0].transform, LevelManager.instance.turretsOnScene[1].transform);
		}
		else
		{
			tutorial3.StopMove();
		}
	}

	public void SetWaveDate(WaveList waveList)
    {
		this.waveList = waveList;
    }

	public void StartNextWave()
    {
		if (EnemiesAlive >0)
        {
			LevelManager.instance.KillEveryEnemy();
        }
		//enemiesinWave = 0;
		//EnemiesAlive = 0;
		//waveBar.fillAmount = 0;

		LevelManager.instance.SwitchButton(true);
		menuOpened = false;

		Grabber.instance.isActive = true;
		startNextWave = true;
		UIController.instance.SetWaveText("Wave: " + waveIndex);
	}

	public void LoseScreen()
    {
		//GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level_" + waveIndex + "_started");

		winInARow = 0;
		loseInARow++;

		enemiesinWave = 0;
		EnemiesAlive = 0;

		menuOpened = true;
		waveIndexCur--;
		waveIndex--;
		UIController.instance.SetWaveText("Wave: " + waveIndex);
		Grabber.instance.isActive = false;
		UIController.instance.OpenMenu(false);
		startNextWave = false;
	}

	public void ChangeSetting()
    {
		int index = waveIndexCur / 5;
		settingsController.ChangeSettings(index);
		LevelManager.instance.ChangeNodeTexture(settingsController.GetTexture());
    }

	public void SetWaveIndex()
    {
		if (waveIndex >= waveList.waves.Count - 1)
		{
			waveIndexCur = waveIndex % (waveList.waves.Count - 1);
		}
		else
        {
			waveIndexCur = waveIndex;
		}
	}

	public int GetWaveIndex()
    {
		return waveIndex;
    }

	public void SetWaveIndex(int waveIndex)
    {
		this.waveIndex = waveIndex;
    }
}
