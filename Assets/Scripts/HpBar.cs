using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private GameObject hpBarPrefab;
    void Update()
    {
        if(hpBar.fillAmount <=0 || hpBar.fillAmount>=1)
        {
            hpBarPrefab.active = false;
        }
        else
        {
            hpBarPrefab.active = true;
        }
    }
}
