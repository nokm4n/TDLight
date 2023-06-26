using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    [SerializeField, NotNull] private Sprite enabledImg;
    [SerializeField, NotNull] private Sprite disabledImg;

    [SerializeField, NotNull] private AudioListener audioListener;

    private Image img;
    private bool isActive = true;


    private void Start()
    {
        img = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(ChangeState);

        if (PlayerPrefs.HasKey("Music"))
            isActive = Convert.ToBoolean(PlayerPrefs.GetInt("Music"));
        else
            isActive = true;

        if (isActive)
        {
            img.sprite = enabledImg;
        }
        else
        {
            img.sprite = disabledImg;

        }
        audioListener.enabled = isActive;

    }
    public void ChangeState()
    {
        if(isActive)
        {
            isActive = false;
            img.sprite = disabledImg;
            audioListener.enabled = false;

        }
        else
        {
            isActive = true;
            img.sprite = enabledImg;
            audioListener.enabled=true;

        }

        PlayerPrefs.SetInt("Music", isActive?1:0);
    }
}
