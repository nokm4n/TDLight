using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PopUpButton : MonoBehaviour
{
    public RectTransform rect;

    public bool isVisible;
    public float scaleTime = 0.25f;

    public void ChangeMenuState()
    {
        if (!isVisible)
        {
            isVisible = true;
            rect.DOScale(Vector3.one, scaleTime);
        }
        else
        {
            isVisible = false;
            rect.DOScale(Vector3.zero, scaleTime);
        }
    }
}
