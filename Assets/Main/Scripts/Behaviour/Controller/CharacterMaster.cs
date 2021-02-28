using UnityEngine;

/// <summary>
/// The master component for the character used to get all core references.
/// </summary>
public class CharacterMaster : MonoBehaviour
{
    public static CharacterMaster instance;
    // Character Components.
    public CharacterController Controller { get; private set; }

    public EntityHealth Health { get; private set; }
    public Rigidbody Body { get; private set; }
    public CapsuleCollider Capsule { get; private set; }
    public CharacterShoot ShootAbility { get; private set; }
    public CharacterMelee MeleeAbility { get; private set; }
    public CharacterGrenade GrenadeAbility { get; private set; }
    public string CurrentState { get; private set; }

    public bool CanMove { get; set; }

    public Transform projectileSpawnPos;

    //[SerializeField] private float abilityEffectiveness = 1f;

    private CharacterAbility previousAbility;
    [SerializeField] private float effectivenessLoss = 0.1f;

    private void Start()
    {
        instance = this;
        Controller = GetComponent<CharacterController>();
        Health = GetComponent<EntityHealth>();
        ShootAbility = GetComponent<CharacterShoot>();
        MeleeAbility = GetComponent<CharacterMelee>();
        GrenadeAbility = GetComponent<CharacterGrenade>();
        Body = GetComponent<Rigidbody>();
        Capsule = GetComponent<CapsuleCollider>();
        CurrentState = "Grounded";
        CanMove = true;
    }

    //public float RecieveAbilityEffectiveness(CharacterAbility curAbility)
    //{
    //    if (previousAbility != null)
    //    {
    //        if (previousAbility == curAbility)
    //        {
    //            abilityEffectiveness -= effectivenessLoss;
    //        }
    //        else
    //        {
    //            abilityEffectiveness += effectivenessLoss;
    //        }
    //    }
    //    previousAbility = curAbility;
    //    abilityEffectiveness = Mathf.Clamp(abilityEffectiveness, 0.1f, 1f);
    //    return abilityEffectiveness;
    //}

    /// <summary>
    /// Update the Ability Power of each ability, with context to the last used ability
    /// </summary>
    /// <param name="currentAbility"></param>
    public void ChangeAbilityPower(CharacterAbility currentAbility)
    {
        foreach (CharacterAbility a in GetComponents<CharacterAbility>())
        {
            // Check if this is the current ability
            if (a.GetType() == currentAbility.GetType())
            {
                // Decrease power
                a.abilityPower -= effectivenessLoss;
                a.abilityPower = Mathf.Clamp(a.abilityPower, 0.1f, 1f);
            }
            else
            {
                // Increase power
                a.abilityPower += effectivenessLoss;
                a.abilityPower = Mathf.Clamp(a.abilityPower, 0.1f, 1f);
            }
        }
    }
}
