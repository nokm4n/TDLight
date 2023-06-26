using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStats : MonoBehaviour
{
    public static UpgradeStats instance;

    private int damageCost = 180;
    public float damageModif = 1;
    private bool damageAvalible = true;

    private int speedCost = 250;
    public float speedModif = 1;
    private bool speedAvalible = true;

    private int moneyCost = 275;
    public float moneyModif = 1;
    private bool moneyAvalible = true;

    private int lvlCost = 6;
    public int lvlModif = 0;
    public bool lvlAvalible = true;

    [SerializeField, NotNull] private TextMeshProUGUI speedText;
    [SerializeField, NotNull] private TextMeshProUGUI damageText;
    [SerializeField, NotNull] private TextMeshProUGUI moneyText;
    [SerializeField, NotNull] private TextMeshProUGUI lvlText;

    [SerializeField, NotNull] private TextMeshProUGUI speedProcentText;
    [SerializeField, NotNull] private TextMeshProUGUI damageProcentText;
    [SerializeField, NotNull] private TextMeshProUGUI moneyProcentText;
    [SerializeField, NotNull] private TextMeshProUGUI lvlProcentText;

    [SerializeField, NotNull] private Button speedButton;
    [SerializeField, NotNull] private Button damageButton;
    [SerializeField, NotNull] private Button moneyButton;
    [SerializeField, NotNull] private Button lvlButton;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one UpgradeStats in scene!");
            return;
        }
        instance = this;
    }

    public void LoadData(float damage, float speed, float money, int lvl)
    {
        damageModif = damage;
        speedModif = speed;
        moneyModif = money;
        lvlModif = lvl;


        moneyCost = moneyCost + ((int)((moneyModif - 1) / 0.05f * 100));

        speedCost = speedCost + ((int)((speedModif - 1) / 0.05f * 50));

        damageCost = damageCost + ((int)((damageModif - 1) / 0.05f * 70));

        lvlCost = lvlCost + ((int)((lvlModif - 1) / 5));

        speedText.text = speedCost.ToString();
        damageText.text = damageCost.ToString();
        moneyText.text = moneyCost.ToString();
        lvlText.text = lvlCost.ToString();

        speedProcentText.text = (speedModif*100).ToString() + "%";
        damageProcentText.text = (damageModif*100).ToString() + "%";
        moneyProcentText.text = (moneyModif*100).ToString() + "%";
        lvlProcentText.text = lvlModif.ToString() + " Level";
        //lvl??
    }
    public void BuySpeed()
    {
        if (!speedAvalible) return;

        if(PlayerStats.instance.GetDiamonds() >= speedCost)
        {
            speedModif += .05f;
            PlayerStats.instance.BuyDiamonds(speedCost);
            speedCost += 50;

            speedText.text = speedCost.ToString();
            speedProcentText.text = (speedModif * 100).ToString() + "%";
        }
        else
        {
            Debug.Log("No diamonds");
        }
    }

    public void BuyDamage()
    {
        if (!damageAvalible) return;

        if (PlayerStats.instance.GetDiamonds() >= damageCost)
        {
            damageModif += .05f;
            PlayerStats.instance.BuyDiamonds(damageCost);
            damageCost += 70;

            damageText.text = damageCost.ToString();
            damageProcentText.text = (damageModif * 100).ToString() + "%";
        }
        else
        {
            Debug.Log("No diamonds");
        }
    }

    public void BuyMoney()
    {
        if(!moneyAvalible) return;

        if (PlayerStats.instance.GetDiamonds() >= moneyCost)
        {
            moneyModif += .05f;
            PlayerStats.instance.BuyDiamonds(moneyCost);
            moneyCost += 100;

            moneyText.text = moneyCost.ToString();
            moneyProcentText.text = (moneyModif * 100).ToString() + "%";
        }
        else
        {
            Debug.Log("No diamonds");
        }
    }

    public void BuyLvl()
    {
        if(!lvlAvalible) return;

        if(WaveSpawner.instance.GetWaveIndex() >= lvlCost)
        {
            lvlModif++;
            lvlCost += 5;

            lvlText.text = lvlCost.ToString();
            lvlProcentText.text = lvlModif.ToString() + " Level";

            LevelManager.instance.UpgradeTurretsOnScene(lvlModif);
        }
        else
        {
            Debug.Log("No diamonds");
        }
    }

    public void CheckStats()
    {
        if(speedModif >= 1.4f)
        {
            speedAvalible = false;
            speedButton.image.enabled = false;
            speedText.text = "Max";
        }

        if(damageModif >= 1.4f)
        {
            damageAvalible = false;
            damageButton.image.enabled = false;
            damageText.text = "Max";
        }

        if(moneyModif >= 1.4f)
        {
            moneyAvalible = false;
            moneyButton.image.enabled = false;
            moneyText.text = "Max";
        }

        if(lvlModif > 12)
        {
            lvlAvalible = false;
            lvlButton.image.enabled = false;
            lvlText.text = "Max";
        }
    }
}
