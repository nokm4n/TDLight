using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	//public EnemySystem.EnemyObject typeObject;

	[HideInInspector]
	private float _speed;

	[Header("Unity Stuff")]
	public Image healthBar;


	private bool _isDead = false;
	public float _health;
	private float baseHp;
	public bool isBoss = false;

	[HideInInspector]
	public Animator animator;

	public Transform modelCenter;
	public bool isHitted = false;

	//[SerializeField, NotNull] GameObject deathEffect;

	void Start()
	{
		animator = GetComponent<Animator>();
		_speed = .8f;

		LevelManager.instance.enemies.Add(this);
		baseHp = _health;

		if(WaveSpawner.instance.menuOpened)
        {
			_speed = 0;
			animator.SetTrigger("Dance");
			Die();
		}
	}

	public void TakeDamage(float amount)
	{
		_health -= amount;
		healthBar.fillAmount = _health / baseHp;
		if (_health <= 0 && !_isDead)
		{
			Die();
		}
	}
	
	public void BoostHp()
    {
		_health *= 2;
    }
	public void ReturnHP()
    {
		_health = baseHp;
    }

	public void Slow(float pct, bool multiply)
	{
		if(multiply)
        {
			_speed *= pct;
        }
		else
        {
			_speed /= pct;
        }
		//_speed = (1f - pct);
	}

	public void SetHP(float hp)
    {
		_health = hp;
		baseHp = hp;
		if (isBoss)
		{
			_health++;
			_health *= 10;
		}
	}

	public void Die()
	{
		CheckQuest();
		
		_speed = 0;
		animator.SetTrigger("Die");
		_isDead = true;

		PlayerStats.instance.AddCoins(WaveSpawner.instance.GetWaveIndex() / 5 + 1);
		CoinCountAnimator.instance.AnimateSkull();

		CoinCountAnimator.instance.AnimateCoinCollect(transform.position);

		//GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		//Destroy(effect, 2f);
		WaveSpawner.EnemiesAlive--;
		LevelManager.instance.enemies.Remove(this);
		GetComponent<BoxCollider>().enabled = false;


		Destroy(gameObject, .7f);
	}

	public void Dance()
    {
		animator.SetTrigger("Dance");
		_isDead = true;

		WaveSpawner.EnemiesAlive--;
		LevelManager.instance.enemies.Remove(this);
		Destroy(gameObject, 3f);
	}
	public float GetSpeed()
	{
		return _speed;
	}


	private void CheckQuest()
	{
		int index = 0;
		if (isBoss)
		{
			if (QuestList.instance.CheckQuest(Quest.QuestType.KillBoss, out index))
			{
				QuestList.instance.currentQuests[index].curAmount = QuestList.instance.currentQuests[index].amount; //переделать под сравнение уровня ???
			}
		}
		else 
		if (QuestList.instance.CheckQuest(Quest.QuestType.KillEnemy, out index))
		{
			QuestList.instance.currentQuests[index].curAmount++;
			//Debug.Log("+enemy");
		}
		//QuestList.instance.UpdateQuest();
		//Debug.Log(index);
	}
}

