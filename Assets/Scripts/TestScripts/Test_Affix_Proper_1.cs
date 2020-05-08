using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Affix_Proper_1 : GenericAffixModel
{
    public void Init()
    {
        m_Name = "Damage DoT";
        m_Description = "Does " + m_DamageAmount + " damage every turn.";
        DEFAULT_AFFIX_USES = 3;

        m_DamageAmount = 10;
        m_bActionDealsDamage = true;
        m_ID = ActionData.AFFIX_LIST_ID.DOT_BASIC;
    }
}
