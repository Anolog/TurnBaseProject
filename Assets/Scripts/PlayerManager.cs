using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to globally store the characters throughout the game, then the list in here is passed into the combat manager to initialize it there
public class PlayerManager : MonoBehaviour 
{
    //Maybe eventually make this an array with a cap on it once those things are worked out.
    //TODO:
    //Have this store the characters at all times it will be the main reference
    //Then have it passed into the combat manager to have that work in there.
    List<GenericCharacter> m_ListOfCharacters = new List<GenericCharacter>();

    private static int DEFAULT_USE_CAPACITY = 4;

    //What we currently have
    private int m_CurrentActionUses = 0;

    //The capacity of it
    private int m_CurrentActionUseCapacity = DEFAULT_USE_CAPACITY;
    
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void ResetCurrentActionUses()
    {
        m_CurrentActionUses = m_CurrentActionUseCapacity;
    }

    public void SetCurrentActionUses(int aCount)
    {
        m_CurrentActionUses = aCount;
    }

    public int GetCurrentActionUses()
    {
        return m_CurrentActionUses;
    }

    public void AddToCurrentActionUses(int aAmountToAdd)
    {
        m_CurrentActionUses += aAmountToAdd;
    }

    public void SubtractToCurrentActionUses(int aAmountToSubtract)
    {
        m_CurrentActionUses -= aAmountToSubtract;
    }

    public List<GenericCharacter> GetCharacterList()
    {
        return m_ListOfCharacters;
    }

    public void AddCharacterToList(GenericCharacter aCharacter)
    {
        m_ListOfCharacters.Add(aCharacter);
    }

    private void CharacterDoesNotExistDebugLog(bool aDoesCharacterExist)
    {
        if (aDoesCharacterExist == false)
        {
            Debug.LogWarning("Debug Warning - The character being searched for, does not exist in the character list!");
        }
    }

    public void RemoveCharacterFromListByName(string aNameOfCharacter)
    {
        bool bDoesNameExistInList = false;

        //Multiple characters with the same name is possible...  need to add extra ID counter on the end of the string
        //to prevent this when making them
        for (int i = 0; i < m_ListOfCharacters.Count; i++)
        {
            if (m_ListOfCharacters[i].GetCharacterName() == aNameOfCharacter)
            {
                m_ListOfCharacters.RemoveAt(i);
                bDoesNameExistInList = true;
            }
        }

        CharacterDoesNotExistDebugLog(bDoesNameExistInList);
    }

    public void RemoveCharacterFromListByObject(GenericCharacter aCharacter)
    {
        bool bDoesCharacterExist = false;

        for (int i = 0; i < m_ListOfCharacters.Count; i++)
        {
            if (m_ListOfCharacters[i] == aCharacter)
            {
                m_ListOfCharacters.RemoveAt(i);
                bDoesCharacterExist = true;
            }
        }

        CharacterDoesNotExistDebugLog(bDoesCharacterExist);
    }

}
