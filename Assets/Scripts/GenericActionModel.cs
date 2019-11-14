using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericActionModel
{
    protected string m_Name = "Generic";

    public enum SPELL_AFFIX
    {
        NONE,
        EXHAUST,
        DOUBLE_ATTACK

    }

    public enum ACTION_TYPE
    {
        OFFENSIVE,
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

    protected SPELL_AFFIX m_SpellAffix  = SPELL_AFFIX.NONE;
    protected SPELL_AFFIX m_SpellAffix2 = SPELL_AFFIX.NONE;
    protected SPELL_AFFIX m_SpellAffix3 = SPELL_AFFIX.NONE;

    protected ACTION_TYPE m_ActionType  = ACTION_TYPE.NULL;

    protected ACTION_TARGET_AMOUNT m_TargetAmount = ACTION_TARGET_AMOUNT.NO_TARGETS;

    protected int m_DamageAmount      = 0;
    protected int m_ShieldAmount      = 0;
    protected int m_HealAmount        = 0;
    protected int m_StrengthAmount    = 0;
    protected int m_ManaGainAmount    = 0;
    protected int m_ManaCostAmount    = 0;
    protected int m_ActionCostAmount  = 0;
    protected int m_ActionGainAmount  = 0;
    protected int m_StatusOneAmount   = 0;
    protected int m_StatusTwoAmount   = 0;
    protected int m_StatusThreeAmount = 0;

    protected bool m_bActionDealsDamage = false;
    protected bool m_bActionHeals       = false;
    protected bool m_bActionHasAffix    = false;


    public string GetActionName()
    {
        return m_Name;
    }

    public int GetDamageAmount()
    {
        return m_DamageAmount;
    }

    public int GetShieldAmount()
    {
        return m_ShieldAmount;
    }

    public int GetHealAmount()
    {
        return m_HealAmount;
    }

    public ACTION_TYPE GetActionType()
    {
        return m_ActionType;
    }

    //Probably don't need this...? Maybe keep but change to use the new variable
    /*
    public bool GetIfActionHasAffix()
    {
        bool bHasAffix = false;

        if (m_SpellAffix  != SPELL_AFFIX.NONE ||
            m_SpellAffix2 != SPELL_AFFIX.NONE ||
            m_SpellAffix3 != SPELL_AFFIX.NONE  )
        {
            bHasAffix = true;
        }

        return bHasAffix;
    }
    */

}
