using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    [SerializeField, NotNull] private List<GameObject> lvlItems;
    [SerializeField, NotNull] private List<TextMeshProUGUI> lvlText;

    [SerializeField, NotNull] private List<Sprite> images;

    private int lvlsRate;
    public void SetItems()
    {
        lvlsRate = WaveSpawner.instance.GetWaveIndex() / 5;
        for (int i = 0; i < lvlText.Count; i++)
        {
            lvlText[i].text = (lvlsRate*5 + i + 1).ToString();
            lvlItems[i].GetComponent<Image>().sprite = images[0];
        }

        lvlItems[4].GetComponent<Image>().sprite = images[2];
        int curLvl = WaveSpawner.instance.GetWaveIndex() % 5;
        lvlItems[curLvl].transform.DOScale(1.2f, 0);
        lvlItems[curLvl].GetComponent<Image>().sprite = images[1];

    }

    public void SetBack()
    {
        for (int i = 0; i < lvlItems.Count; i++)
        {
            lvlItems[i].GetComponent<Image>().sprite = images[0];
            //change texture back
            lvlItems[i].transform.DOScale(1, 0);
        }
        lvlItems[4].GetComponent<Image>().sprite = images[2];
    }
}
