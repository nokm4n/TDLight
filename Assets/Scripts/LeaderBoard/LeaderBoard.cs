using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public List<LeaderItem> leaderItems;

    [NotNull] public Image[] countryFlags = new Image[19];

    public static LeaderBoard instance;

    LeaderUIItem uiItem;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one LeaderBoard in scene!");
            return;
        }
        instance = this;

       /* var p = Instantiate(uiItem, transform);
        p.item = leaderItems[0];
        p.Init();*/

    }


}
