using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderUIItem : MonoBehaviour
{
   // public LeaderItem item;
    
    [SerializeField, NotNull] private TextMeshProUGUI score;
    [SerializeField, NotNull] private TextMeshProUGUI names;
    [SerializeField, NotNull] private TextMeshProUGUI position;
    [SerializeField] private Image countryFlag;


    public void Init(LeaderItem item)
    {
        score.text = item.killCount.ToString();
        names.text = item.names.ToString();
        position.text = item.position.ToString();
    }
}
