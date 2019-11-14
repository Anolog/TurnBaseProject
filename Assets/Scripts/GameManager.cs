using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    static private CombatManager m_CombatManager;
    static public CombatManager GetCombatManager { get { return m_CombatManager; } }

    static private PlayerManager m_PlayerManager;
    static public PlayerManager GetPlayerManager { get { return m_PlayerManager; } }

    //static private ActionData m_ActionData;
    //static public ActionData GetActionData { get { return m_ActionData; } }

    /*
    static public GameManager GetInstance()
    {
        return this;
    }
    */

	void Awake () 
    {
        DontDestroyOnLoad(this);

        m_CombatManager = GetComponent<CombatManager>();

        if (m_CombatManager == null)
        {
            m_CombatManager = gameObject.AddComponent<CombatManager>();
        }

        m_PlayerManager = GetComponent<PlayerManager>();

        if (m_PlayerManager == null)
        {
            m_PlayerManager = gameObject.AddComponent<PlayerManager>();
        }

        //Debug/Test Code
        Test_AddAllSkillsToList();
	}

	private void Start()
	{
        Test_CharacterInteraction1();
        Test_ListAllAbilitiesInDictionary();
	}

	// Update is called once per frame
	void Update () 
    {
		
	}

    public void Test_AddAllSkillsToList()
    {
        Debug.Log("Adding skill to the master list");
        //Need better way of doing this if possible...
        //Come into play possibly with factory and parameters to adjust for it
        TestAction1 testAction1 = new TestAction1();
        testAction1.SetInitProperties();

        //Maybe put checks around this master list?
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.ATTACK_BASIC, testAction1);
    }

    public void Test_CharacterInteraction1()
    {
        TestCharacter testCharacter = new TestCharacter();
        testCharacter.InitChar();
        GetPlayerManager.AddCharacterToList(testCharacter.m_Character);

        Debug.Log("Test Character made and added to the character list");
        Debug.Log(testCharacter.m_Character.GetCharacterName() + ": has health stat of " + testCharacter.m_Character.GetCharacterHealth());

        testCharacter.m_Character.ListUsableActions();
    }

    public void Test_ListAllAbilitiesInDictionary()
    {
        foreach (KeyValuePair<ActionData.ACTION_LIST_ID, GenericActionModel> actionDataIndex in ActionData.ABILITY_DICTIONARY)
        {
            Debug.Log("Action In Dictionary With The Name: " + actionDataIndex.Value.GetActionName());
        }
    }

    public void Test_CharacterInteraction2()
    {
        TestCharacter testChar1 = new TestCharacter();
        testChar1.InitChar();

        TestCharacter testChar2 = new TestCharacter();
        testChar2.InitChar();
        testChar2.m_Character.SetCharacterName("Enemy Test");

        PerformActionDataModel testDataModel = new PerformActionDataModel(ActionData.ACTION_LIST_ID.ATTACK_BASIC, GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET, testChar1.m_Character, testChar2.m_Character);

        GetCombatManager.ProcessAction(testDataModel);
    }
}
