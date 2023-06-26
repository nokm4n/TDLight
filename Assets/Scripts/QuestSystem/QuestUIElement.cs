using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestUIElement : MonoBehaviour
{
    [SerializeField, NotNull] private TextMeshProUGUI description;
    [SerializeField, NotNull] private TextMeshProUGUI rewardText;

    [SerializeField, NotNull] private TextMeshProUGUI barText;
    [SerializeField, NotNull] private Button rewardButton;

    [SerializeField, NotNull] private Image progressBar;
    private int index;

    private void Awake()
    {
        rewardButton.onClick.AddListener(() => ClaimReward());
    }

    public void SetQuestWindow(int index)
    {
        this.index = index;
        barText.text = QuestList.instance.currentQuests[index].curAmount.ToString() + " / " + (QuestList.instance.currentQuests[index].amount).ToString();
        description.text = QuestList.instance.currentQuests[index].description;
        progressBar.fillAmount = (float)QuestList.instance.currentQuests[index].curAmount / QuestList.instance.currentQuests[index].amount;
        rewardText.text = QuestList.instance.currentQuests[index].reward.ToString();

        if (QuestList.instance.currentQuests[index].isComplete)
        {
            rewardButton.gameObject.SetActive(true);
           // progressBar.gameObject.SetActive(false);
        }
        else
        {
            rewardButton.gameObject.SetActive(false);
           // progressBar.gameObject.SetActive(true);
        }
    }

    private void ClaimReward()
    {
        //player stats ++
        PlayerStats.instance.AddDiamonds(QuestList.instance.currentQuests[index].reward);

        QuestBonus.instance.QuestComplete();

        QuestList.instance.SetNewQuest(index);

        SetQuestWindow(index);
    }

    public void ProgressBarAnim()
    {

        //progressBar.fillAmount
    }


}
