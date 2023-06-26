using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace TurretSystem
{
    [CreateAssetMenu(fileName = "New Turret Type", menuName = "Turrets/New Type")]
    public class TurretObject : ScriptableObject
    {
        public float range { get { return _range; } }
        public float fireRate { get { return _fireRate; } }
       // public float turnSpeed { get { return _turnSpeed; } }
        public ProjectileSystem.ProjectileObject bulletPrefab { get { return _bullet; } }

        //public TurretType type = TurretType.Fast;
        
        public enum TurretType
        {
            Fast,
            Normal,
            Tough
        }

        [SerializeField, Range(0f, 25f)] private float _range ;
        [SerializeField, Range(0f, 5f)] private float _fireRate;
        //[SerializeField, Range(0f, 360f)] private float _turnSpeed = 10f;
        [SerializeField, NotNull] private ProjectileSystem.ProjectileObject _bullet;      

    }
}
