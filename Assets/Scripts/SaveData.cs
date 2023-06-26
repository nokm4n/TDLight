using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
	public static SaveData instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;

		//GetNodes();
	}
	public void Save(List<Node> nodes)
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("Money", PlayerStats.instance.GetCoins());
		PlayerPrefs.SetInt("Diamonds", PlayerStats.instance.GetDiamonds());
		PlayerPrefs.SetInt("Wave", WaveSpawner.instance.GetWaveIndex());
		PlayerPrefs.SetInt("Cost", LevelManager.instance.priceForTurret);



		for (int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i].turret != null)
				PlayerPrefs.SetInt("Node" + i, nodes[i].turret.turretLvl);
		}
		for (int i = 0; i < 3; i++)
		{
			//QuestList.instance
			PlayerPrefs.SetInt("Quest" + i, i); //index
			PlayerPrefs.SetInt("QuestA" + i, QuestList.instance.currentQuests[i].amount); //amount
			PlayerPrefs.SetInt("QuestCA" + i, QuestList.instance.currentQuests[i].curAmount); // current amount
			PlayerPrefs.SetInt("QuestI" + i, QuestList.instance.currentQuests[i].GetQuestIndex()); // quest index
		}
		PlayerPrefs.SetFloat("DamageUp", UpgradeStats.instance.damageModif);
		PlayerPrefs.SetFloat("SpeedUp", UpgradeStats.instance.speedModif);
		PlayerPrefs.SetFloat("MoneyUp", UpgradeStats.instance.moneyModif);
		PlayerPrefs.SetInt("LvlUp", UpgradeStats.instance.lvlModif);


		PlayerPrefs.Save();
	}

	public void Load()
	{

		if (PlayerPrefs.HasKey("Cost"))
		{
			LevelManager.instance.priceForTurret = (PlayerPrefs.GetInt("Cost"));
		}
		else
		{
			LevelManager.instance.priceForTurret = 5;
		}

		if (PlayerPrefs.HasKey("Money"))
		{
			PlayerStats.instance.SetCoins(PlayerPrefs.GetInt("Money"));
		}	
		else
		{
			PlayerStats.instance.SetCoins(8);
		}

		if (PlayerPrefs.HasKey("Diamonds"))
		{
			PlayerStats.instance.SetDiamonds(PlayerPrefs.GetInt("Diamonds"));
		}
		else
		{
			PlayerStats.instance.SetDiamonds(0);
		}

		if (PlayerPrefs.HasKey("Wave"))
		{
			WaveSpawner.instance.SetWaveIndex(PlayerPrefs.GetInt("Wave"));
		}
		else
		{
			WaveSpawner.instance.SetWaveIndex(0);
		}

		for (int i = 0; i < 15; i++) // nodes count
		{
			if (PlayerPrefs.HasKey("Node" + i))
			{
				Debug.Log(PlayerPrefs.GetInt("Node" + i) + "Node"+i);
				LevelManager.instance.SetTurretOnNode(i, PlayerPrefs.GetInt("Node" + i));
			}
		}

		/*if(PlayerPrefs.HasKey("DamageUp"))
        {
			UpgradeStats.instance.damageModif = PlayerPrefs.GetFloat("DamageUp");
			UpgradeStats.instance.speedModif = PlayerPrefs.GetFloat("SpeedUp");
			UpgradeStats.instance.moneyModif = PlayerPrefs.GetFloat("MoneyUp");
			UpgradeStats.instance.moneyModif= PlayerPrefs.GetInt("LvlUp");
		}*/
		if (PlayerPrefs.HasKey("DamageUp"))
		{
			UpgradeStats.instance.LoadData(PlayerPrefs.GetFloat("DamageUp"), PlayerPrefs.GetFloat("SpeedUp"), PlayerPrefs.GetFloat("MoneyUp"), PlayerPrefs.GetInt("LvlUp"));
		}
		else
		{
			UpgradeStats.instance.LoadData(1, 1, 1, 0);

		}

		//LoadQuests();
	}
	public void DeleteSaves()
    {
		PlayerPrefs.DeleteAll();
	}

	public void LoadQuests()
    {
		for (int i = 0; i < 3; i++)
		{
			if (PlayerPrefs.HasKey("Quest" + i))
			{
				int index = PlayerPrefs.GetInt("Quest" + i);
				int amount = PlayerPrefs.GetInt("QuestA" + i);
				int currentAmount = PlayerPrefs.GetInt("QuestCA" + i);
				int questIndex = PlayerPrefs.GetInt("QuestI" + i);
				QuestList.instance.SetQuest(index, amount, currentAmount, questIndex);
			}
		}
	}
}
