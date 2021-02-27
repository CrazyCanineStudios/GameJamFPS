using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;

public class CharacterMelee : CharacterAbility
{
    public float rechargeTime = 2f;
    private float currentRechargeTime;

    [SerializeField] private Vector3 meleeOffset = Vector3.zero;
    [SerializeField] private float meleeRadius = 3f;

    public float lungeTime = 0.3f;
    public float lungeForce = 500f;

    public Animator anim;

    public float meleeDamage = 50f;

    private float actualMeleeDamage;

    private float actualTime;
    private Vector3 direction;
    private bool isMeleeAttack;
    private List<GameObject> enemyBuffer;

    public PostProcessVolume postProcessVolume;

    private PostProcessProfile postProcess;
    private LensDistortion lens;

    protected override void Initialisation()
    {
        base.Initialisation();
        enemyBuffer = new List<GameObject>();

        // Set the post processing volume.
        postProcess = postProcessVolume.profile;
        lens = postProcess.GetSetting<LensDistortion>();
    }

    void Update()
    {
        if (!AbilityAuthorized) { return; }

        if (Input.GetButtonDown("Fire3") && currentRechargeTime <= 0)
        {
            OnAbilityStart();
        }
        else if (currentRechargeTime > 0)
        {
            // Decrease the rechargeTime.
            currentRechargeTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (isMeleeAttack && actualTime < lungeTime)
        {
            _master.Body.AddForce(direction * lungeForce);
            actualTime += Time.fixedDeltaTime;
            lens.intensity.value = Mathf.Lerp(-20, 0, actualTime / 0.4f);
            lens.scale.value = Mathf.Lerp(1.1f, 1, actualTime / 0.2f);           

            Collider[] targets = Physics.OverlapSphere(transform.position + meleeOffset, meleeRadius);
            for (int i = 0; i < targets.Length; i++)
            {
                // Apply damage if they have health. TODO Kenneth - EntityHealth not yet implemented.
                if (targets[i].gameObject != this.gameObject && targets[i].TryGetComponent(out EntityHealth target))
                {
                    if (enemyBuffer.Contains(targets[i].gameObject) == false)
                    {
                        target.DealDamage(actualMeleeDamage);
                        Debug.Log("Apply Damage to target");
                        // Add target to buffer
                        enemyBuffer.Add(targets[i].gameObject);
                    }
                }
            }
        }
        else if (isMeleeAttack && actualTime >= lungeTime)
        {
            enemyBuffer.Clear();
            isMeleeAttack = false;
        }
    }
    protected override void OnAbilityStart()
    {
        isMeleeAttack = true;
        anim.SetTrigger("melee");
        actualMeleeDamage = meleeDamage * _master.RecieveAbilityEffectiveness(this);

        Debug.Log("Melee Attack");
        // Get all targets inside the melee radius.
        direction = transform.forward;
        direction.y = 0;
        actualTime = 0;

        // Set the recharge time.
        currentRechargeTime = rechargeTime;
    }

    private void OnDrawGizmos()
    {
        if (currentRechargeTime <= 0)
        {
            // Draw the explosion radius for debugging
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + meleeOffset, meleeRadius);
        }
    }
}
