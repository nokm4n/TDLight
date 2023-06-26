using UnityEngine;

namespace ProjectileSystem {
    public class ProjectileBase : MonoBehaviour
    {

        public ProjectileObject type;
        protected Transform _target;
        protected Transform _targetCenter;
        
        protected float damage = 1;

        protected GameObject _explodeEffect;
        public bool CheckForNoTarget()
        {
            if (_target != null)
            {
                return false;
            }
            return true;
        }

        public void SetUpExplodeEffect(GameObject explodeEffect)
        {
            _explodeEffect = explodeEffect;
        }

        public void SetUpTarget(Enemy _target)
        {
            this._target = _target.transform;
            _targetCenter = _target.modelCenter;
        }

        public void SetDamage(float damage)
        {
            this.damage = damage;
        }

        protected void HitTargetOnBase(float damage)
        {
            if (type.explosionRadius > 0f)
            {
                Explode(damage);
            }
            else
            {
                Damage(_target, damage);
            }
        }

        protected void HitEffects()
        {
            if(TryGetComponent<ProjectileMoveScript>(out var proj))
            {
                proj.Collision(_target);
            }
            //GameObject effectIns = Instantiate(type.impactEffect, _target.position + new Vector3(0, .2f, 0), _target.rotation);
            //Destroy(effectIns, 2f); 
        }



        protected void Damage(Transform enemy, float amount)
        {
            if (enemy == null) return;
            Enemy e = enemy.GetComponent<Enemy>();

            if (e != null)
            {
                e.TakeDamage(amount);
                //e.Slow(type.slowAmount);
            }
        }
        protected void Explode(float damage)
        {
            bool exploded = false;

            Collider[] colliders = Physics.OverlapSphere(transform.position, type.explosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<Enemy>(out var e))
                {
                    Damage(collider.transform, damage);

                    exploded = true;
                }
            }
            if(exploded && _explodeEffect != null)
            {
                if (_explodeEffect != null)
                {
                    var muzzleVFX = Instantiate(_explodeEffect, transform.position, Quaternion.identity);
                    var ps = muzzleVFX.GetComponent<ParticleSystem>();
                    if (ps != null)
                        Destroy(muzzleVFX, ps.main.duration);
                    else
                    {
                        var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(muzzleVFX, psChild.main.duration);
                    }
                }
            }
        }

    }
}
