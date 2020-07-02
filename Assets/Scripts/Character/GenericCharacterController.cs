using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GenericCharacterController : MonoBehaviour
{
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

    public void InitCharacterWithJSON(string aJSONFilePath)
    {
        TextAsset file = Resources.Load(aJSONFilePath) as TextAsset;
        JSONNode characterJSON = JSON.Parse(file.ToString());

        Debug.Log(characterJSON);

        if (m_CharacterStats == null)
        {
            m_CharacterStats = new GenericCharacter();
        }

        m_CharacterStats.SetIsPlayerControlled(characterJSON["isPlayerControlled"]);
        m_CharacterStats.SetIsCharacterDead(characterJSON["isCharacterDead"]);
        m_CharacterStats.SetCharacterHasMana(characterJSON["doesCharacterHaveMana"]);
        m_CharacterStats.SetCharacterName(characterJSON["characterName"]);
        m_CharacterStats.SetSpriteFilePath(characterJSON["spriteFilePath"]);
        m_CharacterStats.SetSpriteFileName(characterJSON["spriteName"]);
        m_CharacterStats.SetCharacterHealth(characterJSON["characterStats"]["maxHealth"]);
        m_CharacterStats.SetCurrentHealth(characterJSON["characterStats"]["currentHealth"]);
        m_CharacterStats.SetShield(characterJSON["characterStats"]["shield"]);
        m_CharacterStats.SetStrength(characterJSON["characterStats"]["strength"]);
        m_CharacterStats.SetSpellPower(characterJSON["characterStats"]["spellPower"]);

        if (characterJSON["actionListIDs"].IsArray)
        {
            for (int i = 0; i < characterJSON["actionListIDs"].AsArray.Count; i++)
            {
                //TODO: Check if ability is in the dictionary first before trying to add to character
                //NOTE: I might already be doing that in another function that gets called when I add it.             
                //What in the C# wizardy fuck is this
                ActionData.ACTION_LIST_ID action = (ActionData.ACTION_LIST_ID) System.Enum.Parse(typeof(ActionData.ACTION_LIST_ID), characterJSON["actionListIDs"][i]);

                m_CharacterStats.AddActionIDToUsableActionList(action);
            }
        }

        //Oh god here we go again with the magic
        m_CharacterStats.AddEquipmentToCharacter(
            (ItemData.ITEM_ID) System.Enum.Parse(typeof(ItemData.ITEM_ID), characterJSON["equipmentSlots"]["chest"])
            );
        m_CharacterStats.AddEquipmentToCharacter(
            (ItemData.ITEM_ID)System.Enum.Parse(typeof(ItemData.ITEM_ID), characterJSON["equipmentSlots"]["helm"])
             );
        m_CharacterStats.AddEquipmentToCharacter(
            (ItemData.ITEM_ID)System.Enum.Parse(typeof(ItemData.ITEM_ID), characterJSON["equipmentSlots"]["weapon"])
             );
        m_CharacterStats.AddEquipmentToCharacter(
        (ItemData.ITEM_ID)System.Enum.Parse(typeof(ItemData.ITEM_ID), characterJSON["equipmentSlots"]["ring"])
            );

        //Unload this somewhere....? at some point in time? Not sure where, but somewhere...

        string spritePath = m_CharacterStats.GetSpriteFilePath() + "/" + m_CharacterStats.GetSpriteFileName();
        m_SpriteRenderer.sprite = Resources.Load<Sprite>(spritePath);
        
        Resources.UnloadAsset(file);

        GameManager.GetPlayerManager.AddCharacterToList(m_CharacterStats);
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
