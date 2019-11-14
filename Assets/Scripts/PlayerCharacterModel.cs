using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterModel : MonoBehaviour 
{
    private GenericCharacter m_CharacterStats;

    //Have a 'get determined player' type of function
    //I forgot what the comment above was for....

    private ActionData.ACTION_LIST_ID m_SelectedActionID = ActionData.ACTION_LIST_ID.NONE;

	// Use this for initialization
	void Start () 
    {
        //Change to who we are initializing it to here
        m_CharacterStats = new GenericCharacter();
        //Possibly have it take an ID for the character we want to initialize as. Have a way to look up the values for that ID
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    //GameManeger -> GetCombatManager() -> Do the action with the id that is listed
    //Have an "OnClick"/"OnSelected" function for the ability selection to callback to set the actionID here
    void TestCallActionFunction()
    {
        PerformActionDataModel performActionData = new PerformActionDataModel();
        //GameManager.GetCombatManager.ProcessAction();
        //GameManager.GetCombatManager.PerformAction(m_SelectedActionID, m_CharacterStats);
    }

    void OnCharacterSelected()
    {
        
    }

}
