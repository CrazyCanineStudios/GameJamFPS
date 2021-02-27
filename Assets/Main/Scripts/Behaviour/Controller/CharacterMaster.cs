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
    public string CurrentState { get; private set; }

    public bool CanMove { get; set; }

    public Transform projectileSpawnPos;

    [SerializeField] private float abilityEffectiveness = 1f;

    private CharacterAbility previousAbility;
    [SerializeField] private float effectivenessLoss = 0.1f;

    private void Start()
    {
        instance = this;
        Controller = GetComponent<CharacterController>();
        Health = GetComponent<EntityHealth>();
        Body = GetComponent<Rigidbody>();
        Capsule = GetComponent<CapsuleCollider>();
        CurrentState = "Grounded";
        CanMove = true;
    }

    public float RecieveAbilityEffectiveness(CharacterAbility curAbility)
    {
        if (previousAbility != null)
        {
            if (previousAbility == curAbility)
            {
                abilityEffectiveness -= effectivenessLoss;
            }
            else
            {
                abilityEffectiveness += effectivenessLoss;
            }
        }
        previousAbility = curAbility;
        abilityEffectiveness = Mathf.Clamp(abilityEffectiveness, 0.1f, 1f);
        return abilityEffectiveness;
    }
}
