using System.Collections;
using System.Collections.Generic;
using TMPro;
using TurretSystem;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //[HideInInspector]
    public int turretLvl;

    [Header("Prefab Variables")]
    public Transform partToRotate;
    public Transform firePoint;

    //public TurretType type;

    //public Node node;
    [Header("Data References")]
    [SerializeField, NotNull] private TurretObject turretData;

    public Enemy targetEnemy;

    [HideInInspector]
    public float delays;

    //private Transform bulletPool;
    public TextMeshPro text;

    public bool useLaser = false;

    public ProjectileSystem.ProjectileBase lazer;
    private float targetUpdateTimer;
    private float fireCooldown;

    private float damageModif = 1;
    private float speedModif = 1;
    private void Awake()
    {
        delays = turretData.fireRate / speedModif;

        if(delays <0 || delays > 10)
        {
            delays = turretData.fireRate;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (WaveSpawner.instance.menuOpened)
        {
            return;
        }
        TargetUpdateTimer(0.5f);

        if (targetEnemy != null)
        {
            LockOnTarget();
        }
        if (useLaser)
        {
            Lazer();
        }
        else
        {
            ShootUpdateTimer(delays);
        }
    }

    void TargetUpdateTimer(float delay)
    {
        targetUpdateTimer += Time.deltaTime;
        if (targetUpdateTimer >= delay)
        {
            targetUpdateTimer -= delay;
            UpdateTarget();
        }
    }
    void ShootUpdateTimer(float delay)
    {
        fireCooldown += Time.deltaTime;
        if (fireCooldown >= delay)
        {
            fireCooldown -= delay;
            if (targetEnemy == null) return;
            Shoot();
        }
    }
    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;

        
        if (LevelManager.instance.enemies != null)
        foreach (Enemy enemy in LevelManager.instance.enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
           // if (distanceToEnemy < 3.5f)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (!useLaser)
            //Debug.Log(shortestDistance);
            if (shortestDistance > 3.5f && shortestDistance < turretData.range)
            {
               // do
                {
                    nearestEnemy = LevelManager.instance.enemies[Random.Range(0, LevelManager.instance.enemies.Count/2)];
                    shortestDistance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
                }
                //while (shortestDistance < turretData.range);
            }


        if (nearestEnemy != null && shortestDistance < turretData.range)
        {
            targetEnemy = nearestEnemy;
        }
        else
        {
            targetEnemy = null;
        }
        
            

    }
    void LockOnTarget()
    {
        Vector3 dir = targetEnemy.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
       // Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretData.turnSpeed).eulerAngles;
        //rotation.y = Mathf.Clamp(rotation.y, 0, 10);
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Shoot()
    {
        turretData.bulletPrefab.InstantiateProjectile(firePoint, targetEnemy, (2 * (turretLvl + 1) + Random.Range(0, 2)) * damageModif);
    }

    void Lazer()
    {
        if (targetEnemy == null) return;
        if(lazer == null)
        {
            lazer = turretData.bulletPrefab.UseLaser(firePoint, targetEnemy, (turretLvl + 1 + Random.Range(0, 2)) * damageModif);
        }
        else
        {
            lazer.SetUpTarget(targetEnemy);
            lazer.SetDamage((turretLvl * 2 + 1  + Random.Range(0, 2)) * damageModif);
        }
    }
    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretData.range);
    }*/

    public float GetTurretRange()
    {
        return turretData.range;
    }

    public int GetTurretDamage()
    {
        return turretLvl*2 + 1;
        //return turretData.bulletPrefab.damage;
    }

    public void SetDamageModif(float boost)
    {
        damageModif = boost;
    }

    public void SetSpeedModif(float boost)
    {
        speedModif =  boost - 1;


        //delays = turretData.fireRate - turretData.fireRate * boost;
    }
}
