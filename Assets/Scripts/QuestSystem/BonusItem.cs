using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusItem : MonoBehaviour
{
    private bool isComplete = false;

    [SerializeField, NotNull] private Image image;
    
    public void SetNoComplete()
    {
        isComplete = false;
        image.enabled = false;
    }

    public void SetComplete()
    {
        isComplete = true;
        image.enabled = true;
    }

    private void Awake()
    {
        isComplete = false;
        image.enabled = false;
    }

    public bool IsComplete()
    {
        return isComplete;
    }
}
