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

    private bool m_IsPlayerTurn = true;

    //TODO: Get rid of these 3 variables
    public GenericCharacter m_CurrentActionUser;
    public GenericCharacter m_CurrentPlayer;
    private ushort m_TurnQueueIndex = 0;

    private GenericCharacter m_CurrentSelectedCharacter;

    //TODO: This is set to test so change this later on
    [SerializeField]
    private CombatInterfaceController m_CombatUIController;

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

    public void OnPlayerCombatBegin()
    {
        //Combat Interface -> Set Initial UI ? 
    }

    public void OnPlayerCombatEnd()
    {
        m_CombatState = COMBAT_STATE.ENEMY_TURN;
    }

    public void OnEnemyCombatBegin()
    {
        //Combat Interface -> Hide specific UI
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
            if (aGenericCharacter.IsPlayerControlled())
            {
                if (m_CurrentSelectedCharacter == null)
                {
                    m_CurrentSelectedCharacter = aGenericCharacter;

                    m_CombatUIController.GetInterfaceModel().UpdateListOfActions();
                }
                else
                {
                    
                }
            }
            else
            {
                //Figure out what we wanna do here
            }
        }
    }
}
