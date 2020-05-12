﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCharacter
{
    protected bool m_IsPlayerControlled = false;
    protected bool m_bIsCharacterDead = false;

    protected string m_CharacterName = "";
    protected string m_SpriteFilePath = "";
    protected string m_SpriteFileName = "";

    protected int m_CharacterHealth = 100;
    protected int m_CharacterMana = 0;
    protected bool m_CharacterHasMana = false;
    protected int m_CharacterTurnDamage = 0;

    //TODO: Delete the stuff below, not being used anymore ? 
    //Amount of damage taken every every rate
    protected int m_CharacterRealTimeDamage = 0;
    protected static int REAL_TIME_DAMAGE_DEFAULT = 1;

    //How often health decreases in seconds
    protected float m_CharacterRealTimeDamageRate = 0;
    protected static float REAL_TIME_DAMAGE_RATE_DEFAULT = 1.0f;

    //Can change
    protected int m_MaxPlayerAction = 3;
    protected int m_CurrentPlayerAction = 0;
    protected int m_AmountOfAttacksThisTurn = 0;
    protected int m_AmountOfActionsThisTurn = 0;

    //List of actions character can use
    protected List<ActionData.ACTION_LIST_ID> m_ActionList = new List<ActionData.ACTION_LIST_ID>();

    //List of affixes on the generic character
    protected List<ActionData.AFFIX_LIST_ID> m_AffixList = new List<ActionData.AFFIX_LIST_ID>();

    //List of equipment
    protected Dictionary<ItemData.ITEM_TYPE, ItemData.ITEM_ID> m_Equipment = new Dictionary<ItemData.ITEM_TYPE, ItemData.ITEM_ID>()
    {
        { ItemData.ITEM_TYPE.CHEST,  ItemData.ITEM_ID.NONE  },
        { ItemData.ITEM_TYPE.HELM,   ItemData.ITEM_ID.NONE   },
        { ItemData.ITEM_TYPE.WEAPON, ItemData.ITEM_ID.NONE },
        { ItemData.ITEM_TYPE.RING,   ItemData.ITEM_ID.NONE   },
        { ItemData.ITEM_TYPE.RING,   ItemData.ITEM_ID.NONE   }
    };

    public void ListUsableActions()
    {
        //Go through the list of actions that this user can use and do something with them
        for (int i = 0; i < m_ActionList.Count; i++)
        {
            //Change this to get the name as well from wherever it is being stored
            Debug.Log("Action list ID: " + m_ActionList);
        }
    }

    public string GetSpriteFilePath()
    {
        return m_SpriteFilePath;
    }

    public string GetSpriteFileName()
    {
        return m_SpriteFileName;
    }

    //Mostly for debugging
    public void SetSpriteFilePath(string aFilePath)
    {
        m_SpriteFilePath = aFilePath;
    }

    //Mostly for debugging
    public void SetSpriteFileName(string aFileName)
    {
        m_SpriteFileName = aFileName;
    }

    public string GetCharacterName()
    {
        return m_CharacterName;
    }

    public void SetCharacterName(string aName)
    {
        m_CharacterName = aName;
    }

    public int GetCharacterHealth()
    {
        return m_CharacterHealth;
    }

    public void SetCharacterHealth(int aHealth)
    {
        m_CharacterHealth = aHealth;
    }

    public int GetCharacterMana()
    {
        return m_CharacterMana;
    }

    public void SetCharacterMana(int aMana)
    {
        if (GetCharacterHasMana())
        {
            m_CharacterMana = aMana;
        }
    }

    public bool GetCharacterHasMana()
    {
        return m_CharacterHasMana;
    }

    public bool IsPlayerControlled()
    {
        return m_IsPlayerControlled;
    }

    public void SetIsPlayerControlled(bool aIsPlayerControlled)
    {
        m_IsPlayerControlled = aIsPlayerControlled;
    }

    public List<ActionData.ACTION_LIST_ID> GetUsableActionIDList()
    {
        return m_ActionList;
    }

    public void AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID aActionID)
    {
        if (!DoesActionExistInList(aActionID))
        {
            m_ActionList.Add(aActionID);
        }
        //else, do something else possibly...?
    }

    public bool DoesActionExistInList(ActionData.ACTION_LIST_ID aActionID)
    {
        bool bExists = false;

        for (int i = 0; i < m_ActionList.Count; i++)
        {
            if (m_ActionList[i] == aActionID)
            {
                Debug.Log("Action ID " + aActionID + " already exists for this character: " + m_CharacterName);
                bExists = true;
                break;
            }
        }

        return bExists;
    }

    public void ClearActionList()
    {
        m_ActionList.Clear();
    }

    public bool DoesAffixIDExistOnCharacter(ActionData.AFFIX_LIST_ID aAffixID)
    {
        bool bExists = false;

        for (int i = 0; i < m_AffixList.Count; i++)
        {
            if (m_AffixList[i] == aAffixID)
            {
                Debug.Log("Action ID " + aAffixID + " is affecting this character: " + m_CharacterName);
                bExists = true;
            }
        }

        return bExists;
    }

    public void UseCurrentAction(ActionData.ACTION_LIST_ID aActionID)
    {
        
    }

    public List<ItemData.ITEM_ID> GetEquipmentIDList()
    {
        List<ItemData.ITEM_ID> equipment = new List<ItemData.ITEM_ID>();

        foreach(var index in m_Equipment)
        {
            equipment.Add(index.Value);
        }

        return equipment;
    }

    public bool IsEquipSlotFull(ItemData.ITEM_TYPE aItemType)
    {
        bool bIsSlotFull = false;

        if (aItemType != ItemData.ITEM_TYPE.RING)
        {
            if (m_Equipment[aItemType] != ItemData.ITEM_ID.NONE)
            {
                bIsSlotFull = true;
            }
        }
        else
        {
            int ringCount = 0;

            foreach (var index in m_Equipment)
            {
                if (index.Key == ItemData.ITEM_TYPE.RING &&
                    index.Value != ItemData.ITEM_ID.NONE)
                {
                    ringCount++;
                }
            }

            if (ringCount == 2)
            {
                bIsSlotFull = true;
            }
        }

        return bIsSlotFull;
    }

    public bool IsItemEquippedOnCharacter(ItemData.ITEM_ID aItemID)
    {
        bool bExists = false;

        foreach (var index in m_Equipment)
        {
            if (index.Value == aItemID)
            {
                bExists = true;
            }
        }

        return bExists;
    }

    public GenericItem GetEquippedItemByID(ItemData.ITEM_ID aItemID)
    {
        GenericItem item = new GenericItem();

        if (IsItemEquippedOnCharacter(aItemID))
        {
            item = ItemData.ITEM_DICTIONARY[aItemID];
        }

        return item;
    }

    public void AddEquipmentToCharacter(ItemData.ITEM_ID aItemID)
    {
        ItemData.ITEM_TYPE itemType = ItemData.ITEM_DICTIONARY[aItemID].GetItemType();

        if (IsEquipSlotFull(itemType) == false)
        {
            m_Equipment[itemType] = aItemID;
        }
        else
        {
            Debug.Log("Warning: aItemID: " + aItemID + " cannot be added, the slot is already filled");
        }
    }
}
