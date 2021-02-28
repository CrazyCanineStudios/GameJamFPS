using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    private CharacterMaster player;
    [SerializeField] private Text healthDisplay = null;
    [SerializeField] private Text gunDisplay = null;
    [SerializeField] private Text meleeDisplay = null;
    [SerializeField] private Text grenadeDisplay = null;

    private void Update()
    {
        // If the player is available and the ref is not set, set the player ref, or update the UI if it is
        if (CharacterMaster.instance != null)
            if (player == null)
            {
                player = CharacterMaster.instance;
            }
            else
                UpdateUserInterface();
    }

    /// <summary>
    /// Update elements of the user interface with game data
    /// </summary>
    private void UpdateUserInterface()
    {
        healthDisplay.text = player.Health.currentHealth.ToString();
        gunDisplay.text = player.ShootAbility.abilityPower.ToString();
        meleeDisplay.text = player.MeleeAbility.abilityPower.ToString();
        grenadeDisplay.text = player.GrenadeAbility.abilityPower.ToString();
    }
}
