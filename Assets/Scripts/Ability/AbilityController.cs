using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{
	[SerializeField, NotNull] private AbilityDamage[] abilities;
	//[SerializeField, NotNull] private Transform abilitiesSpawnPoint;

	[SerializeField, NotNull] private Button[] buyButtons;
	[SerializeField, NotNull] private Button[] selectButtons;

	private void Awake()
	{
		for(int i = 0; i < abilities.Length; i++)
		{
			if (PlayerPrefs.HasKey("Ability" + i))
			{
				abilities[i].bought = Convert.ToBoolean(PlayerPrefs.GetInt("Ability" + i));
				buyButtons[i].enabled = false;
				selectButtons[i].enabled = true;
			}
			else
			{
				buyButtons[i].enabled = true;
				selectButtons[i].enabled = false;
			}

		}
	}


	public void BuyAbility(int index)
	{
		if (PlayerStats.instance.GetDiamonds() > abilities[index].price && !abilities[index].bought)
		{
			PlayerStats.instance.BuyDiamonds(abilities[index].price);
			abilities[index].bought = true;

			PlayerPrefs.SetInt("Ability" + index, 1);

			buyButtons[index].enabled = false;
			selectButtons[index].enabled = true;
		}
	}

	public void SelectAbility(int index)
	{
		if (abilities[index].bought)
		{
			for (int i = 0; i < abilities.Length; i++)
			{
				abilities[i].SetActiveAbility(false);

			}

			abilities[index].SetActiveAbility(true);
		}
	}
}
