using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveListData", order = 1)]
public class WaveList : ScriptableObject
{
    public List<Wave> waves = new List<Wave>();
}
