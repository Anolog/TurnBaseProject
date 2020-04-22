using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour 
{
    //Change this list here to be from the player/character manager class instead
    public List<GenericCharacter> m_AllActionUsers;
    public List<GenericActionModel> m_AllAbilitiesList;
    public List<GenericAffixModel> m_AllAffixList;

    public Dictionary<GenericCharacter, List<GenericAffixModel>> m_ActionUsersWithAffixes;

    private ActionController m_ActionController;
    private ActionAffixController m_AffixController;

    //Whos turn is it?
    public GenericCharacter m_CurrentActionUser;

    public GenericCharacter m_CurrentPlayer;

    private bool m_bIsPlayersTurn = false;
    private ushort m_TurnQueueIndex = 0;

    enum COMBAT_STATE
    {
        PLAYER_TURN,
        ENEMY_TURN
    }

    private COMBAT_STATE m_CombatState = COMBAT_STATE.PLAYER_TURN;

	private void Awake()
	{
		//Init vars
        if (m_AllActionUsers == null)
        {
            m_AllActionUsers = new List<GenericCharacter>();
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

	// Use this for initialization
	void Start () 
    {
        ResetInitialActionUser();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Maybe switch to tag check instead to see if less taxing
        //Maybe move this into the update turn index? Or after it?
		if (m_CurrentActionUser == m_CurrentPlayer)
        {
            m_bIsPlayersTurn = true;
        }
	}

    public void ProcessAction(PerformActionDataModel aPerformActionDataModel)
    {
        m_ActionController.PerformAction(aPerformActionDataModel);
        
    }

    public void ProcessAffix()
    {
        //TODO: Create algorithm for processing affixes
        //Who's turn is ending, etc... User side / Enemy side
        //Get the side, look through that side's player list and do the affix
        //Make another function for looking through to see who needs an affix done to them, then call this one after?
        //Create the way to process affixes for the end of the turn

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
}
