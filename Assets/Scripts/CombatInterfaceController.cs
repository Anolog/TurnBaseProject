using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatInterfaceController : MonoBehaviour
{
    CombatInterfaceActionModel m_Model;

    [SerializeField]
    private GameObject m_MainSelection;
    [SerializeField]
    private GameObject m_ActionSelection;
    [SerializeField]
    private GameObject m_ActionUpButton;
    [SerializeField]
    private GameObject m_ActionDownButton;

    [SerializeField]
    private Button[] m_ActionSelectionButtons;
    [SerializeField]
    private Button[] m_MainSelectionButtons;

    private int m_UserSelectedButton = -1;

	// Use this for initialization
	void Start ()
    {
		if (m_Model == null)
        {
            m_Model = new CombatInterfaceActionModel();
            SetAllUIInactive();
            SetMainSelectionState();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetAllUIInactive()
    {
        m_MainSelection.SetActive(false);
        m_ActionSelection.SetActive(false);
        m_ActionUpButton.SetActive(false);
        m_ActionDownButton.SetActive(false);
    }

    public void SetMainSelectionState()
    {
        m_MainSelection.SetActive(true);
    }

    public void SetActionSelection()
    {
        m_MainSelection.SetActive(false);
        m_ActionSelection.SetActive(true);
        if (GameManager.GetCombatManager.m_CurrentPlayer.GetUsableActionIDList().Count < 4)
        {
            m_ActionDownButton.SetActive(true);
            m_ActionUpButton.SetActive(true);
        }

        UpdateActionButtonVisuals(true);

        /*
        foreach (ActionData.ACTION_LIST_ID actionID in GameManager.GetCombatManager.m_CurrentActionUser.GetUsableActionIDList())
        {

        }
        */
    }

    public void OnAttackButtonPressed()
    {
        //This will call the attack action, simple and easy
        
    }

    public void OnDefendButtonPressed()
    {
        //This will call defend action simple and easy
    }

    public void OnAbilityButtonPressed()
    {
        //This will call to create the list of abilities that the character has, but only with doing so for the ones categorized as ability
        m_Model.UpdateListOfActions();
        SetActionSelection();
        //UpdateActionButtonVisuals(true);

    }

    public void OnSpellButtonPressed()
    {
        //This will call to create the list of abilities that the character has, but only with doing so for the ones categorized as spells

    }

    public void SetUserButtonSelected(int aButton)
    {
        m_UserSelectedButton = aButton;
    }

    public void OnActionSelected()
    {
        if (m_UserSelectedButton > -1 && m_UserSelectedButton < 5)
        {
            ActionData.ACTION_LIST_ID actionID = m_Model.GetListOfActions()[m_Model.GetCurrentIndex() + m_UserSelectedButton];

            Debug.Log("Action Selected with the ID: " + actionID + " Button No: " + m_UserSelectedButton + " With current list index: " + m_Model.GetCurrentIndex());


        }

        //m_ActionSelectionButtons[1];

        m_UserSelectedButton = -1;
    }

    public void OnUpButtonPressed()
    {
        //This will end horribly
        m_Model.SetCurrentIndex(m_Model.GetCurrentIndex() - 4);

        UpdateActionButtonVisuals(false);
    }

    public void OnDownButtonPressed()
    {
        //This will end really really bad as well probably...
        m_Model.SetCurrentIndex(m_Model.GetCurrentIndex() + 4);

        UpdateActionButtonVisuals(true);
    }

    public void UpdateActionButtonVisuals(bool bWasDownButtonPressed)
    {
        if (m_Model != null)
        {
            List<ActionData.ACTION_LIST_ID> actionList = m_Model.UpdateActionsToShow(bWasDownButtonPressed);

            for (int i = 0; i < m_ActionSelectionButtons.Length; i++)
            {
                if (m_ActionSelectionButtons[i] != null)
                {
                    if (i > actionList.Count - 1)
                    {
                        m_ActionSelectionButtons[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        m_ActionSelectionButtons[i].GetComponentInChildren<Text>().text = ActionData.ABILITY_DICTIONARY[actionList[i]].GetActionName();
                        m_ActionSelectionButtons[i].gameObject.SetActive(true);
                    }
                }
            }

            if (m_Model.GetCanDisplayNext())
            {
                m_ActionDownButton.SetActive(true);
            }
            else
            {
                m_ActionDownButton.SetActive(false);
            }

            if (m_Model.GetCanDisplayPrevious())
            {
                m_ActionUpButton.SetActive(true);
            }
            else
            {
                m_ActionUpButton.SetActive(false);
            }
        }
        else
        {
            //lol wut
        }
    }
}
