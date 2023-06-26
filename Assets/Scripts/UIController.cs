using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[Header("")]
	[SerializeField, NotNull] private GameObject loseImage;
	[SerializeField, NotNull] private GameObject winImage;
	[SerializeField, NotNull] private TextMeshProUGUI rewardCoins;

	/*[Header("")]
	[SerializeField, NotNull] private TextMeshProUGUI coins;
	[SerializeField, NotNull] private TextMeshProUGUI diamonds;*/

	[Header("")]
	[SerializeField, NotNull] private TextMeshProUGUI waveText;
	[SerializeField, NotNull] private TextMeshProUGUI levelText;

	[Header("Canvas")]
	[SerializeField, NotNull] private CanvasGroup menuCanvas;
	[SerializeField, NotNull] private CanvasGroup gamePlayCanvas;

	[Header("Windows")]
	[SerializeField, NotNull] private Canvas menuWindow;
	[SerializeField, NotNull] private Canvas leaderWindow;
	[SerializeField, NotNull] private Canvas questWindow;
	[SerializeField, NotNull] private Canvas prepWindow;
	[SerializeField, NotNull] private Canvas upgradeWindow;
	//[SerializeField, NotNull] private Canvas abilityWindow;


	[SerializeField, NotNull] private UILevel uiLevel;

	[SerializeField, NotNull] private GameObject x2Button;

	[Header("Stuff")]
	[SerializeField, NotNull] private TextMeshProUGUI rewardText;
	[SerializeField, NotNull] private MovingArrow movingArrow;
	[SerializeField, NotNull] private GameObject questNotification;
	private bool showed = false;

	public static UIController instance;

    private void Awake()
    {
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;

		gamePlayCanvas.blocksRaycasts = true;
		menuCanvas.blocksRaycasts = false;
	}

	public void SetWaveText(string str)
    {
		waveText.text = str;
	}

	public async void OpenMenu(bool win)
    {
		await Task.Delay(3000);

		uiLevel.SetItems();
		

		menuWindow.transform.DOScale(1, .5f);
		leaderWindow.transform.DOScale(0, .5f);
		questWindow.transform.DOScale(0, .5f);
		prepWindow.transform.DOScale(0, .5f);
		upgradeWindow.transform.DOScale(0, .5f);

		//gamePlayCanvas.gameObject.SetActive(false);
		//menuCanvas.gameObject.SetActive(true);
		gamePlayCanvas.blocksRaycasts = false;
		menuCanvas.blocksRaycasts = true;
		movingArrow.isActive = true;
		movingArrow._isPlaying = false;
		if (win)
		{
			Debug.Log("Win");
			winImage.SetActive(true);
			loseImage.SetActive(false);
		}
		else
        {
			Debug.Log("Lose");
            winImage.SetActive(false);
			loseImage.SetActive(true);
		}
		rewardCoins.text = ("+" + WaveSpawner.instance.reward.ToString());
		levelText.text = ("Level: " + (WaveSpawner.instance.GetWaveIndex()).ToString());
		menuCanvas.DOFade(1, .5f);
		gamePlayCanvas.DOFade(0, .5f);

		LevelManager.instance.Save();
	}

	public void OpenGamePlay()
	{
		showed = false;

		movingArrow.isActive = false;
		//movingArrow._isPlaying = true;
		//gamePlayCanvas.gameObject.SetActive(true);
		//menuCanvas.gameObject.SetActive(false);

		gamePlayCanvas.blocksRaycasts = true;
		menuCanvas.blocksRaycasts = false;

		menuCanvas.DOFade(0, .5f);
		gamePlayCanvas.DOFade(1, .5f);
		WaveSpawner.instance.StartNextWave();
		uiLevel.SetBack();
	}

	public void OpenBonus()
    {
		// show banner;
    }

	public void OpenLeaderBoard()
    {
		menuWindow.transform.DOScale(0, .5f);
		leaderWindow.transform.DOScale(1, 0.5f);
		questWindow.transform.DOScale(0, .5f);
		prepWindow.transform.DOScale(0, .5f);
		upgradeWindow.transform.DOScale(0, .5f);
	//	abilityWindow.transform.DOScale(0, .5f);
	}

	public void OpenAbilityWindow()
	{
	//	abilityWindow.transform.DOScale(1, .5f);
		menuWindow.transform.DOScale(0, .5f);
		leaderWindow.transform.DOScale(0, 0.5f);
		questWindow.transform.DOScale(0, .5f);
		prepWindow.transform.DOScale(0, .5f);
		upgradeWindow.transform.DOScale(0, .5f);
	}

	public void OpenQuestWindow()
    {
		

		questWindow.GetComponent<QuestUI>().SetQuests();
		menuWindow.transform.DOScale(0, .5f);
		leaderWindow.transform.DOScale(0, .5f);
		questWindow.transform.DOScale(1, .5f);
		prepWindow.transform.DOScale(0, .5f);
		upgradeWindow.transform.DOScale(0, .5f);
		//abilityWindow.transform.DOScale(0, .5f);
		// start progress bar anim

		LevelManager.instance.Save();
	}

	public void OpenPrepWindow()
    {
		PlayerStats.instance.AddCoins(WaveSpawner.instance.reward * movingArrow.GetModif());
		questNotification.gameObject.SetActive(QuestList.instance.CheckQuests());

		menuWindow.transform.DOScale(0, .5f);
		leaderWindow.transform.DOScale(0, .5f);
		questWindow.transform.DOScale(0, .5f);
		prepWindow.transform.DOScale(1, .5f);
		upgradeWindow.transform.DOScale(0, .5f);
		//abilityWindow.transform.DOScale(0, .5f);
	}
	public void OpenUpgradeWindow()
    {
		menuWindow.transform.DOScale(0, .5f);
		leaderWindow.transform.DOScale(0, .5f);
		questWindow.transform.DOScale(0, .5f);
		prepWindow.transform.DOScale(0, .5f);
		upgradeWindow.transform.DOScale(1, .5f);
		//abilityWindow.transform.DOScale(0, .5f);
	}		

	public void x2ButtonScale()
    {
		if (x2Button.active && !showed)
		{
			showed = true;
			x2Button.transform.DOPunchScale(Vector3.one * .2f, 1f, 10, 1).OnComplete(SecondPunch);
		}
    }

	private void SecondPunch()
    {
		x2Button.transform.DOPunchScale(Vector3.one * .2f, 1f, 5, 1);
	}


    private void Update()
    {
		rewardText.text = "+" + WaveSpawner.instance.reward * movingArrow.GetModif();

	}
}
