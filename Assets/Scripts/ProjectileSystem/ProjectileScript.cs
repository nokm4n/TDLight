using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileSystem
{
    public class ProjectileScript : ProjectileBase
    {
        Vector3 dir;
        void Awake()
        {
            transform.SetParent(null);
            //AudioManager.instance.Play(soundName);
        }
        void Update()
        {
            if (transform.position.z > 15 || transform.position.z < -15 || transform.position.x > 15 || transform.position.x < -15)
            {
                Destroy(gameObject);
            }
            if (CheckForNoTarget())
            {
                //dir = Vector3.forward;
                //Destroy(gameObject, 2f);
                //return;
            }
            else
            {

                dir = new Vector3(_target.position.x, _target.position.y+.7f, _target.position.z) - transform.position;
                transform.LookAt(_targetCenter);
            }
            float distanceThisFrame = type.speed * Time.deltaTime;
            //dir.y = -.1f;
            if (dir.magnitude <= distanceThisFrame && !CheckForNoTarget())
            {
                Hit();
            }
            
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            

        }
   
        void Hit()
        {
            AudioController.instance.PlaySound("hit");

            //HitTargetOnBase(type.damage);
            HitTargetOnBase(damage);
            HitEffects();
            
            //Debug.Log(damage + " damage");
            Destroy(gameObject);
            return;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Enemy>(out var e))
            {
                //Debug.Log("Hit no target");
                _target = e.transform;
                Hit();
            }
        }
    }
}
