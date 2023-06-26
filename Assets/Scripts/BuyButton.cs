using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField, NotNull] private Sprite buttonGreen;
    [SerializeField, NotNull] private Sprite buttonGrey;

    [SerializeField] private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void Check()
    {
        if(PlayerStats.instance.GetCoins() >= LevelManager.instance.priceForTurret)
        {
            button.image.sprite = buttonGreen;
        }
        else
        {
            button.image.sprite = buttonGrey;
        }
    }
}
