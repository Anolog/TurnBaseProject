using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Action_Proper_1 : GenericActionModel
{
    public void Init()
    {
        m_ActionType = ACTION_TYPE.OFFENSIVE;
        m_TargetAmount = ACTION_TARGET_AMOUNT.SINGLE_TARGET;

        m_SpellAffix1 = ActionData.AFFIX_LIST_ID.DOT_BASIC;
        m_SpellAffix2 = ActionData.AFFIX_LIST_ID.NONE;
        m_SpellAffix3 = ActionData.AFFIX_LIST_ID.NONE;

        m_ActionCost = 1;
        m_DamageAmount = 10;
        m_bActionDealsDamage = true;

        m_Name = "Strike";
    }

}
