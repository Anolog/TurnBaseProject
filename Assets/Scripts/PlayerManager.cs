using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make this a class to have all instead of more player specific???
public class PlayerManager : MonoBehaviour 
{
    //Why did I think this class needed to inherate from GenericCharacter? :AmIRetarded?:

    //Maybe eventually make this an array with a cap on it once those things are worked out.
    List<GenericCharacter> m_ListOfCharacters = new List<GenericCharacter>();
    
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
		
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

    public void RemoveCHaracterFromListByObject(GenericCharacter aCharacter)
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
