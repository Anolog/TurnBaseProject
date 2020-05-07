using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCharacter
{
    protected bool m_IsPlayerControlled = false;
    protected bool m_bIsCharacterDead = false;

    protected string m_CharacterName = "";

    protected int m_CharacterHealth = 100;
    protected int m_CharacterMana = 0;
    protected bool m_CharacterHasMana = false;
    protected int m_CharacterTurnDamage = 0;

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

    //Temp for now, change to dictionary or something later
    protected List<ActionData.ACTION_LIST_ID> m_ActionList = new List<ActionData.ACTION_LIST_ID>();

    //List of affixes on the generic character
    protected List<ActionData.AFFIX_LIST_ID> m_AffixList = new List<ActionData.AFFIX_LIST_ID>();

    public void ListUsableActions()
    {
        //Go through the list of actions that this user can use and do something with them
        for (int i = 0; i < m_ActionList.Count; i++)
        {
            //Change this to get the name as well from wherever it is being stored
            Debug.Log("Action list ID: " + m_ActionList);
        }
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
}
