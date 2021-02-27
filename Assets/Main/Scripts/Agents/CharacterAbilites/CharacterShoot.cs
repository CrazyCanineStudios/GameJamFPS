using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : CharacterAbility
{
    public float rechargeTime = 1.0f;
    private float currentRechargeTime;

    public float gunDamage = 50.0f;
    private float actualGunDamage;

    public Animator anim;

    [SerializeField] private Transform gunMuzzle = null;
    [SerializeField] private GameObject explosionPrefab = null;
    [SerializeField] private GameObject bulletPrefab = null;


    void Update()
    {
        if (!AbilityAuthorized) { return; }

        if (Input.GetButtonDown("Fire1") && currentRechargeTime <= 0)
        {
            OnAbilityStart();
        }
        else if (currentRechargeTime > 0)
        {
            // Decrease the rechargeTime.
            currentRechargeTime -= Time.deltaTime;
        }
    }

    protected override void OnAbilityStart()
    {
        actualGunDamage = gunDamage * _master.RecieveAbilityEffectiveness(this);

        Debug.Log("Shoot Gun");
        anim.SetTrigger("shot");
        GameObject explosion = Instantiate(explosionPrefab, gunMuzzle);
        GameObject bullet = Instantiate(bulletPrefab, gunMuzzle);
        bullet.transform.SetParent(null);

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
        if (hit.collider.GetComponent<EntityHealth>())
        {
            Debug.Log("Hit Enemy");
            hit.collider.GetComponent<EntityHealth>().DealDamage(actualGunDamage);
        }

        currentRechargeTime = rechargeTime;
    }
}
