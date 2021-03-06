﻿using System.Collections;
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
                    if (actionUser != actionDefender)
                    {
                        PerformActionSingle(action, actionUser, actionDefender);
                    }
                    else if (actionUser == actionDefender)
                    {
                        PerformSelfAction(action, actionUser);
                    }
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
    
    public void PerformAction(GenericAffixModel aAffix, GenericCharacter aCharacter)
    {
        Debug.Log("Performing Action - Affix Type");
        Debug.Log("Affix " + aAffix.GetActionName() + " being used on " + aCharacter.GetCharacterName());

        PerformSelfAction(aAffix, aCharacter);

        aAffix.AddToAffixUses(-1);

        if (aAffix.GetAffixUses() <= 0)
        {
            AffixDepleated(aAffix);
        }
    }

    //TODO: Get rid of affix
    public void AffixDepleated(GenericAffixModel aAffix)
    {
        //Delete the affix
        Debug.Log("Affix " + aAffix.GetActionName() + "Depleated - Deleting (Not actually at the moment).");
        
    }

    //Used mostly for 1 on 1 actions
    //TODO: Refactor the if statment in these functions and put it into a single function
    //TODO: Change this to use the perform action model for parameters
    public void PerformActionSingle(GenericActionModel aAction, GenericCharacter aAttacker, GenericCharacter aDefender)
    {
        Debug.Log("Performing 1 on 1 action");
        Debug.Log("Action name from dictionary: " + aAction.GetActionName());

        if (aAction.GetDoesActionDamage())
        {
            ApplyDamage(aDefender, aAttacker, aAction);
        }

        if (aAction.GetDoesActionHeal())
        {
            ApplyHeal(aDefender, aAttacker, aAction);
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
                ApplyDamage(genericCharacter, aAttacker, aAction);
                hitTracking++;
            }

            if (aAction.GetDoesActionHeal())
            {
                ApplyHeal(genericCharacter, aAttacker, aAction);
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

    public void PerformSelfAction(Action aAction, GenericCharacter aAttacker)
    {
        Debug.Log("Performing self action");
        Debug.Log("Action name from dictionary: " + aAction.GetActionName());

        if (aAction.GetDoesActionDamage())
        {
            ApplyDamage(aAttacker, aAttacker, aAction);
        }

        if (aAction.GetDoesActionHeal())
        {
            ApplyHeal(aAttacker, aAttacker, aAction);
        }

        if (aAction.GetDoesActionHaveAffix() && aAction.GetType() != typeof(GenericAffixModel))
        {
            List<ActionData.AFFIX_LIST_ID> affixList = ((GenericActionModel)aAction).GetListOfAffixes();

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
                    ApplyDamage(genericCharacter, aAttacker, aAction);
                    hitTracking++;
                }

                if (aAction.GetDoesActionHeal())
                {
                    ApplyHeal(genericCharacter, aAttacker, aAction);
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
                    ApplyDamage(genericCharacter, aDefender, aAction);
                    hitTracking++;
                }

                if (aAction.GetDoesActionHeal())
                {
                    ApplyHeal(genericCharacter, aDefender, aAction);
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

    private void ApplyDamage(GenericCharacter aDamagerReceiver, GenericCharacter aAttacker, Action aAction)
    {
        Debug.Log("Action: " + aAction.GetActionName() + " - is being used against Target: " + aDamagerReceiver.GetCharacterName());

        int actionDamageValue = 0;

        Debug.Log("DEBUG - Type: " + aAction.GetType());

        if (aAction.GetType().IsSubclassOf(typeof(GenericAffixModel)) ||
            aAction.GetType() == typeof(GenericAffixModel))
        {
            Debug.Log("Action is GenericAffixModel type, attempting to stack.");

            GenericAffixModel affixAction = (GenericAffixModel)aAction;
            if (affixAction.GetIsStackable())
            {
                Debug.Log("Affix is stackable. Multiplying damage by " + affixAction.GetStackAmount() + " .");
                actionDamageValue = affixAction.GetDamageAmount() * affixAction.GetStackAmount();
            }
        }
        else
        {
            actionDamageValue = ComputeAttackValue(aAttacker, aAction);
        }

        ProcessDamageTaken(aDamagerReceiver, actionDamageValue);
    }

    private void ApplyHeal(GenericCharacter aHealReceiver, GenericCharacter aAttacker, Action aAction)
    {
        Debug.Log("Action: " + aAction.GetActionName() + " - is being used on Target: " + aHealReceiver.GetCharacterName());

        int actionHealValue = 0;

        if (aAction.GetType() == typeof(GenericAffixModel))
        {
            Debug.Log("Action is GenericAffixModel type, attempting to stack.");

            GenericAffixModel affixAction = (GenericAffixModel)aAction;
            if (affixAction.GetIsStackable())
            {
                Debug.Log("Affix is stackable. Multiplying healing by " + affixAction.GetStackAmount() + " .");
                actionHealValue = affixAction.GetHealAmount() * affixAction.GetStackAmount();
            }
        }
        else
        {
            actionHealValue = ComputeHealValue(aAttacker, aAction);
        }

        ProcessHealingTaken(aHealReceiver, actionHealValue);
    }

    private void ApplyAffix(GenericCharacter aAffixReceiver, GenericAffixModel aAffix)
    {
        Debug.Log("Action: " + aAffix.GetActionName() + "is being used on Target: " + aAffixReceiver.GetCharacterName());

        Dictionary<GenericCharacter, List<GenericAffixModel>> actionUsersWithAffixes = GameManager.GetCombatManager.m_ActionUsersWithAffixes;

        if (actionUsersWithAffixes.ContainsKey(aAffixReceiver))
        {
            bool bDoesAffixExist = false;
            int indexFound = 0;

            for (int i = 0; i < actionUsersWithAffixes[aAffixReceiver].Count; i++)
            {
                if (actionUsersWithAffixes[aAffixReceiver][i].GetAffixID() == aAffix.GetAffixID())
                {
                    bDoesAffixExist = true;
                    indexFound = i;
                    break;
                }
            }

            if (bDoesAffixExist == true)
            {
                if (aAffix.GetIsStackable())
                {
                    actionUsersWithAffixes[aAffixReceiver][indexFound].AddStackAmount(1);
                }
                else
                {
                    actionUsersWithAffixes[aAffixReceiver][indexFound].RefreshAffix();
                }
            }
            else
            {
                actionUsersWithAffixes[aAffixReceiver].Add(aAffix);
            }
        }
        else
        {
            List<GenericAffixModel> affixModelList = new List<GenericAffixModel>();
            affixModelList.Add(aAffix);
            actionUsersWithAffixes.Add(aAffixReceiver, affixModelList);
        }
    }

    private int ComputeAttackValue(GenericCharacter aAttacker, Action aAction)
    {
        int value = 0;

        value = aAction.GetDamageAmount() + aAttacker.GetCharacterTotalStats()[(int)GenericCharacter.STAT_INDEX.STRENGTH];

        return value;
    }

    private int ComputeHealValue(GenericCharacter aAttacker, Action aAction)
    {
        int value = 0;

        value = aAction.GetHealAmount() + aAttacker.GetCharacterTotalStats()[(int)GenericCharacter.STAT_INDEX.SPELL_POWER];

        return value;
    }

    //TODO: Change this to actually work properly. Need to add a current health, and a max health type of variable in generic character
    private void ProcessDamageTaken(GenericCharacter aGenericCharacter, int aDamageValue)
    {
        int currentDamage = aDamageValue;

        aDamageValue -= aGenericCharacter.GetCharacterTotalStats()[(int)GenericCharacter.STAT_INDEX.SHIELD];

        if (aDamageValue < 0)
        {
            aDamageValue = 0;
        }

        int currentHP = aGenericCharacter.GetCharacterTotalStats()[(int)GenericCharacter.STAT_INDEX.HEALTH] -= aDamageValue;

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        aGenericCharacter.SetCurrentHealth(currentHP);
    }

    private void ProcessHealingTaken(GenericCharacter aGenericCharacter, int aHealingValue)
    {
        aGenericCharacter.SetCurrentHealth(aGenericCharacter.GetCharacterHealth() + aHealingValue);
    }
}
