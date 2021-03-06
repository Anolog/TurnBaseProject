﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActionMulti : GenericActionModel
{
    public void SetInitPropertiesOffensive()
    {
        m_ActionOffOrDef = ACTION_OFF_OR_DEF.OFFENSIVE;

        m_DamageAmount = 30;
        m_Name = "Test Action, Multi Offensive";

        m_bActionDealsDamage = true;
        m_TargetAmount = ACTION_TARGET_AMOUNT.MULTI_TARGET_OFFENSIVE;
    }

    public void SetInitPropertiesDefensive()
    {
        m_ActionOffOrDef = ACTION_OFF_OR_DEF.DEFENSIVE;

        m_DamageAmount = 15;
        m_Name = "Test Action, Multi Defensive";

        m_bActionDealsDamage = true;
        m_TargetAmount = ACTION_TARGET_AMOUNT.MULTI_TARGET_DEFENSIVE;
    }

    public void SetInitPropertiesAll()
    {
        m_ActionOffOrDef = ACTION_OFF_OR_DEF.OFFENSIVE;

        m_DamageAmount = 25;
        m_Name = "Test Action, All Targets";

        m_bActionDealsDamage = true;
        m_TargetAmount = ACTION_TARGET_AMOUNT.ALL_TARGETS;
    }
}

