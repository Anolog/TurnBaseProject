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

    public ActionData.ACTION_LIST_ID GetCurrentSelectedActionID()
    {
        return m_CurrentSelectedActionID;
    }

    public ActionData.ACTION_LIST_ID GetPreviousSelectedActionID()
    {
        return m_PreviousSelectedActionID;
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

    public virtual void DecideActionToUseAlgorithm()
    {
        m_CurrentSelectedActionID = ActionData.ACTION_LIST_ID.NONE;
    }

    //TODO: Can probably clean these up and make a reference var to the component and/or gameobject
    public GenericCharacter GetRandomNonPlayerCharacter()
    {
        GenericCharacter character = null;
        bool bCharFound = false;

        while (bCharFound == false)
        {
            int randIndex = Random.Range(0, GameManager.GetCombatManager.m_CharacterCombatList.Count);

            if (!GameManager.GetCombatManager.m_CharacterCombatList[randIndex].GetComponent<GenericCharacterController>().GetCharacterStats().IsPlayerControlled() &&
                GetComponentInParent<GenericCharacterController>().GetCharacterStats() != GameManager.GetCombatManager.m_CharacterCombatList[randIndex].GetComponent<GenericCharacterController>().GetCharacterStats())
            {
                bCharFound = true;

                character = GameManager.GetCombatManager.m_CharacterCombatList[randIndex].GetComponent<GenericCharacterController>().GetCharacterStats();
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
            int randIndex = Random.Range(0, GameManager.GetCombatManager.m_CharacterCombatList.Count);

            if (GameManager.GetCombatManager.m_CharacterCombatList[randIndex].GetComponent<GenericCharacterController>().GetCharacterStats().IsPlayerControlled() &&
                GetComponentInParent<GenericCharacterController>().GetCharacterStats() != GameManager.GetCombatManager.m_CharacterCombatList[randIndex].GetComponent<GenericCharacterController>().GetCharacterStats())
            {
                bCharFound = true;

                character = GameManager.GetCombatManager.m_CharacterCombatList[randIndex].GetComponent<GenericCharacterController>().GetCharacterStats();
            }
        }

        return character;
    }

}
