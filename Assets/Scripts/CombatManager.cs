﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour 
{
    public List<GameObject> m_CharacterCombatList;

    //TODO: Deprecate all action users list
    //Change this list here to be from the player/character manager class instead
    public List<GenericCharacter> m_AllActionUsers;
    public List<GenericActionModel> m_AllAbilitiesList;
    public List<GenericAffixModel> m_AllAffixList;

    public Dictionary<GenericCharacter, List<GenericAffixModel>> m_ActionUsersWithAffixes;

    private ActionController m_ActionController;
    private ActionAffixController m_AffixController;

    private bool m_IsPlayerTurn = true;

    //TODO: Get rid of these 3 variables
    public GenericCharacter m_CurrentActionUser;
    public GenericCharacter m_CurrentPlayer;
    private ushort m_TurnQueueIndex = 0;

    private GenericCharacter m_CurrentSelectedCharacter;
    private ActionData.ACTION_LIST_ID m_CurrentSelectedAction = ActionData.ACTION_LIST_ID.NONE;

    //TODO: This is set to test so change this later on
    [SerializeField]
    public CombatInterfaceController m_CombatUIController;


    enum COMBAT_STATE
    {
        PLAYER_TURN,
        ENEMY_TURN
    }

    //Dunno if i need this....
    enum PLAYER_TURN_STATES
    {
        CHARACTER_SELECTED,
        SELECTING_TARGET,

    }

    private COMBAT_STATE m_CombatState = COMBAT_STATE.PLAYER_TURN;

	private void Awake()
	{
		//Init vars
        if (m_AllActionUsers == null)
        {
            m_AllActionUsers = new List<GenericCharacter>();
        }

        if (m_CharacterCombatList == null)
        {
            m_CharacterCombatList = new List<GameObject>();
        }

        if (m_AllAbilitiesList == null)
        {
            m_AllAbilitiesList = new List<GenericActionModel>();
        }

        if (m_ActionController == null)
        {
            m_ActionController = new ActionController();
        }

        if (m_AffixController == null)
        {
            m_AffixController = new ActionAffixController();
        }

        if (m_ActionUsersWithAffixes == null)
        {
            m_ActionUsersWithAffixes = new Dictionary<GenericCharacter, List<GenericAffixModel>>();
        }
	}
    
	void Start () 
    {
        ResetInitialActionUser();
	}
	
	void Update () 
    {

	}

    public GenericCharacter GetCurrentSelectedCharacter()
    {
        return m_CurrentSelectedCharacter;
    }

    public void SetCurrentSelectedCharacter(GenericCharacter aCharacter)
    {
        m_CurrentSelectedCharacter = aCharacter;
    }

    public void OnPlayerCombatBegin()
    {
        //Combat Interface -> Set Initial UI ? 
    }

    public void OnPlayerCombatEnd()
    {
        m_CombatState = COMBAT_STATE.ENEMY_TURN;

        OnEnemyCombatBegin();
    }

    public void OnEnemyCombatBegin()
    {
        //Combat Interface -> Hide specific UI

        //TODO: Clean this up
        for (int i = 0; i < m_CharacterCombatList.Count; i++)
        {
            if (m_CharacterCombatList[i].GetComponent<GenericCharacterController>().GetCharacterStats().IsPlayerControlled() == false)
            {
                Debug.Log("An AI character is selecting a move to use");

                m_CharacterCombatList[i].GetComponent<GenericCharacterController>().m_GenericAIComponent.DecideActionToUseAlgorithm();

                Debug.Log("AI is using the move ID: " + m_CharacterCombatList[i].GetComponent<GenericCharacterController>().m_GenericAIComponent.GetCurrentSelectedActionID());

                //Mother of god...
                m_ActionController.PerformAction(
                    m_CharacterCombatList[i].GetComponent<GenericCharacterController>().m_GenericAIComponent.CreatePerformActionDataModel(
                    m_CharacterCombatList[i].GetComponent<GenericCharacterController>().m_GenericAIComponent.GetCurrentSelectedActionID()));

            }
        }

    }

    public void OnEnemyCombatEnd()
    {
        m_CombatState = COMBAT_STATE.PLAYER_TURN;
    }

    public void ProcessAction(PerformActionDataModel aPerformActionDataModel)
    {
        m_ActionController.PerformAction(aPerformActionDataModel);

    }

    public void ProcessAffix()
    {
        //if combat state is player turn and character we are checking is == to player controlled

        foreach(var index in m_ActionUsersWithAffixes)
        {
            if (index.Key.IsPlayerControlled() && m_CombatState == COMBAT_STATE.ENEMY_TURN ||
                !index.Key.IsPlayerControlled() && m_CombatState == COMBAT_STATE.PLAYER_TURN)
            {
                for (int i = 0; i < index.Value.Count; i++)
                {
                    if (index.Value[i].GetAffixUses() > 0)
                    {
                        m_ActionController.PerformAction(index.Value[i], index.Key);
                    }
                }
            }
        }
    }

    //DEPRECATED. DO NOT USE
    /*
    public void AddAffixToCharacter(GenericAffixModel aAffix, GenericCharacter aCharacter)
    {
        if (m_ActionUsersWithAffixes.ContainsKey(aCharacter))
        {
            bool bDoesAffixExist = false;
            int indexFound = 0;

            for (int i = 0; i < m_ActionUsersWithAffixes[aCharacter].Count; i++)
            {
                if (m_ActionUsersWithAffixes[aCharacter][i].GetAffixID() == aAffix.GetAffixID())
                {
                    bDoesAffixExist = true;
                    indexFound = i;
                    break;
                }
            }

            if (bDoesAffixExist == true)
            {
                if (aAffix.GetIsStackable())
                {
                    m_ActionUsersWithAffixes[aCharacter][indexFound].AddStackAmount(1);
                }
                else
                {
                    m_ActionUsersWithAffixes[aCharacter][indexFound].RefreshAffix();
                }
            }
            else
            {
                m_ActionUsersWithAffixes[aCharacter].Add(aAffix);
            }
        }
        else
        {
            List<GenericAffixModel> affixModelList = new List<GenericAffixModel>();
            affixModelList.Add(aAffix);
            m_ActionUsersWithAffixes.Add(aCharacter, affixModelList);
        }
    }
    */

      
    //TODO: Get rid of this because it's not being used anymore
    //      Not doing a turnbased in order of whoever, it's now, you select them
    public void ResetInitialActionUser()
    {
        m_CurrentActionUser = null;
        m_TurnQueueIndex = 0;

        //Start by setting the player to be the main user
        for (int i = 0; i < m_AllActionUsers.Count; i++)
        {
            if (m_AllActionUsers[i].IsPlayerControlled())
            {
                m_CurrentActionUser = m_AllActionUsers[i];
                m_CurrentPlayer = m_AllActionUsers[i];
            }

            if (m_CurrentActionUser == null &&
                (i >= m_AllActionUsers.Count))
            {
                Debug.LogError("ERROR: There is no player controlled action user in the current combat");
                break;
            }
        }
    }

    //TODO: Deprecate
    private void UpdateNextCharacter()
    {
        m_TurnQueueIndex++;

        if (m_TurnQueueIndex > m_AllActionUsers.Count)
        {
            m_TurnQueueIndex = 0;
        }

        m_CurrentActionUser = m_AllActionUsers[m_TurnQueueIndex];

        if (m_CurrentActionUser.IsPlayerControlled() == true)
        {
            m_CurrentPlayer = m_CurrentActionUser;
        }
        else
        {
            m_CurrentPlayer = null;
        }
    }

    public void OnCharacterSelected(GenericCharacter aGenericCharacter)
    {
        if (m_CombatState == COMBAT_STATE.PLAYER_TURN)
        {
            //If it is the players turn, and we are selecting a player party member
            if (aGenericCharacter.IsPlayerControlled())
            {
                //Do we have something selected already and is it not equal to the new selection?
                if (m_CurrentSelectedCharacter != aGenericCharacter)
                {
                    //If we don't have a selected action and are just swapping characters
                    if (m_CurrentSelectedAction == ActionData.ACTION_LIST_ID.NONE)
                    {
                        m_CurrentSelectedCharacter = aGenericCharacter;

                        m_CombatUIController.GetInterfaceModel().UpdateListOfActions();
                    }
                    else
                    {
                        //If we have an action and users selected...
                        //Create perform action data and send it to be used
                        //If it's single target, do this, else, the rest will be automatically sorted
                        if (ActionData.ABILITY_DICTIONARY[m_CurrentSelectedAction].GetActionTargetAmount() == GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET)
                        {
                            PerformActionDataModel performActionDataModel = new PerformActionDataModel(m_CurrentSelectedAction, GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET,
                                                                                                       m_CurrentSelectedCharacter, aGenericCharacter);

                            m_ActionController.PerformAction(performActionDataModel);
                        }
                        else if (ActionData.ABILITY_DICTIONARY[m_CurrentSelectedAction].GetActionTargetAmount() == GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_DEFENSIVE)
                        {
                            PerformActionDataModel performActionDataModel = new PerformActionDataModel(m_CurrentSelectedAction, GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_DEFENSIVE,
                                                                                                       m_CurrentSelectedCharacter);
                            m_ActionController.PerformAction(performActionDataModel);
                        }
                        else
                        {
                            //Won't attack everyone on your team, perhaps change this?
                            if (ActionData.ABILITY_DICTIONARY[m_CurrentSelectedAction].GetActionTargetAmount() == GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_OFFENSIVE &&
                                !aGenericCharacter.IsPlayerControlled())
                            {
                                PerformActionDataModel performActionDataModel = new PerformActionDataModel(m_CurrentSelectedAction, ActionData.ABILITY_DICTIONARY[m_CurrentSelectedAction].GetActionTargetAmount(),
                                                                                                           m_CurrentSelectedCharacter);
                            }
                            else
                            {
                                Debug.Log("Can't offensive on teammates");
                            }
                        }


                        m_CurrentSelectedAction = ActionData.ACTION_LIST_ID.NONE;

                        //TODO: Clean this up
                        m_CombatUIController.SetAllUIInactive();
                        m_CombatUIController.SetEndTurnUIActive();
                        m_CombatUIController.SetMainSelectionState();

                    }
                }
            }
            //If it's the player turn, and we are selecting an enemy
            else
            {
                if (m_CurrentSelectedCharacter != null)
                {
                    if (m_CurrentSelectedCharacter != aGenericCharacter)
                    {
                        //If we don't have something selected
                        if (m_CurrentSelectedAction == ActionData.ACTION_LIST_ID.NONE)
                        {
                            //do nothing I guess...?
                        }
                        else
                        {
                            if (ActionData.ABILITY_DICTIONARY[m_CurrentSelectedAction].GetActionTargetAmount() == GenericActionModel.ACTION_TARGET_AMOUNT.SINGLE_TARGET)
                            {
                                PerformActionDataModel performActionDataModel = new PerformActionDataModel(m_CurrentSelectedAction, ActionData.ABILITY_DICTIONARY[m_CurrentSelectedAction].GetActionTargetAmount(),
                                                                                                           m_CurrentSelectedCharacter, aGenericCharacter);
                                m_ActionController.PerformAction(performActionDataModel);
                            }
                            else
                            {
                                if (ActionData.ABILITY_DICTIONARY[m_CurrentSelectedAction].GetActionTargetAmount() == GenericActionModel.ACTION_TARGET_AMOUNT.MULTI_TARGET_OFFENSIVE)
                                {
                                    PerformActionDataModel performActionDataModel = new PerformActionDataModel(m_CurrentSelectedAction, ActionData.ABILITY_DICTIONARY[m_CurrentSelectedAction].GetActionTargetAmount(),
                                                                                                               m_CurrentSelectedCharacter);

                                    m_ActionController.PerformAction(performActionDataModel);

                                }
                                else
                                {
                                    Debug.Log("Can't use defensive on enemies.");
                                }
                            }


                            m_CurrentSelectedAction = ActionData.ACTION_LIST_ID.NONE;

                            //TODO: Clean this up
                            m_CombatUIController.SetAllUIInactive();
                            m_CombatUIController.SetEndTurnUIActive();
                            m_CombatUIController.SetMainSelectionState();
                        }
                    }
                }
            }
        }
    }

    public void OnActionSelected(ActionData.ACTION_LIST_ID aActionID)
    {
        m_CurrentSelectedAction = aActionID;
    }

}
