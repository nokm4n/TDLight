using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace EnemySystem
{
    [CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy/New Enemy Type")]
    public class EnemyObject : ScriptableObject
    {
        //public float startSpeed { get { return _startSpeed; } }
        //public float startHealth { get { return _startHealth; } }
        //public int worth { get { return _worth; } }
        //public GameObject deathEffect { get { return _deathEffect; } }

        //[SerializeField] float _startSpeed = 1f;
        //[SerializeField] float _startHealth = 100;
        //[SerializeField] int _worth = 50;
        [SerializeField, NotNull] GameObject _deathEffect;

    }
}
