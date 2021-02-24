using UnityEngine;

public class CharacterGrenade : CharacterAbility
{

    void Update()
    {
        if (!AbilityAuthorized) { return; }

        if (Input.GetButtonDown("Fire2"))
        {
            OnAbilityStart();              
        }
        OnAbilityActive();
    }

    protected override void OnAbilityStart()
    {
        Debug.Log("FIRE!!");
    }
    protected override void OnAbilityActive()
    {

    }
    protected override void OnAbilityEnd()
    {

    }
}
