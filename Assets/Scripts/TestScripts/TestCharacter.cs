using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour 
{
    public GenericCharacter m_Character = new GenericCharacter();

	private void Start()
	{
	}

    public void InitChar()
    {
        if (m_Character != null)
        {
            m_Character.SetIsPlayerControlled(true);
            m_Character.SetCharacterHealth(150);
            m_Character.SetCharacterName("Test Name");
        }
        else
        {
            m_Character = new GenericCharacter();
            m_Character.SetIsPlayerControlled(true);
            m_Character.SetCharacterHealth(150);
            m_Character.SetCharacterName("Test Name");
        }

        AddInitialTestActions();
    }

    private void AddInitialTestActions()
    {
        m_Character.ClearActionList();
        m_Character.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.ATTACK_BASIC);

    }
}
