using UnityEngine;
public class LeaderLists : MonoBehaviour
{
    public TextAsset namesTxt;

    public string[] names = new string[1000];

    private void Awake()
    {
        names = namesTxt.text.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < 6000; i++)
        {
            LeaderItem item = new LeaderItem();

            item.country = (CountryList)Random.Range(0, 19);
            if (i >= names.Length)
            {
                item.name = names[i % names.Length];
            }
            else
            {
                item.name = names[i];

            }
            item.position = i;
            item.killCount = 6000 - i;

            LeaderBoard.instance.leaderItems.Add(item);
            // LeaderBoard.instance.leaderItems.Add();
        }
    }

    
}
