using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCharacterController : MonoBehaviour
{
    //TODO:
    //Create JSON Utility function to call FromJSON to create the character
    //https://docs.unity3d.com/ScriptReference/JsonUtility.FromJson.html

    GenericCharacter m_CharacterStats;

    SpriteRenderer m_SpriteRenderer;

    //TODO: Fix this later
    public GenericAIComponent m_GenericAIComponent;

    //Set the generic AI component with JSON by using a string in the JSON to create the object type
    //Then set it to the generic so it will just use it

	// Use this for initialization
	void Start ()
    {
		if (m_SpriteRenderer == null)
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetCharacterStats(GenericCharacter aGenericCharacter)
    {
        m_CharacterStats = aGenericCharacter;
    }

    public GenericCharacter GetCharacterStats()
    {
        return m_CharacterStats;
    }

    public void OnCharacterSelected()
    {
        //Return the character stats?
        //Change the sprite to resemble it was selected if there isn't already a selected target?
    }

    public void OnMouseDown()
    {
        Debug.Log("DEBUG - OBJECT WAS CLICKED: " + this.name);

        if (m_CharacterStats != null)
        {
            GameManager.GetCombatManager.OnCharacterSelected(m_CharacterStats);

            if (m_CharacterStats.IsPlayerControlled())
            {
                GameManager.GetCombatManager.m_CombatUIController.UpdateActionButtonVisuals(true);
            }
        }
    }
}
