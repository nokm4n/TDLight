using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDamage : MonoBehaviour
{

    [SerializeField] private GameObject _explodeEffect;
    [SerializeField] private float explRadius = 4;
    [SerializeField] private float damage = .2f;
    [SerializeField] private float duration = 10f;
    [SerializeField] private float delay = 30f;

    private bool exploded = false;
    private bool canExplode = true;
    private GameObject explodeEffect;

   // public bool isActive = false;
    public bool bought = false;
    public int price = 100;

    protected void Damage(Enemy enemy, float amount)
    {
        if (enemy == null) return;

        if (enemy != null)
        {
            enemy.TakeDamage(amount * Time.deltaTime);
            //e.Slow(type.slowAmount);
        }
    }

    private void OnTriggerStay(Collider other)
    {
		if (!bought) return;

		if (exploded)
            if (other.TryGetComponent<Enemy>(out var e))
            {
                Damage(e, damage);
                Debug.Log(e.name);
                //exploded = true;
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!bought) return;
        if (!exploded && _explodeEffect != null && canExplode)
        {
            if (other.TryGetComponent<Enemy>(out var e))
            {
                explodeEffect = Instantiate(_explodeEffect, transform.position, _explodeEffect.transform.rotation);
                exploded = true;
                canExplode = false;
                StartCoroutine(ExplodeDuration());
            }
        }
    }

    IEnumerator ExplodeDuration()
    {
        yield return new WaitForSeconds(duration);
        exploded = false;
        Destroy(explodeEffect);
        StartCoroutine(ExplodeDelay());

    } 
    
    IEnumerator ExplodeDelay()
    {
        yield return new WaitForSeconds(delay);
        canExplode = true;
    }

    public void SetActiveAbility(bool active)
    {
        gameObject.SetActive(active);
    }
}
