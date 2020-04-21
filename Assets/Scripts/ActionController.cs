using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController
{
    //Expose UseAction()
    // -> Have it look at the information from the action and do stuff based on it.

    public void PerformAction(PerformActionDataModel aPerformActionDataModel)
    {
        Debug.Log("Performing Action");

        //TODO: Maybe make null checks around this
        GenericActionModel action = ActionData.ABILITY_DICTIONARY[aPerformActionDataModel.GetActionID()];
        GenericCharacter actionUser = aPerformActionDataModel.GetAttackerData();
        GenericCharacter actionDefender = aPerformActionDataModel.GetDefenderData();

        if (action != null)
        {
            switch (aPerformActionDataModel.GetTargetAmount())
            {
                case GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET:
                    PerformActionSingle(action, actionUser, actionDefender);
                    break;

                case GenericActionModel.ACTION_TARGET_AMOUNT.ALL_TARGETS:
                    PerformActionAll(action, actionUser);
                    break;

                case GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_OFFENSIVE:
                    PerformMultiOffensiveAction(action, actionUser);
                    break;

                case GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_DEFENSIVE:
                    PerformMultiDefensiveAction(action, actionUser);
                    break;

                default:
                    Debug.Log("Error: Action does not have a target amount type, or is set to none.");
                    break;
            }
        }
        else
        {
            Debug.Log("ERROR: Action is null/does not exist!");
        }
    }
    
    //TODO:
    //Perform action for affix
    
    //Used mostly for 1 on 1 actions
    //TODO: Refactor the if statment in these functions and put it into a single function
    //TODO: Change this to use the perform action model for parameters
    public void PerformActionSingle(GenericActionModel aAction, GenericCharacter aAttacker, GenericCharacter aDefender)
    {
        Debug.Log("Performing 1 on 1 action");
        Debug.Log("Action name from dictionary: " + aAction.GetActionName());

        if (aAction.GetDoesActionDamage())
        {
            ApplyDamage(aDefender, aAction);
        }

        if (aAction.GetDoesActionHeal())
        {
            ApplyHeal(aAttacker, aAction);
        }

        if (aAction.GetDoesActionHaveAffix())
        {
            List<ActionData.AFFIX_LIST_ID> affixList = aAction.GetListOfAffixes();

            if (affixList.Count != 0)
            {
                for (int i = 0; i < affixList.Count; i++)
                {
                    ApplyAffix(aDefender, ActionData.AFFIX_DICTIONARY[affixList[i]]);
                }
            }
        }
    }

    public void PerformActionAll(GenericActionModel aAction, GenericCharacter aAttacker)
    {
        Debug.Log("Performing 1 on ALL action");
        Debug.Log("Action name from dictionary: " + aAction.GetActionName());

        int hitTracking = 0;

        for (int i = 0; i < GameManager.GetPlayerManager.GetCharacterList().Count; i++)
        {
            GenericCharacter genericCharacter = GameManager.GetPlayerManager.GetCharacterList()[i];

            if (aAction.GetDoesActionDamage())
            {
                ApplyDamage(genericCharacter, aAction);
                hitTracking++;
            }

            if (aAction.GetDoesActionHeal())
            {
                ApplyHeal(genericCharacter, aAction);
                hitTracking++;
            }

            if (aAction.GetDoesActionHaveAffix())
            {
                List<ActionData.AFFIX_LIST_ID> affixList = aAction.GetListOfAffixes();

                if (affixList.Count != 0)
                {
                    for (int j = 0; j < affixList.Count; j++)
                    {
                        ApplyAffix(genericCharacter, ActionData.AFFIX_DICTIONARY[affixList[j]]);
                    }
                }
            }
        }

        Debug.Log("Action hit " + hitTracking + " times");
    }

    public void PerformSelfAction(GenericActionModel aAction, GenericCharacter aAttacker)
    {
        Debug.Log("Performing self action");
        Debug.Log("Action name from dictionary: " + aAction.GetActionName());

        if (aAction.GetDoesActionDamage())
        {
            ApplyDamage(aAttacker, aAction);
        }

        if (aAction.GetDoesActionHeal())
        {
            ApplyHeal(aAttacker, aAction);
        }

        if (aAction.GetDoesActionHaveAffix())
        {
            List<ActionData.AFFIX_LIST_ID> affixList = aAction.GetListOfAffixes();

            if (affixList.Count != 0)
            {
                for (int j = 0; j < affixList.Count; j++)
                {
                    ApplyAffix(aAttacker, ActionData.AFFIX_DICTIONARY[affixList[j]]);
                }
            }
        }
    }

    public void PerformMultiOffensiveAction(GenericActionModel aAction, GenericCharacter aAttacker)
    {
        Debug.Log("Performing multi offensive action");
        Debug.Log("Action name from dictionary: " + aAction.GetActionName());

        int hitTracking = 0;

        for (int i = 0; i < GameManager.GetPlayerManager.GetCharacterList().Count; i++)
        {
            GenericCharacter genericCharacter = GameManager.GetPlayerManager.GetCharacterList()[i];

            if (genericCharacter.IsPlayerControlled() == false)
            {
                if (aAction.GetDoesActionDamage())
                {
                    ApplyDamage(genericCharacter, aAction);
                    hitTracking++;
                }

                if (aAction.GetDoesActionHeal())
                {
                    ApplyHeal(genericCharacter, aAction);
                    hitTracking++;
                }

                if (aAction.GetDoesActionHaveAffix())
                {
                    List<ActionData.AFFIX_LIST_ID> affixList = aAction.GetListOfAffixes();

                    if (affixList.Count != 0)
                    {
                        for (int j = 0; j < affixList.Count; j++)
                        {
                            ApplyAffix(genericCharacter, ActionData.AFFIX_DICTIONARY[affixList[j]]);
                        }
                    }
                }
            }
        }

        Debug.Log("Action hit " + hitTracking + " times");

    }

    public void PerformMultiDefensiveAction(GenericActionModel aAction, GenericCharacter aDefender)
    {
        Debug.Log("Performing multi defensive action");
        Debug.Log("Action name from dictionary: " + aAction.GetActionName());

        int hitTracking = 0;

        for (int i = 0; i < GameManager.GetPlayerManager.GetCharacterList().Count; i++)
        {
            GenericCharacter genericCharacter = GameManager.GetPlayerManager.GetCharacterList()[i];

            if (genericCharacter.IsPlayerControlled() == true)
            {
                if (aAction.GetDoesActionDamage())
                {
                    ApplyDamage(genericCharacter, aAction);
                    hitTracking++;
                }

                if (aAction.GetDoesActionHeal())
                {
                    ApplyHeal(genericCharacter, aAction);
                    hitTracking++;
                }

                if (aAction.GetDoesActionHaveAffix())
                {
                    List<ActionData.AFFIX_LIST_ID> affixList = aAction.GetListOfAffixes();

                    if (affixList.Count != 0)
                    {
                        for (int j = 0; j < affixList.Count; j++)
                        {
                            ApplyAffix(genericCharacter, ActionData.AFFIX_DICTIONARY[affixList[j]]);
                        }
                    }
                }
            }
        }

        Debug.Log("Action hit " + hitTracking + " times");
    }

    //These will be redefined and/or changed later to encorperate the other stats from the player/action user
    private void ApplyDamage(GenericCharacter aDamagerReceiver, GenericActionModel aAction)
    {
        Debug.Log("Action: " + aAction.GetActionName() + " - is being used against Target: " + aDamagerReceiver.GetCharacterName());

        aDamagerReceiver.SetCharacterHealth(aDamagerReceiver.GetCharacterHealth() - aAction.GetDamageAmount());
    }

    private void ApplyHeal(GenericCharacter aHealReceiver, GenericActionModel aAction)
    {
        Debug.Log("Action: " + aAction.GetActionName() + " - is being used on Target: " + aHealReceiver.GetCharacterName());

        aHealReceiver.SetCharacterHealth(aHealReceiver.GetCharacterHealth() + aAction.GetHealAmount());
    }

    private void ApplyAffix(GenericCharacter aAffixReceiver, GenericAffixModel aAffix)
    {
        Debug.Log("Action: " + aAffix.GetActionName() + "is being used on Target: " + aAffixReceiver.GetCharacterName());

    }

    //Change the apply functions, to take a value and apply that value as the heal, damage, etc...
    //Make overrideable functions that calculate damage, healing, etc...
}
