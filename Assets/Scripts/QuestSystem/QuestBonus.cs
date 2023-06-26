using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBonus : MonoBehaviour
{
    [SerializeField, NotNull] private Image bonusBar;
    [NotNull] public List<BonusItem> bonusItems = new List<BonusItem>(5);

    private int countCompletedQuests = 0;
    private bool avalibleToCollect = false;

    public static QuestBonus instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Grabber in scene!");
            return;
        }
        instance = this;

        Init(0);
        bonusBar.fillAmount = 0;
    }

    public void Init(int countCompletedQuests)
    {
        this.countCompletedQuests = countCompletedQuests;
        if(countCompletedQuests > 5)
        {
            Debug.Log("quest init error");
            return;
        }

        for (int i = 0; i < countCompletedQuests; i++)
        {
            bonusItems[i].SetComplete();
        }
    }


   /* private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            QuestComplete();
        }
        if (Input.GetKeyDown(KeyCode.E) && avalibleToCollect)
        {
            CollectBonus();
        }
    }*/

    public void QuestComplete()
    {
        if (countCompletedQuests < 5)
        {
            bonusItems[countCompletedQuests].SetComplete();
            bonusBar.fillAmount = countCompletedQuests / 4f;
            countCompletedQuests++;

        }
        if (countCompletedQuests >= 5)
        {
            avalibleToCollect = true;
        }
    }

    private void CollectBonus()
    {
        countCompletedQuests = 0;
        for (int i = 0; i < bonusItems.Count; i++)
        {
            bonusItems[i].SetNoComplete();
        }
        bonusBar.fillAmount = 0;
        avalibleToCollect =false;    
        Debug.Log("bonus");
        // bonus
    }
}
