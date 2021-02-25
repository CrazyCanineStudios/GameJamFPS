using UnityEngine;

/// <summary>
/// The master component for the character used to get all core references.
/// </summary>
public class CharacterMaster : MonoBehaviour
{
    // Character Components.
    public CharacterController Controller { get; private set; }
    public Rigidbody Body { get; private set; }
    public CapsuleCollider Capsule { get; private set; }
    public string CurrentState { get; private set; }

    public bool CanMove { get; set; }

    public Transform projectileSpawnPos;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        Body = GetComponent<Rigidbody>();
        Capsule = GetComponent<CapsuleCollider>();
        CurrentState = "Grounded";
        CanMove = true;
    }
}
