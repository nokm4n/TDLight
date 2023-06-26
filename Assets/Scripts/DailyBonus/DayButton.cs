using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayButton : MonoBehaviour
{
    [SerializeField, NotNull] private Image completeImage;

    private Button dayButton;

    public bool isComplete = false;

    private void Awake()
    {
        dayButton = GetComponent<Button>();
    }


}
