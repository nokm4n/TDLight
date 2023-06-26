using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField, NotNull] private QuestUIElement[] questUIElements = new QuestUIElement[3];

    public static QuestUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Grabber in scene!");
            return;
        }
    }

    public void SetQuests()
    {
        QuestList.instance.UpdateQuest();

        for (int i = 0; i < 3; i++)
        {
            questUIElements[i].SetQuestWindow(i);
        }
    }
}
