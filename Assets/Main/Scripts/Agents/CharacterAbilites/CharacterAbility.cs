using UnityEngine;
using System.Linq;

// Inspiration was taken from the way Corgi Engine handles their state and ability systems.
public class CharacterAbility : MonoBehaviour
{
    protected CharacterMaster _master;

    public bool allowAbility = true;
	public float abilityPower = 1.0f;

    public string[] BlockedStates;

    protected virtual void Start()
    {
        Initialisation();
    }
    protected virtual void Initialisation()
    {
        _master = this.gameObject.GetComponentInParent<CharacterMaster>();
    }
	public virtual bool AbilityAuthorized
	{
		get
		{
			if (_master != null)
			{
				if ((BlockedStates != null) && (BlockedStates.Length > 0))
				{
					if (BlockedStates.Contains(_master.CurrentState))
					{
						return false;
					}
				}
			}
			return allowAbility;
		}
	}
	protected virtual void OnAbilityStart()
    {

    }
	protected virtual void OnAbilityActive()
    {

    }
	protected virtual void OnAbilityEnd()
	{

	}
}
