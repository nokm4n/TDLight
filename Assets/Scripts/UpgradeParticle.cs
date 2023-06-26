using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeParticle : MonoBehaviour
{
	private ParticleSystem upgradePrefab;
    private void Awake()
    {
		upgradePrefab = GetComponent<ParticleSystem>();
		if (upgradePrefab != null)
		{
			Destroy(upgradePrefab, upgradePrefab.main.duration);
		}
	}
}
