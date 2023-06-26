using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileSystem
{
    public class PierceScript : ProjectileBase
    {
        Vector3 dir;
        private bool noTarget = false;
        void Awake()
        {
            transform.SetParent(null);
        }
        void Update()
        {
            if (transform.position.z > 15 || transform.position.z < -15 || transform.position.x > 15 || transform.position.x < -15)
            {
                Destroy(gameObject);
            }
            if (CheckForNoTarget() || noTarget)
            {
               // dir = Vector3.forward;
                //Destroy(gameObject, 2f);
                //return;
            }
            else
            {
                
                //dir = _target.position - transform.position;
                dir = new Vector3(_target.position.x, _target.position.y + .7f, _target.position.z) - transform.position;
                transform.LookAt(_targetCenter);
            }
            float distanceThisFrame = type.speed * Time.deltaTime;
            //dir.y = 0f;

            if (dir.magnitude <= distanceThisFrame && !CheckForNoTarget())
            {
                if (!_target.GetComponent<Enemy>().isHitted)
                {
                    Hit();
                    _target.GetComponent<Enemy>().isHitted = true;
                    _target = null;
                    noTarget = true;
                }
                _target = null;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);


        }

        void Hit()
        {
            AudioController.instance.PlaySound("hit");

            //HitTargetOnBase(type.damage);
            HitTargetOnBase(damage);
            HitEffects();
            return;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out var e))
            {
                if (!e.isHitted)
                {
                    e.isHitted = true;
                    Debug.Log("Hit pierce");
                    _target = e.transform;
                    Hit();
                    _target = null;
                }
            }
        }
    }
}
