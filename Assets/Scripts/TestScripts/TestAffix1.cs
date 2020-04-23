using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAffix1 : GenericAffixModel
{
    public void InitAffix()
    {
        m_bActionDealsDamage = true;
        m_bActionHeals = false;
        m_CurrentAffixUses = 5;
        m_Description = "This is the description for TestAffix1";
        m_Name = "TestAffix1";
        m_IsStackable = true;
        m_StackAmount = 3;
        m_DamageAmount = 10;
        m_ID = ActionData.AFFIX_LIST_ID.TEST_AFFIX_1;
        SetDefaultAffixUses(5);
        SetAffixUses(5);
    }
}
