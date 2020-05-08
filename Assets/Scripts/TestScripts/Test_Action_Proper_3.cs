using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Action_Proper_3 : GenericActionModel
{
    public void Init()
    {
        m_ActionType = ACTION_TYPE.OFFENSIVE;
        m_TargetAmount = ACTION_TARGET_AMOUNT.MULTI_TARGET_OFFENSIVE;

        m_SpellAffix1 = ActionData.AFFIX_LIST_ID.NONE;
        m_SpellAffix2 = ActionData.AFFIX_LIST_ID.NONE;
        m_SpellAffix3 = ActionData.AFFIX_LIST_ID.NONE;

        m_ActionCost = 2;
        m_DamageAmount = 15;
        m_bActionDealsDamage = true;

        m_Name = "Multi Strike";
    }
}
