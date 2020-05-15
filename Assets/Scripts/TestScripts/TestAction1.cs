using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction1 : GenericActionModel 
{
    public void SetInitProperties()
    {
        m_ActionOffOrDef = ACTION_OFF_OR_DEF.OFFENSIVE;

        m_DamageAmount = 10;
        m_Name = "Test Action, Basic";

        m_bActionDealsDamage = true;
        m_TargetAmount = ACTION_TARGET_AMOUNT.SINGLE_TARGET;
    }

    public void SetInitPropertiesSelfTarget()
    {
        m_ActionOffOrDef = ACTION_OFF_OR_DEF.OFFENSIVE;

        m_DamageAmount = 60;
        m_Name = "Test Action, Self Target, with affix";

        m_bActionDealsDamage = true;
        m_TargetAmount = ACTION_TARGET_AMOUNT.SINGLE_TARGET;

        m_bActionHasAffix = true;
        m_SpellAffix1 = ActionData.AFFIX_LIST_ID.TEST_AFFIX_1;
    }

    public void SetInitPropertiesSelfTargetDefensive()
    {
        m_ActionOffOrDef = ACTION_OFF_OR_DEF.DEFENSIVE;

        m_ShieldAmount = 40;
        m_Name = "Test Action, Self Target Defensive";

        m_bActionDealsDamage = true;
        m_TargetAmount = ACTION_TARGET_AMOUNT.SINGLE_TARGET;

    }

    public void SetName(string name)
    {
        m_Name = name;
    }

}
