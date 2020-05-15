using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericActionModel : Action
{
    public enum ACTION_OFF_OR_DEF
    {
        OFFENSIVE,
        DEFENSIVE,

        NULL
    }

    public enum ACTION_TYPE
    {
        ABILITY,
        SPELL,
        DEFENSIVE,

        NULL
    }

    public enum ACTION_TARGET_AMOUNT
    {
        SINGLE_TARGET,
        MULTI_TARGET_OFFENSIVE,
        MULTI_TARGET_DEFENSIVE,
        ALL_TARGETS,

        NO_TARGETS
    }

    protected ActionData.AFFIX_LIST_ID m_SpellAffix1 = ActionData.AFFIX_LIST_ID.NONE;
    protected ActionData.AFFIX_LIST_ID m_SpellAffix2 = ActionData.AFFIX_LIST_ID.NONE;
    protected ActionData.AFFIX_LIST_ID m_SpellAffix3 = ActionData.AFFIX_LIST_ID.NONE;

    protected ACTION_OFF_OR_DEF m_ActionOffOrDef  = ACTION_OFF_OR_DEF.NULL;

    protected ACTION_TYPE m_ActionType = ACTION_TYPE.NULL;

    protected ACTION_TARGET_AMOUNT m_TargetAmount = ACTION_TARGET_AMOUNT.NO_TARGETS;

    protected int m_ManaCostAmount    = 0;

    public ACTION_TYPE GetActionType()
    {
        return m_ActionType;
    }

    public ACTION_OFF_OR_DEF GetActionOffOrDef()
    {
        return m_ActionOffOrDef;
    }

    public ACTION_TARGET_AMOUNT GetActionTargetAmount()
    {
        return m_TargetAmount;
    }

    //Used for lazy/non manual checking
    private bool GetIfActionHasAffix()
    {
        bool bHasAffix = false;

        if (m_SpellAffix1 != ActionData.AFFIX_LIST_ID.NONE ||
            m_SpellAffix2 != ActionData.AFFIX_LIST_ID.NONE ||
            m_SpellAffix3 != ActionData.AFFIX_LIST_ID.NONE)
        {
            bHasAffix = true;
        }

        return bHasAffix;
    }

    public List<ActionData.AFFIX_LIST_ID> GetListOfAffixes()
    {
        List<ActionData.AFFIX_LIST_ID> listOfAffixes = new List<ActionData.AFFIX_LIST_ID>();

        if (m_SpellAffix1 != ActionData.AFFIX_LIST_ID.NONE)
        {
            listOfAffixes.Add(m_SpellAffix1);
        }

        if (m_SpellAffix2 != ActionData.AFFIX_LIST_ID.NONE)
        {
            listOfAffixes.Add(m_SpellAffix2);
        }

        if (m_SpellAffix3 != ActionData.AFFIX_LIST_ID.NONE)
        {
            listOfAffixes.Add(m_SpellAffix3);
        }

        return listOfAffixes;
    }

}
