using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class MovingArrow : MonoBehaviour
{
    public bool isActive = true;

    public bool _isPlaying = false;
    RectTransform transforms;
   // [SerializeField, NotNull] private RectTransform[] points;

    int step = -1;
    int speed = 300;

    int modif = 0;

    private void Start()
    {
        transforms = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (!isActive) return;
        modif = 5 - Mathf.Abs(Convert.ToInt32(transforms.anchoredPosition.x)) / 125;
        //Debug.Log(modif + " modif");

        /*if (transforms.anchoredPosition.x > 480 || transforms.anchoredPosition.x < -480)
            step = -step;

        transforms.Translate(new Vector3(speed * step, 0, 0) * Time.deltaTime);*/

        if (_isPlaying) return;
       // gameObject.SetActive(true);
        _isPlaying = true;

        TweenStart();
    }

    public int GetModif()
    {
        return modif;
    }


    private void TweenStart()
    {
        if (!isActive) return;
        transforms.DOAnchorPos(new Vector2(480, transforms.anchoredPosition.y), 2f).OnComplete(TweenReset);
    }
    private void TweenReset()
    {
        if (!isActive) return;
        transforms.DOAnchorPos(new Vector2(-480, transforms.anchoredPosition.y), 2f).OnComplete(TweenStart);
    }
}
