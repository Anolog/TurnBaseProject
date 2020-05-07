﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCharacterController : MonoBehaviour
{
    GenericCharacter m_CharacterStats;

    SpriteRenderer m_SpriteRenderer;

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

        GameManager.GetCombatManager.OnCharacterSelected(m_CharacterStats);
    }
}