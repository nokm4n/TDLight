using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public List<Turret> turrets = new List<Turret>();
    public List<Turret> turretsOnScene = new List<Turret>();
    public List<Enemy> enemies = new List<Enemy>();

    public int priceForTurret = 5;
    public int emptySpaces;
    [SerializeField, NotNull] private GameObject upgradePrefab;
    [SerializeField, NotNull] private GameObject x2Button;
    [SerializeField, NotNull] private GameObject slowButton;

    //[SerializeField, NotNull] private Image buttonGreen;
    //[SerializeField, NotNull] private Image buttonGrey;


    public static LevelManager instance;

    [SerializeField, NotNull] private TextMeshProUGUI buyText;
    private async void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than 1 LevelManager in scene");
        }
        instance = this;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].GetEmpty())
            {
                emptySpaces++;
            }
        }
        //await Task.Delay(200);
        await Task.Yield();

        SaveData.instance.Load();

        WaveSpawner.instance.SetWaveIndex();

        buyText.text = new string(priceForTurret.ToString());
        CheckQuestLvl();
    }
    public void SetTurretOnNode(int nodeIndex, int turretLvl)
    {
        emptySpaces--;
        nodes[nodeIndex].CreateTurret(turrets[turretLvl], turretLvl);
    }

    public void GameOver()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Slow(0, true);
        }

        WaveSpawner.instance.LoseScreen();
        while (enemies.Count > 0)
        {
            enemies[0].Dance();
        }
        Save();
    }

    public void BuyNewTurret()
    {
        if (emptySpaces <= 0)
        {
            Debug.Log("no empty spaces");
            return;
        }
        if (PlayerStats.instance.GetCoins() >= priceForTurret)
        {
            PlayerStats.instance.Buy(priceForTurret);
            priceForTurret++;
            buyText.text = new string(priceForTurret.ToString());
        }
        else
        {
            Debug.Log("no money");
            return;
        }
        int rand = Random.Range(0, nodes.Count);
        /* for (int i = 0; i < nodes.Count; i++)
         {
             if (nodes[i].GetEmpty())
             {
                 emptySpaces--;
                 nodes[i].CreateTurret(turrets[0], 0);
                 return;
             }
         }*/
        int temp = 0;
        
        while (!nodes[rand].GetEmpty())
        {
            temp++;
            rand = Random.Range(0, nodes.Count);
            if (temp > 10000) break;
        }
        
        emptySpaces--;
        if(UpgradeStats.instance.lvlModif < turrets.Count)
            nodes[rand].CreateTurret(turrets[UpgradeStats.instance.lvlModif], UpgradeStats.instance.lvlModif);
       
    }

    public Node GetClosestNode(Transform pos)
    {
        float dist = Vector3.Distance(pos.position, nodes[0].transform.position);
        int index = 0;
        for (int i = 1; i < nodes.Count; i++)
        {
            if (dist > Vector3.Distance(pos.position, nodes[i].transform.position))
            {
                dist = Vector3.Distance(pos.position, nodes[i].transform.position);
                index = i;
            }
            if (Vector3.Distance(pos.position, nodes[i].transform.position) < Grabber.instance.dist)
            {
                nodes[i].SetColor(true);
            }
            else
            {
                nodes[i].SetColor(false);
            }
        }
        return nodes[index];
    }

    public void Upgrade(Node targetNode)
    {
        // boost hp x2, if 5 rounds without upgrade               
        CheckQuestUpgrade();
        CheckQuestLvl();
        emptySpaces++;
        if (targetNode.turret.turretLvl < turrets.Count - 1)
        {
            targetNode.UpgrageTurret(turrets[targetNode.turret.turretLvl + 1], upgradePrefab);
        }
        else
        {
            targetNode.UpgrageTurret(turrets[(targetNode.turret.turretLvl)/9], upgradePrefab);
        }

    }

    public void KillEveryEnemy()
    {
        while (enemies.Count > 0)
        {
            enemies[0].Die();
        }
    }

    public void X2Speed()
    {
        if (x2Button.active)
            if (PlayerStats.instance.GetDiamonds() >= 200)
            {
                x2Button.active = false;
                PlayerStats.instance.BuyDiamonds(200);

                for (int i = 0; i < turretsOnScene.Count; i++)
                {
                    turretsOnScene[i].delays /= 2f;
                }
                StartCoroutine(StopX2());
            }
            else
            {
                Debug.Log("No diamonds");
            }
    }
    private IEnumerator StopX2()
    {
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < turretsOnScene.Count; i++)
        {
            turretsOnScene[i].delays *= 2f;
        }
    }

    public void SlowEnemy()
    {
        if(slowButton.active)
            if(PlayerStats.instance.GetDiamonds() >= 200)
            {
                slowButton.active = false;
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Slow(2, false);
                }
               // StartCoroutine(StopX2());
            }
            else 
            {
                Debug.Log("No diamonds");
            }
    }
    private IEnumerator ReturnEnemySpeed()
    {
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Slow(2f, true);
        }
    }


    public void Save()
    {
        SaveData.instance.Save(nodes);
    }

    private void CheckQuestUpgrade()
    {
        int index = 0;
        if (QuestList.instance.CheckQuest(Quest.QuestType.MergeTowers, out index))
        {
            QuestList.instance.currentQuests[index].curAmount++;
        }
        Debug.Log(index);
    }

    private void CheckQuestLvl()
    {
        int index = 0;
        if (QuestList.instance.CheckQuest(Quest.QuestType.GetTowerLvl, out index))
        {
            for (int i = 0; i < turretsOnScene.Count; i++)
            {
                if(QuestList.instance.currentQuests[index].curAmount < turretsOnScene[i].turretLvl)
                {
                    QuestList.instance.currentQuests[index].curAmount = turretsOnScene[i].turretLvl;
                    
                }
            }
        }
    }

    public void ChangeNodeTexture(Texture texture)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].SetTexture(texture);
        }
    }

    public void SwitchButton(bool act)
    {
        x2Button.active = act;
        slowButton.active = act;
    }

    public int GetMaxTurret()
    {
        int max = 1;
        for (int i = 0; i <turretsOnScene.Count; i++)
        {
            if(max< turretsOnScene[i].GetTurretDamage())
                max = turretsOnScene[i].GetTurretDamage();
        }
        return max;
    }

    public int GetMaxTurretLvl()
    {
        int max = 1;
        for (int i = 0; i < turretsOnScene.Count; i++)
        {
            if (max < turretsOnScene[i].turretLvl)
                max = turretsOnScene[i].turretLvl;
        }
        return max;
    }

    public int GetAllDamage()
    {
        int sum = 0;
        for (int i = 0; i < turretsOnScene.Count; i++)
        {
            sum += turretsOnScene[i].GetTurretDamage();
        }

        return sum;
    }

    public void Boost(float damage, float speed)
    {
        for (int i = 0; i < turretsOnScene.Count; i++)
        {
            turretsOnScene[i].SetDamageModif(damage);
            turretsOnScene[i].SetSpeedModif(speed);
        }
    }


    public void UpgradeTurretsOnScene(int minTurretLvl)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            while(nodes[i].turret.turretLvl < minTurretLvl)
            {
                emptySpaces++;
                if (nodes[i].turret.turretLvl < turrets.Count - 1)
                {
                    nodes[i].UpgrageTurret(turrets[nodes[i].turret.turretLvl + 1], upgradePrefab);
                }
                else
                {
                    nodes[i].UpgrageTurret(turrets[(nodes[i].turret.turretLvl) / 9], upgradePrefab);
                }

                Debug.Log("Turret Upgraded");
            }
        }
    }
}
