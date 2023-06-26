using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave 
{
	public List<Waves> waveStruct;
}

[System.Serializable] public struct Waves
{
	public Enemy[] enemy;
	public int count;
	//public float rate;
	public bool waveSkip;
}