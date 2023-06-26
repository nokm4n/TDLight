using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int coins;
    private int diamonds;

    [SerializeField, NotNull] private TextMeshProUGUI coinsText;
    [SerializeField, NotNull] private TextMeshProUGUI coinsText2;

    [SerializeField, NotNull] private TextMeshProUGUI diamondsText;
    [SerializeField, NotNull] private TextMeshProUGUI diamondsText2;

    [SerializeField, NotNull] private BuyButton buyButton;

    public static PlayerStats instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one PlayerStats in scene!");
            return;
        }
        instance = this;

        //buyButton.Check();
    }

    public void SetCoins(int coins)
	{
        this.coins = coins;
        coinsText.text = new string("" + this.coins);
        coinsText2.text = new string("" + this.coins);

        buyButton.Check();

    }

    public void SetDiamonds(int diamonds)
    {
        this.diamonds = diamonds;
        diamondsText.text = new string("" + this.diamonds);
        diamondsText2.text = new string("" + this.diamonds);

    }
    public void Buy(int amount)
	{
        coins -= amount;
        coinsText.text = new string("" + coins);
        coinsText2.text = new string("" + coins);

        buyButton.Check();
    }
    public void AddCoins(int amount)
    {
        coins += amount;
        coinsText.text = new string("" + coins);
        coinsText2.text = new string("" + coins);

        buyButton.Check();
    }

    public void BuyDiamonds(int amount)
    {
        diamonds -= amount;
        diamondsText.text = new string("" + diamonds);
        diamondsText2.text = new string("" + diamonds);
    }
    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        diamondsText.text = new string("" + diamonds);
        diamondsText2.text = new string("" + diamonds);
    }

    public int GetCoins()
	{
        return coins;
	}

    public int GetDiamonds()
    {
        return diamonds;
    }
}
