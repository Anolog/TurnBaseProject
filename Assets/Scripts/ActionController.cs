using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController
{
    //Expose UseAction()
    // -> Have it look at the information from the action and do stuff based on it.



    //Used mostly for 1 on 1 actions
    public void PerformAction(ActionData.ACTION_LIST_ID aActionID, GenericCharacter aAttacker, GenericCharacter aDefender)
    {
        //Get attacker's action that they are using
        //Call the action's "use ability/action" function to determine the effects etc.
        //Get the data for that and apply the outcome to the attacker and defender
        //Make a function to calculate/perform a turn for the data/effects, whatever happens

        //GameManager.GetCombatManager-> ? This Function -> GetActionController()->IseAction
        //Have instance of action controller here....?

        Debug.Log("Performing 1 on 1 action");
        Debug.Log("Action name from dictionary: " + ActionData.ABILITY_DICTIONARY[aActionID].GetActionName());

        GenericActionModel action = ActionData.ABILITY_DICTIONARY[aActionID];

        //One function for

        //if does damage
        // damage()
        //if does healing
        // heal()
        //if does affix
        // affix()

        //Another function for

        //if does damage
        //loop through list
        // damage() each one
        //  etc..

    }

    public void PerformActionAll(ActionData.ACTION_LIST_ID aActionID, GenericCharacter aAttacker)
    {

    }

    public void PerformSelfAction(ActionData.ACTION_LIST_ID aActionID, GenericCharacter aAttacker)
    {

    }

    public void PerformMultiOffensiveAction(ActionData.ACTION_LIST_ID aActionID, GenericCharacter aAttacker)
    {

    }

    public void PerformMultiDefensiveAction(ActionData.ACTION_LIST_ID aActionID, GenericCharacter aDefender)
    {

    }


    /*
    virtual protected void UseAbility()
    {
        Debug.Log("Generic Ability Use Called!");

        if (m_bActionDealsDamage == true)
        {
            ActionDamage();
        }

        if (m_bActionHeals == true)
        {
            ActionHeal();
        }

        if (m_bActionHasAffix == true)
        {
            ActionAffix();
        }
    }
*/

    /*
    virtual protected void ActionDamage(GenericCharacter aCharacter)
    {
        Debug.Log("Genetic Action Damage Called");
    }

    virtual protected void ActionHeal(GenericCharacter aCharacter)
    {
        Debug.Log("Generic Action Heal Called");
    }

    virtual protected void ActionAffix(GenericCharacter aCharacter)
    {
        Debug.Log("Generic Action Affix Called");
    }
*/

    private void ApplyDamage(GenericCharacter aDamagerReceiver, ActionData.ACTION_LIST_ID aActionID)
    {
        Debug.Log("Action ID: " + aActionID + " is being used against Target: " + aDamagerReceiver.GetCharacterName());

        aDamagerReceiver.SetCharacterHealth(aDamagerReceiver.GetCharacterHealth() - ActionData.ABILITY_DICTIONARY[aActionID].GetDamageAmount());
    }

    private void ApplyHeal(GenericCharacter aHealReceiver, ActionData.ACTION_LIST_ID aActionID)
    {
        Debug.Log("Action ID: " + aActionID + " is being used on Target: " + aHealReceiver.GetCharacterName());

        aHealReceiver.SetCharacterHealth(aHealReceiver.GetCharacterHealth() + ActionData.ABILITY_DICTIONARY[aActionID].GetHealAmount());
    }

    private void ApplyAffix(GenericCharacter aAffixReceiver, ActionData.ACTION_LIST_ID aActionID)
    {
        Debug.Log("Action ID: " + aActionID + "is being used on Target: " + aAffixReceiver.GetCharacterName());
    }

    //Change the apply functions, to take a value and apply that value as the heal, damage, etc...
    //Make overrideable functions that calculate damage, healing, etc...
}
