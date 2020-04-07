using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour 
{
    //Change this list here to be from the player/character manager class instead
    public List<GenericCharacter> m_AllActionUsers;
    public List<GenericActionModel> m_AllAbilitiesList;

    private ActionController m_ActionController;

    //public GenericCharacter m_PlayerInfo;
    //public List<GenericCharacter> m_EnemyInfo;

    //Whos turn is it?
    public GenericCharacter m_CurrentActionUser;

    public GenericCharacter m_CurrentPlayer;

    private bool m_bIsPlayersTurn = false;
    private ushort m_TurnQueueIndex = 0;

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
