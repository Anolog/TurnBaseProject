using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController
{
    //Expose UseAction()
    // -> Have it look at the information from the action and do stuff based on it.



    //Used mostly for 1 on 1 actions
    //TODO: Refactor the if statment in these functions and put it into a single function
    public void PerformAction(ActionData.ACTION_LIST_ID aActionID, GenericCharacter aAttacker, GenericCharacter aDefender)
    {
        Debug.Log("Performing 1 on 1 action");
        Debug.Log("Action name from dictionary: " + ActionData.ABILITY_DICTIONARY[aActionID].GetActionName());

        GenericActionModel action = ActionData.ABILITY_DICTIONARY[aActionID];

        if (action.GetDoesActionDamage())
        {
            ApplyDamage(aDefender, aActionID);
        }

        if (action.GetDoesActionHeal())
        {
            ApplyHeal(aAttacker, aActionID);
        }

        if (action.GetDoesActionHaveAffix())
        {
            ApplyAffix(aDefender, aActionID);
        }
    }

    public void PerformActionAll(ActionData.ACTION_LIST_ID aActionID, GenericCharacter aAttacker)
    {
        Debug.Log("Performing 1 on ALL action");
        Debug.Log("Action name from dictionary: " + ActionData.ABILITY_DICTIONARY[aActionID].GetActionName());

        int hitTracking = 0;

        GenericActionModel action = ActionData.ABILITY_DICTIONARY[aActionID];

        for (int i = 0; i < GameManager.GetPlayerManager.GetCharacterList().Count; i++)
        {
            GenericCharacter genericCharacter = GameManager.GetPlayerManager.GetCharacterList()[i];

            if (action.GetDoesActionDamage())
            {
                ApplyDamage(genericCharacter, aActionID);
                hitTracking++;
            }

            if (action.GetDoesActionHeal())
            {
                ApplyHeal(genericCharacter, aActionID);
                hitTracking++;
            }

            if (action.GetDoesActionHaveAffix())
            {
                ApplyAffix(genericCharacter, aActionID);
                hitTracking++;
            }
        }

        Debug.Log("Action hit " + hitTracking + " times");

    }

    public void PerformSelfAction(ActionData.ACTION_LIST_ID aActionID, GenericCharacter aAttacker)
    {
        Debug.Log("Performing self action");
        Debug.Log("Action name from dictionary: " + ActionData.ABILITY_DICTIONARY[aActionID].GetActionName());

        GenericActionModel action = ActionData.ABILITY_DICTIONARY[aActionID];

        if (action.GetDoesActionDamage())
        {
            ApplyDamage(aAttacker, aActionID);
        }

        if (action.GetDoesActionHeal())
        {
            ApplyHeal(aAttacker, aActionID);
        }

        if (action.GetDoesActionHaveAffix())
        {
            ApplyAffix(aAttacker, aActionID);
        }
    }

    public void PerformMultiOffensiveAction(ActionData.ACTION_LIST_ID aActionID, GenericCharacter aAttacker)
    {
        Debug.Log("Performing multi offensive action");
        Debug.Log("Action name from dictionary: " + ActionData.ABILITY_DICTIONARY[aActionID].GetActionName());

        int hitTracking = 0;

        GenericActionModel action = ActionData.ABILITY_DICTIONARY[aActionID];

        for (int i = 0; i < GameManager.GetPlayerManager.GetCharacterList().Count; i++)
        {
            GenericCharacter genericCharacter = GameManager.GetPlayerManager.GetCharacterList()[i];

            if (genericCharacter.IsPlayerControlled() == false)
            {
                if (action.GetDoesActionDamage())
                {
                    ApplyDamage(genericCharacter, aActionID);
                    hitTracking++;
                }

                if (action.GetDoesActionHeal())
                {
                    ApplyHeal(genericCharacter, aActionID);
                    hitTracking++;
                }

                if (action.GetDoesActionHaveAffix())
                {
                    ApplyAffix(genericCharacter, aActionID);
                    hitTracking++;
                }
            }
        }

        Debug.Log("Action hit " + hitTracking + " times");

    }

    public void PerformMultiDefensiveAction(ActionData.ACTION_LIST_ID aActionID, GenericCharacter aDefender)
    {
        Debug.Log("Performing multi defensive action");
        Debug.Log("Action name from dictionary: " + ActionData.ABILITY_DICTIONARY[aActionID].GetActionName());

        int hitTracking = 0;

        GenericActionModel action = ActionData.ABILITY_DICTIONARY[aActionID];

        for (int i = 0; i < GameManager.GetPlayerManager.GetCharacterList().Count; i++)
        {
            GenericCharacter genericCharacter = GameManager.GetPlayerManager.GetCharacterList()[i];

            if (genericCharacter.IsPlayerControlled() == true)
            {
                if (action.GetDoesActionDamage())
                {
                    ApplyDamage(genericCharacter, aActionID);
                    hitTracking++;
                }

                if (action.GetDoesActionHeal())
                {
                    ApplyHeal(genericCharacter, aActionID);
                    hitTracking++;
                }

                if (action.GetDoesActionHaveAffix())
                {
                    ApplyAffix(genericCharacter, aActionID);
                    hitTracking++;
                }
            }
        }

        Debug.Log("Action hit " + hitTracking + " times");
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

    //These will be redefined and/or changed later to encorperate the other stats from the player/action user
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
