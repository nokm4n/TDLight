using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int reward = 100;
    public string description = "Quest description";
    public bool isComplete = false;

    public int amount;

    private int questIndex;

    public QuestType type;

    public int prevAmount;
    public int curAmount;


    private void Awake()
    {
        if(type == QuestType.GetTowerLvl)
        {
            amount = LevelManager.instance.GetMaxTurretLvl() + 3;
            description = "Get tower lvl " + amount;
        }
    }
    /*void CheckQuest()
    {
        switch (type)
        {
            case Quest.QuestType.KillEnemy:
                break;
            case Quest.QuestType.KillBoss:
                break;
            case Quest.QuestType.MergeTowers:
                break;
            case Quest.QuestType.GetTowerLvl:
                break;
            case Quest.QuestType.CompleteQuests:
                break;
            case Quest.QuestType.LeaderBoard:
                break;
        }

        if (curAmount >= amount)
        {
            isComplete = true;
        }

    }*/
    /*public int GetQuestIndexs()
    {
        return questIndex;
    }*/

    public int GetQuestIndex()
    {
        switch (type)
        {
            case Quest.QuestType.KillEnemy:
                return 0;
                break;
            case Quest.QuestType.KillBoss:
                return 1;
                break;
            case Quest.QuestType.MergeTowers:
                return 2;
                break;
            case Quest.QuestType.GetTowerLvl:
                return 3;
                break;
            case Quest.QuestType.CompleteQuests:
                return 4;
                break;
            case Quest.QuestType.LeaderBoard:
                return 5;
                break;
            default:
                return -1;
                break;
        }
    }

    public enum QuestType
    {
        KillEnemy,
        KillBoss,
        MergeTowers,
        GetTowerLvl,
        CompleteQuests,
        LeaderBoard
    }


    /*
    Index   Quest type      Amount Description

    1       KillEnemy       count enemies to kill
    2       KillBoss        index of boss Lvl
    3       MergeTowers     count towers to merge
    4       GetTowerLvl     Lvl of tower to get
    5       CompleteQuests  count quests to complete
    6       LeaderBoard     index of place in leaderboard to get
    */
}
