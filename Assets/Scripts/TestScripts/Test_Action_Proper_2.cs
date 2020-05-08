using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Action_Proper_2 : GenericActionModel
{
    public void Init()
    {
        m_ActionType = ACTION_TYPE.DEFENSIVE;
        m_TargetAmount = ACTION_TARGET_AMOUNT.SINGLE_TARGET;

        m_SpellAffix1 = ActionData.AFFIX_LIST_ID.NONE;
        m_SpellAffix2 = ActionData.AFFIX_LIST_ID.NONE;
        m_SpellAffix3 = ActionData.AFFIX_LIST_ID.NONE;

        m_ActionCost = 1;
        m_HealAmount = 20;
        m_ManaCostAmount = 5;
        m_bActionHeals = true;

        m_Name = "Heal Single";
    }
}
