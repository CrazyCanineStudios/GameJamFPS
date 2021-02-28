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
        // Update ability power for this weapon
        _master.ChangeAbilityPower(this);
        actualGunDamage = gunDamage * abilityPower;

        // Shoot gun
        Debug.Log("Shoot Gun");
        anim.SetTrigger("shot");
        Instantiate(explosionPrefab, gunMuzzle);
        GameObject bullet = Instantiate(bulletPrefab, gunMuzzle);
        bullet.transform.SetParent(null);

        // Drawn line cast, deal damage if hits enemy
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity);
        if (hit.collider.GetComponent<EntityHealth>())
        {
            Debug.Log("Hit Enemy");
            hit.collider.GetComponent<EntityHealth>().DealDamage(actualGunDamage);
        }

        // Update cooldown
        currentRechargeTime = rechargeTime;
    }
}
