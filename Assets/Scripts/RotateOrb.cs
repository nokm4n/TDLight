using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOrb : MonoBehaviour
{
    public bool invert = false;
    private int rot = 0;
    private float speed = .7f;
    private int offset = 0;
    private float offset2 = 0;
    // Update is called once per frame
    private void Awake()
    {
        OnScale();
        // OnRotate();
        speed = Random.Range(.5f, 1.5f);
        offset = Random.Range(50, 120);
        offset2 = Random.Range(-.2f, .2f);
    }
    void Update()
    {
       /* if(invert)
        {
            transform.Rotate(new Vector3(0, 1, 0), 1);   
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 1), 1);
        }*/

    }

    void OnScale()
    {
        rot += offset;
        transform.DOScale(1f + offset2, speed).OnComplete(OfScale);
        transform.DORotate(new Vector3(0, rot, 0), speed).SetEase(Ease.Linear);
    }

   /* void OnRotate()
    {
        rot += 90;
        if (invert)
            transform.DORotate(new Vector3(0, rot, 0), .7f).SetEase(Ease.InSine).OnComplete(OnRotate);
        else
            transform.DORotate(new Vector3(rot, 0, 0), .7f).SetEase(Ease.InSine).OnComplete(OnRotate);
    }*/

    void OfScale()
    {
        rot += offset;
        transform.DOScale(.85f + offset2, speed).OnComplete(OnScale);
        transform.DORotate(new Vector3(0, rot, 0), speed).SetEase(Ease.Linear);
    }


}

