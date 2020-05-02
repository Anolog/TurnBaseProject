using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour 
{
    public GenericCharacter m_Character = new GenericCharacter();

	private void Start()
	{
	}

    public void InitChar(bool aIsPlayerControlled)
    {
        if (m_Character != null)
        {
            m_Character.SetIsPlayerControlled(aIsPlayerControlled);
            m_Character.SetCharacterHealth(150);
            m_Character.SetCharacterName(TestRandomNameGen.GenerateName(Random.Range(1, 10)));
        }
        else
        {
            m_Character = new GenericCharacter();
            m_Character.SetIsPlayerControlled(aIsPlayerControlled);
            m_Character.SetCharacterHealth(150);
            m_Character.SetCharacterName(TestRandomNameGen.GenerateName(Random.Range(1, 10)));
        }

        AddInitialTestActions();
    }

    private void AddInitialTestActions()
    {
        m_Character.ClearActionList();
        m_Character.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.ATTACK_BASIC);
        m_Character.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.ATTACK_ONE);
        m_Character.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.ATTACK_TWO);
        m_Character.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.ATTACK_THREE);
        m_Character.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.ATTACK_FOUR);
        //m_Character.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.SHIELD_ONE);
        //m_Character.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.SHIELD_TWO);

    }
}
