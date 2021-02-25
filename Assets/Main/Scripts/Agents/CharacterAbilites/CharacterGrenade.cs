using UnityEngine;

public class CharacterGrenade : CharacterAbility
{
    public GameObject grenadePrefab;
    public float rechargeTime = 2f;
    private float currentRechargeTime;
    private GameObject currentGrenade;

    void Update()
    {
        if (!AbilityAuthorized) { return; }

        if (Input.GetButtonDown("Fire2") && currentRechargeTime<=0)
        {
            OnAbilityStart();              
        }
        else if (currentRechargeTime>0)
        {
            // Decrease the rechargeTime.
            currentRechargeTime -= Time.deltaTime;
        }
    }

    protected override void OnAbilityStart()
    {
        Debug.Log("Throw Grenade");

        // Spawn a grenade.
        currentGrenade = Instantiate(grenadePrefab);

        // Try and get the grenade's collider.
        currentGrenade.TryGetComponent(out Collider grenadeCol);

        // Disable collision between player and grenade.
        Physics.IgnoreCollision(grenadeCol, _master.Capsule);

        // Move to the player's pos;
        currentGrenade.transform.position = _master.projectileSpawnPos.position;

        // Set the recharge time.
        currentRechargeTime = rechargeTime;

    }
}
