using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAIComponent : MonoBehaviour
{
    protected ActionData.ACTION_LIST_ID m_CurrentSelectedActionID = ActionData.ACTION_LIST_ID.NONE;
    protected ActionData.ACTION_LIST_ID m_PreviousSelectedActionID = ActionData.ACTION_LIST_ID.NONE;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public PerformActionDataModel CreatePerformActionDataModel(ActionData.ACTION_LIST_ID aActionID)
    {
        GenericActionModel actionModel = ActionData.ABILITY_DICTIONARY[aActionID];
        PerformActionDataModel actionDataModel = null;

        switch (actionModel.GetActionTargetAmount())
        {
            case GenericActionModel.ACTION_TARGET_AMOUNT.ALL_TARGETS:
                actionDataModel = new PerformActionDataModel(aActionID, GenericActionModel.ACTION_TARGET_AMOUNT.ALL_TARGETS, GetComponentInParent<GenericCharacterController>().GetCharacterStats());
                break;

            case GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_DEFENSIVE:
                actionDataModel = new PerformActionDataModel(aActionID, GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_DEFENSIVE, GetComponentInParent<GenericCharacterController>().GetCharacterStats());
                break;

            case GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_OFFENSIVE:
                actionDataModel = new PerformActionDataModel(aActionID, GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_OFFENSIVE, GetComponentInParent<GenericCharacterController>().GetCharacterStats());
                break;

            case GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET:
                GenericCharacter defender;

                if (actionModel.GetActionOffOrDef() == GenericActionModel.ACTION_OFF_OR_DEF.DEFENSIVE)
                {
                    defender = GetRandomNonPlayerCharacter();
                }
                else
                {
                    defender = GetRandomPlayerCharacter();
                }

                actionDataModel = new PerformActionDataModel(aActionID, GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET, GetComponentInParent<GenericCharacterController>().GetCharacterStats(), defender);
                break;

            case GenericActionModel.ACTION_TARGET_AMOUNT.NO_TARGETS:
            default:
                Debug.Log("ERROR: Tried to create an action model through the AI component but the target type is none or null.");
                break;
            
        }

        return actionDataModel;
    }

    private void DecideActionToUseAlgorithm()
    {
        m_CurrentSelectedActionID = ActionData.ACTION_LIST_ID.NONE;
    }

    public GenericCharacter GetRandomNonPlayerCharacter()
    {
        GenericCharacter character = null;
        bool bCharFound = false;

        while (bCharFound == false)
        {
            int randIndex = Random.Range(0, GameManager.GetCombatManager.m_AllActionUsers.Count);

            if (!GameManager.GetCombatManager.m_AllActionUsers[randIndex].IsPlayerControlled() &&
                GetComponentInParent<GenericCharacterController>().GetCharacterStats() != GameManager.GetCombatManager.m_AllActionUsers[randIndex])
            {
                bCharFound = true;

                character = GameManager.GetCombatManager.m_AllActionUsers[randIndex];
            }
        }

        return character;
    }

    public GenericCharacter GetRandomPlayerCharacter()
    {
        GenericCharacter character = null;
        bool bCharFound = false;

        while (bCharFound == false)
        {
            int randIndex = Random.Range(0, GameManager.GetCombatManager.m_AllActionUsers.Count);

            if (GameManager.GetCombatManager.m_AllActionUsers[randIndex].IsPlayerControlled() &&
                GetComponentInParent<GenericCharacterController>().GetCharacterStats() != GameManager.GetCombatManager.m_AllActionUsers[randIndex])
            {
                bCharFound = true;

                character = GameManager.GetCombatManager.m_AllActionUsers[randIndex];
            }
        }

        return character;
    }

}
