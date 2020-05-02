using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatInterfaceActionModel
{
    private List<ActionData.ACTION_LIST_ID> m_ListOfActions = new List<ActionData.ACTION_LIST_ID>();
    private int m_CurrentListIndex;
    private const int ACTION_AMOUNT_TO_DISPLAY = 4;

    private bool m_CanDisplayNext = true;
    private bool m_CanDisplayPrev = false;

    public void UpdateListOfActions()
    {
        m_ListOfActions.Clear();
        m_ListOfActions = GameManager.GetCombatManager.m_CurrentPlayer.GetUsableActionIDList();
        m_CurrentListIndex = 0;
    }

    public List<ActionData.ACTION_LIST_ID> UpdateActionsToShow(bool bMoveForwardsInList)
    {
        List<ActionData.ACTION_LIST_ID> actionList = new List<ActionData.ACTION_LIST_ID>();
        int amountAdded = 0;

        for (int i = 0; amountAdded < ACTION_AMOUNT_TO_DISPLAY; i++)
        {
            if (m_ListOfActions.Count >= amountAdded + m_CurrentListIndex)
            {
                if (i + m_CurrentListIndex < 0)
                {
                    m_CurrentListIndex = 0;
                    break;
                }
                else if (i + m_CurrentListIndex > m_ListOfActions.Count - 1)
                {
                    break;
                }
                else
                {
                    actionList.Add(m_ListOfActions[i + m_CurrentListIndex]);
                    amountAdded++;
                }
            }
            else
            {
                if (i == 0)
                {
                    Debug.Log("No actions to display");
                    break;
                }
                else
                {
                    m_CurrentListIndex = m_ListOfActions.Count - amountAdded;
                }
            }
        }

        if (amountAdded < ACTION_AMOUNT_TO_DISPLAY)
        {
            m_CanDisplayNext = false;
        }

        if (amountAdded == ACTION_AMOUNT_TO_DISPLAY)
        {
            if (m_ListOfActions.Count - amountAdded < 0 && m_CurrentListIndex != 0)
            {
                m_CurrentListIndex = 0;
                m_CanDisplayNext = true;
                m_CanDisplayPrev = false;
            }
            else if (m_CurrentListIndex == 0)
            {
                m_CanDisplayNext = true;
                m_CanDisplayPrev = false;
            }
            else
            {
                m_CanDisplayNext = true;
                m_CanDisplayPrev = true;
            }
        }
        else
        {
            if (m_ListOfActions.Count - amountAdded >= ACTION_AMOUNT_TO_DISPLAY)
            {
                m_CanDisplayPrev = true;
                m_CanDisplayNext = false;
            }
        }

        return actionList;
    }

    public bool GetCanDisplayPrevious()
    {
        return m_CanDisplayPrev;
    }

    public bool GetCanDisplayNext()
    {
        return m_CanDisplayNext;
    }

    public void SetCurrentIndex(int aIndex)
    {
        m_CurrentListIndex = aIndex;
    }

    public int GetCurrentIndex()
    {
        return m_CurrentListIndex;
    }
}
