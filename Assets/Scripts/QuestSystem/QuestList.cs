using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    [SerializeField, NotNull] private List<Quest> questList;

    public Quest[] currentQuests = new Quest[3];
    public int completedQuestCount = 0;

    public static QuestList instance;

    public int[] curQuestsIndex  = new int[3];
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Grabber in scene!");
            return;
        }
        instance = this;
        SaveData.instance.LoadQuests();

        if (currentQuests[0] != null) return;
        //ListUtilities.Shuffle<Quest>(questList);
        for (int i = 0; i < currentQuests.Length; i++)
        {
            //instantiate quest!!!!!!
            //check for no same quests
            currentQuests[i] = Instantiate(questList[i]);
            //currentQuests[i].questIndex = i;
            //Debug.Log("Set quest " + questList[i].GetQuestIndex());
            curQuestsIndex[i] = questList[i].GetQuestIndex();
        }
    }


    public bool CheckQuest(Quest.QuestType checkType, out int index) // check if quest is complete
    {
        for (int i = 0; i < currentQuests.Length; i++)
        {
            index = i;
            if (currentQuests[i].type == checkType)
            {
                if (currentQuests[i].curAmount >= currentQuests[i].amount)
                {
                    currentQuests[i].isComplete = true;

                    return false;
                }
                else
                {
                    
                    return true;
                }
            }           
        }
        index = 0;
        return false;
    }

    public void UpdateQuest() //update quest info
    {
        for (int i = 0; i < currentQuests.Length; i++)
        {
            if (currentQuests[i].curAmount >= currentQuests[i].amount)
            {
                CheckForCompleteQuest();
                currentQuests[i].isComplete = true;
            }
            else
            {

            }
        }
    }

    public void SetQuest(int index, int amount, int curAmount, int QuestIndex)
    {
        currentQuests[index] = Instantiate(questList[QuestIndex]);
        currentQuests[index].amount = amount;
        currentQuests[index].curAmount = curAmount;
    }


    public void SetNewQuest(int index)
    {
        Destroy(currentQuests[index].gameObject);
        currentQuests[index] = null;
        int rnd = Random.Range(0, questList.Count);
        //for (int i = 0; i < questList.Count; i++)
        //{
        //Debug.Log("QuestInd " + questList[i].GetQuestIndex());

        while (questList[rnd].GetQuestIndex() == curQuestsIndex[0] || questList[rnd].GetQuestIndex() == curQuestsIndex[1] || questList[rnd].GetQuestIndex() == curQuestsIndex[2])
        {
            rnd = Random.Range(0, questList.Count);
        }
        currentQuests[index] = Instantiate(questList[rnd]);
        curQuestsIndex[index] = currentQuests[index].GetQuestIndex();
        Debug.Log("setQuest");
        return;
        /* if (questList[i].GetQuestIndex() != curQuestsIndex[0] && questList[i].GetQuestIndex() != curQuestsIndex[1] && questList[i].GetQuestIndex() != curQuestsIndex[2])
         {
             currentQuests[index] = Instantiate(questList[i]);
             curQuestsIndex[index] = currentQuests[index].GetQuestIndex();
             Debug.Log("setQuest");
             return;
           //  currentQuests[index].
         }*/
        //}

        if (currentQuests[index] == null)
        {
            Debug.Log("no Quest");
            currentQuests[index] = Instantiate(questList[0]);
        }


        //currentQuests[index] = 
        //Instantiate(questList[i]);
    }

    private void CheckForCompleteQuest()
    {
        int index = 0;
        if (CheckQuest(Quest.QuestType.CompleteQuests, out index))
        {
            currentQuests[index].curAmount++;
        }
    }

    public bool CheckQuests() //is there any completed quests
    {
        for (int i = 0; i < currentQuests.Length; i++)
        {
            if(currentQuests[i].isComplete)
            {
                return true;
            }
        }
        return false;
    }
}

