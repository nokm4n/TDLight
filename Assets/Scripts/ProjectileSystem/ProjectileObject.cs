using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace ProjectileSystem
{
    [CreateAssetMenu(fileName = "New Projectile Type", menuName = "Projectile/New Type")]
    public class ProjectileObject : ScriptableObject
    {
        public float speed { get { return _speed; } }
     //   public int damage { get { return _damage; } }
        public float explosionRadius { get { return _explosionRadius; } }


        //public float slowAmount { get { return _slowAmount; } }
        //public GameObject impactEffect { get { return _impactEffect; } }

        [SerializeField, Range(0f, 150f)] private float _speed = 70f;
       // [SerializeField, Range(0, 140)] private int _damage = 50;
        [SerializeField, Range(0f, 10f)] private float _explosionRadius = 0f;
        //[SerializeField, Range(0f, 1f)] private float _slowAmount;
        [SerializeField] protected GameObject _impactEffect;
        [SerializeField, NotNull] private ProjectileBase _prefab;
        ProjectileBase bulletObjects = null;
        //private bool _instatiated = false;

        public void InstantiateProjectile(Transform parent, Enemy target, float damage)
        {
            ProjectileBase bulletObject = Instantiate(_prefab, parent);
            if (bulletObject != null)
            {
                bulletObject.type = this;
                //Debug.Log("set target");
                bulletObject.SetUpTarget(target);
                bulletObject.SetDamage(damage);

                if(_impactEffect != null)
                {
                    bulletObject.SetUpExplodeEffect(_impactEffect);
                }
            }
            else
            {
                Debug.Log("error");
            }
        }

        public ProjectileBase UseLaser(Transform parent, Enemy target, float damage)
        {
            ProjectileBase bulletObject = Instantiate(_prefab, parent);
            if (bulletObject != null)
            {
                bulletObject.type = this;
                //Debug.Log("set target");
                bulletObject.SetUpTarget(target);
                bulletObject.SetDamage(damage);
            }
            else
            {
                Debug.Log("error");
            }
            return bulletObject;
            /*if (!_instatiated)
            {
                Debug.Log(parent.name);
                bulletObjects = Instantiate(_prefab, parent);   
                if (bulletObjects != null)
                {
                    _instatiated = true;
                    bulletObjects.SetUpTarget(target); 
                }
            }
            else
            {
                if (bulletObjects != null)
                {
                    bulletObjects.SetUpTarget(target);
                }
                else
                {
                    _instatiated = false;
                }
            }*/
        }
    }
}
