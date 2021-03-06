﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class GameManager : MonoBehaviour
{
    //TODO: Move to utility class
    static public string MAIN_MENU_SCENE_NAME = "MainMenuScene";
    static public string COMBAT_SCENE_NAME = "CombatScene";
    static public string MAP_SCENE_NAME = "MapScene";
    static public string SHOP_SCENE_NAME = "ShopScene";
    static public string REST_SCENE_NAME = "RestScene";

    static private GameManager m_GameManager;
    static public GameManager GetGameManager { get { return m_GameManager; } }

    static private CombatManager m_CombatManager;
    static public CombatManager GetCombatManager { get { return m_CombatManager; } }

    static private PlayerManager m_PlayerManager;
    static public PlayerManager GetPlayerManager { get { return m_PlayerManager; } }

    private MapManager m_MapManager;

    public TestCombatInterface cbIntTest;
    public GameObject combatInterface;

    public GameObject m_Char1;
    public GameObject m_Char2;
    public GameObject m_Enemy1;

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
        if (m_GameManager == null)
        {
            m_GameManager = this;
        }

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

        // *************************************** //
        // **********Debug/Test Code************** //
        // *************************************** //
        //Test_AddAllSkillsToList();
	}

	private void Start()
	{
        //Test_JSON_Load();
    }

    // Update is called once per frame
    void Update () 
    {

    }


    //TODO: Move the onbuttonpressed functions to a manager for scene changing callbacks.

    public void OnCompletelyDeterminedButtonPressed(Scene aScene, LoadSceneMode aSceneLoadMode)
    {
        Debug.Log("Scene Loaded - Unsubscribing from event.");
        SceneManager.sceneLoaded -= OnCompletelyDeterminedButtonPressed;

        GameObject mapManager = GameObject.Find("MapManager");

        if (mapManager == null)
        {
            Debug.Log("Error: mapManager variable, trying to find/be MainMenuController is null!");
        }
        else
        {
            //TODO: Make this a static string
            string mapFilePath = "JSON/Test_JSON/Test_Map_1";
            mapManager.GetComponent<MapManager>().LoadJSONFromFilePathAndInitMapManager(mapFilePath);
        }
    }

    public void OnMapNodeSelected(Scene aScene, LoadSceneMode aSceneLoadMode)
    {
        Debug.Log("Scene Loaded - Unsubscribing from event");
        SceneManager.sceneLoaded -= OnMapNodeSelected;

        GameObject.Find("MapCamera").SetActive(false);


        //TODO: Move this functionality into it's own function somewhere within another class.
        //Call it something like " PrepareCombatScene() "

        GameManager.GetCombatManager.m_CharacterCombatList.Clear();

        

        /* TODO: Need a player JSON for the characters that are playable to be made.
         * Then just use the genericcharacter that is stored in the list to be set as the value on the game object
         
        for (int i = 0; i < GameManager.GetPlayerManager.GetCharacterList().Count; i++)
        {
            GameObject player = Instantiate(Resources.Load("Prefabs/GenericPlayer_Prefab")) as GameObject;

            GameManager.GetCombatManager.m_CharacterCombatList.Add(player);

        }

        */

    }

    public void Test_JSON_Load()
    {
        Test_Action_Proper_1 tap_1 = new Test_Action_Proper_1();
        tap_1.Init();
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.STRIKE, tap_1);

        Test_Action_Proper_2 tap_2 = new Test_Action_Proper_2();
        tap_2.Init();
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.HEAL_TARGET, tap_2);

        Test_Action_Proper_3 tap_3 = new Test_Action_Proper_3();
        tap_3.Init();
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.MULTI_STRIKE, tap_3);

        Test_Affix_Proper_1 tafp_1 = new Test_Affix_Proper_1();
        tafp_1.Init();
        ActionData.AFFIX_DICTIONARY.Add(ActionData.AFFIX_LIST_ID.DOT_BASIC, tafp_1);

        m_Char1.GetComponent<GenericCharacterController>().InitCharacterWithJSON("JSON/Test_JSON/Test_Player_1");

        m_CombatManager.SetCurrentSelectedCharacter(GameManager.GetPlayerManager.GetCharacterList()[0]);
        m_CombatManager.m_CombatUIController = combatInterface.GetComponent<CombatInterfaceController>();
        m_CombatManager.m_CombatUIController.GetInterfaceModel().UpdateListOfActions();

        m_CombatManager.m_CharacterCombatList.Add(m_Char1);

    }

    public void Test_Enemy_AI()
    {
        Test_Action_Proper_1 tap_1 = new Test_Action_Proper_1();
        tap_1.Init();
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.STRIKE, tap_1);

        Test_Action_Proper_2 tap_2 = new Test_Action_Proper_2();
        tap_2.Init();
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.HEAL_TARGET, tap_2);

        Test_Action_Proper_3 tap_3 = new Test_Action_Proper_3();
        tap_3.Init();
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.MULTI_STRIKE, tap_3);

        Test_Affix_Proper_1 tafp_1 = new Test_Affix_Proper_1();
        tafp_1.Init();
        ActionData.AFFIX_DICTIONARY.Add(ActionData.AFFIX_LIST_ID.DOT_BASIC, tafp_1);

        m_Char1.GetComponent<Test_Character_Proper_1>().Init();
        m_Char2.GetComponent<Test_Character_Proper_2>().Init();
        m_Enemy1.GetComponent<Test_Enemy_1>().Init();

        Test_Equipment1 testItem = new Test_Equipment1();
        testItem.Init();
        ItemData.ITEM_DICTIONARY.Add(ItemData.ITEM_ID.TEST_ITEM_WEAPON, testItem);
        m_PlayerManager.GetCharacterList()[0].AddEquipmentToCharacter(ItemData.ITEM_ID.TEST_ITEM_WEAPON);



        m_CombatManager.SetCurrentSelectedCharacter(GameManager.GetPlayerManager.GetCharacterList()[0]);
        m_CombatManager.m_CombatUIController = combatInterface.GetComponent<CombatInterfaceController>();
        m_CombatManager.m_CombatUIController.GetInterfaceModel().UpdateListOfActions();

        //Add them all to combat manager list
        //m_CombatManager.m_AllActionUsers.Add(GameManager.GetPlayerManager.GetCharacterList()[0]);
        //m_CombatManager.m_AllActionUsers.Add(GameManager.GetPlayerManager.GetCharacterList()[1]);
        //m_CombatManager.m_AllActionUsers.Add(m_Enemy1.GetComponent<Test_Enemy_1>().GetCharacterStats());

        m_CombatManager.m_CharacterCombatList.Add(m_Char1);
        m_CombatManager.m_CharacterCombatList.Add(m_Char2);
        m_CombatManager.m_CharacterCombatList.Add(m_Enemy1);

    }

    public void Test_CharacterWithUISwapping()
    {
        Test_Action_Proper_1 tap_1 = new Test_Action_Proper_1();
        tap_1.Init();
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.STRIKE, tap_1);

        Test_Action_Proper_2 tap_2 = new Test_Action_Proper_2();
        tap_2.Init();
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.HEAL_TARGET, tap_2);

        Test_Action_Proper_3 tap_3 = new Test_Action_Proper_3();
        tap_3.Init();
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.MULTI_STRIKE, tap_3);

        Test_Affix_Proper_1 tafp_1 = new Test_Affix_Proper_1();
        tafp_1.Init();
        ActionData.AFFIX_DICTIONARY.Add(ActionData.AFFIX_LIST_ID.DOT_BASIC, tafp_1);

        m_Char1.GetComponent<Test_Character_Proper_1>().Init();
        m_Char2.GetComponent<Test_Character_Proper_2>().Init();
        //cbIntTest.CreateCharacterInformationDisplay(GameManager.GetPlayerManager.GetCharacterList()[0]);
        //cbIntTest.CreateCharacterInformationDisplay(GameManager.GetPlayerManager.GetCharacterList()[1]);

        m_CombatManager.SetCurrentSelectedCharacter(GameManager.GetPlayerManager.GetCharacterList()[0]);
        m_CombatManager.m_CombatUIController = combatInterface.GetComponent<CombatInterfaceController>();
        m_CombatManager.m_CombatUIController.GetInterfaceModel().UpdateListOfActions();
        //combatInterface.GetComponent<CombatInterfaceController>().GetInterfaceModel().UpdateListOfActions();

        Test_ItemEquip();

    }

    public void Test_ItemEquip()
    {
        Test_Equipment1 testItem = new Test_Equipment1();
        testItem.Init();
        ItemData.ITEM_DICTIONARY.Add(ItemData.ITEM_ID.TEST_ITEM_WEAPON, testItem);

        m_PlayerManager.GetCharacterList()[0].AddEquipmentToCharacter(ItemData.ITEM_ID.TEST_ITEM_WEAPON);
    }

    public void Test_LogCharHealth()
    {
        Debug.Log("Name: " + GameManager.GetPlayerManager.GetCharacterList()[0].GetCharacterName() + " Health: " + GameManager.GetPlayerManager.GetCharacterList()[0].GetCharacterHealth());
        Debug.Log("Name: " + GameManager.GetPlayerManager.GetCharacterList()[1].GetCharacterName() + " Health: " + GameManager.GetPlayerManager.GetCharacterList()[1].GetCharacterHealth());

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

        //Self target
        TestAction1 testAction2 = new TestAction1();
        testAction2.SetInitPropertiesSelfTarget();

        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.SHIELD_ONE, testAction2);

        //Multi Target All
        TestActionMulti testActionOffensive = new TestActionMulti();
        testActionOffensive.SetInitPropertiesAll();

        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.ATTACK_ONE, testActionOffensive);


        //Multi Target Offensive
        TestActionMulti testActionMultiOffensive = new TestActionMulti();
        testActionMultiOffensive.SetInitPropertiesOffensive();

        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.ATTACK_TWO, testActionMultiOffensive);

        //Multi Target Defensive
        TestActionMulti testActionMultiDefensive = new TestActionMulti();
        testActionMultiDefensive.SetInitPropertiesDefensive();

        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.SHIELD_TWO, testActionMultiDefensive);

    }

    public void Test_CharacterInteraction1()
    {
        TestCharacter testCharacter = new TestCharacter();
        testCharacter.InitChar(true);
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

    public void Test_ListAllAffixesInDictionary()
    {
        foreach (KeyValuePair<ActionData.AFFIX_LIST_ID, GenericAffixModel> affixDataIndex in ActionData.AFFIX_DICTIONARY)
        {
            Debug.Log("Affix in dictionary with the name: " + affixDataIndex.Value.GetActionName());
        }
    }

    public void Test_AffixInteraction()
    {
        TestAffix1 testAffix1 = new TestAffix1();
        testAffix1.InitAffix();

        ActionData.AFFIX_DICTIONARY.Add(ActionData.AFFIX_LIST_ID.TEST_AFFIX_1, testAffix1);

        TestAction1 testAction1 = new TestAction1();
        testAction1.SetInitPropertiesSelfTarget();

        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.ATTACK_BASIC, testAction1);

        Test_ListAllAffixesInDictionary();

        TestCharacter testChar1 = new TestCharacter();
        testChar1.InitChar(true);

        TestCharacter testChar2 = new TestCharacter();
        testChar2.InitChar(false);
        testChar2.m_Character.SetCharacterName("Enemy Test");

        GetPlayerManager.AddCharacterToList(testChar1.m_Character);
        GetPlayerManager.AddCharacterToList(testChar2.m_Character);



        PerformActionDataModel testDataModel = new PerformActionDataModel(ActionData.ACTION_LIST_ID.ATTACK_BASIC, GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET, testChar1.m_Character, testChar2.m_Character);

        GetCombatManager.ProcessAction(testDataModel);

        for (int i = 0; i < GetPlayerManager.GetCharacterList().Count; i++)
        {
            Debug.Log("Health - " + GetPlayerManager.GetCharacterList()[i].GetCharacterName() + " : " + GetPlayerManager.GetCharacterList()[i].GetCharacterHealth());
        }

        GetCombatManager.ProcessAffix();

        Debug.Log("Affixes Processed");

        for (int i = 0; i < GetPlayerManager.GetCharacterList().Count; i++)
        {
            Debug.Log("Health - " + GetPlayerManager.GetCharacterList()[i].GetCharacterName() + " : " + GetPlayerManager.GetCharacterList()[i].GetCharacterHealth());
        }

        Debug.Log("Test Case Over");
    }

    public void Test_CharacterInteraction2()
    {
        TestCharacter testChar1 = new TestCharacter();
        testChar1.InitChar(true);

        TestCharacter testChar2 = new TestCharacter();
        testChar2.InitChar(false);
        testChar2.m_Character.SetCharacterName("Enemy Test");

        PerformActionDataModel testDataModel = new PerformActionDataModel(ActionData.ACTION_LIST_ID.ATTACK_BASIC, GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET, testChar1.m_Character, testChar2.m_Character);

        GetCombatManager.ProcessAction(testDataModel);
    }

    public void Test_ActionInteraction()
    {
        TestCharacter testChar1 = new TestCharacter();
        testChar1.InitChar(true);
        testChar1.m_Character.SetCharacterName("Player");

        TestCharacter testChar2 = new TestCharacter();
        testChar2.InitChar(false);

        TestCharacter testChar3 = new TestCharacter();
        testChar3.InitChar(false);

        GetPlayerManager.AddCharacterToList(testChar1.m_Character);
        GetPlayerManager.AddCharacterToList(testChar2.m_Character);
        GetPlayerManager.AddCharacterToList(testChar3.m_Character);

        PerformActionDataModel testActionSelfTarget = new PerformActionDataModel(ActionData.ACTION_LIST_ID.SHIELD_ONE, GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET, testChar1.m_Character);
        PerformActionDataModel testActionAllTarget = new PerformActionDataModel(ActionData.ACTION_LIST_ID.ATTACK_ONE, GenericActionModel.ACTION_TARGET_AMOUNT.ALL_TARGETS, testChar1.m_Character);
        PerformActionDataModel testActionMultiOffensive = new PerformActionDataModel(ActionData.ACTION_LIST_ID.ATTACK_TWO, GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_OFFENSIVE, testChar1.m_Character);
        PerformActionDataModel testActionMultiDefensive = new PerformActionDataModel(ActionData.ACTION_LIST_ID.SHIELD_TWO, GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_DEFENSIVE, testChar1.m_Character);

        GetCombatManager.ProcessAction(testActionSelfTarget);
        GetCombatManager.ProcessAction(testActionAllTarget);
        GetCombatManager.ProcessAction(testActionMultiOffensive);
        GetCombatManager.ProcessAction(testActionMultiDefensive);

        for (int i = 0; i < GetPlayerManager.GetCharacterList().Count; i++)
        {
            Debug.Log("Health - " + GetPlayerManager.GetCharacterList()[i].GetCharacterName() + " : " + GetPlayerManager.GetCharacterList()[i].GetCharacterHealth());
        }

        Debug.Log("Test Case Over");
    }

    public void Test_UIAndCharacterInteraction()
    {
        //Test_CharacterInteraction1();
        //Test_ListAllAbilitiesInDictionary();
        //Test_ActionInteraction();
        //Test_AffixInteraction();

        TestCharacter testChar = new TestCharacter();
        testChar.InitChar(true);

        TestCharacter testChar2 = new TestCharacter();
        testChar2.InitChar(false);


        TestAffix1 testAffix1 = new TestAffix1();
        testAffix1.InitAffix();

        ActionData.AFFIX_DICTIONARY.Add(ActionData.AFFIX_LIST_ID.TEST_AFFIX_1, testAffix1);

        TestAction1 testAction1 = new TestAction1();
        testAction1.SetInitPropertiesSelfTarget();

        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.ATTACK_BASIC, testAction1);

        TestAction1 testAction2 = new TestAction1();
        testAction2.SetInitPropertiesSelfTarget();
        testAction2.SetName("Action 2 Test");
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.ATTACK_ONE, testAction2);

        TestAction1 testAction3 = new TestAction1();
        testAction3.SetInitPropertiesSelfTarget();
        testAction3.SetName("Action 3 Test");
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.ATTACK_TWO, testAction3);

        TestAction1 testAction4 = new TestAction1();
        testAction4.SetInitPropertiesSelfTarget();
        testAction4.SetName("Action 4 Test");
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.ATTACK_THREE, testAction4);

        TestAction1 testAction5 = new TestAction1();
        testAction5.SetInitPropertiesSelfTarget();
        testAction5.SetName("Action 5 Test");
        ActionData.ABILITY_DICTIONARY.Add(ActionData.ACTION_LIST_ID.ATTACK_FOUR, testAction5);

        GetPlayerManager.AddCharacterToList(testChar.m_Character);
        GetPlayerManager.AddCharacterToList(testChar2.m_Character);

        m_CombatManager.m_CurrentPlayer = testChar.m_Character;

        //PerformActionDataModel testDataModel = new PerformActionDataModel(ActionData.ACTION_LIST_ID.ATTACK_BASIC, GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET, testChar.m_Character, testChar2.m_Character);

        //GetCombatManager.ProcessAction(testDataModel);

        cbIntTest.CreateCharacterInformationDisplay(testChar.m_Character);
        cbIntTest.CreateCharacterInformationDisplay(testChar2.m_Character);
    }
}
