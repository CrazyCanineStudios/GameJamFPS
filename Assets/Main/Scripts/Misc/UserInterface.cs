using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    private CharacterMaster player;
    [SerializeField] private Text healthDisplay;
    [SerializeField] private Text gunDisplay;
    [SerializeField] private Text meleeDisplay;
    [SerializeField] private Text grenadeDisplay;

    private void Update()
    {
        if (CharacterMaster.instance != null)
            if (player == null)
                player = CharacterMaster.instance;
            else
                UpdateUserInterface();
    }

    private void UpdateUserInterface()
    {
        healthDisplay.text = player.Health.currentHealth.ToString();
        //gunDisplay.text = player.RecieveAbilityEffectiveness(player.GetComponent<CharacterShoot>()).ToString();
        //meleeDisplay.text = player.RecieveAbilityEffectiveness(player.GetComponent<CharacterMelee>()).ToString();
        //grenadeDisplay.text = player.RecieveAbilityEffectiveness(player.GetComponent<CharacterGrenade>()).ToString();
    }
}
