using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction1 : GenericActionModel 
{
    public void SetInitProperties()
    {
        m_ActionType = ACTION_TYPE.OFFENSIVE;

        m_DamageAmount = 10;
        m_Name = "Test Action, Basic";

        m_bActionDealsDamage = true;
        m_TargetAmount = ACTION_TARGET_AMOUNT.SINGLE_TARGET;
    }

}
