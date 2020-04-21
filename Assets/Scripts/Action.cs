using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    protected string m_Name = "GenericAction";

    protected int m_DamageAmount = 0;
    protected int m_ShieldAmount = 0;
    protected int m_HealAmount = 0;
    protected int m_StrengthAmount = 0;
    protected int m_ManaGainAmount = 0;
    protected int m_ActionGainAmount = 0;

    protected bool m_bActionDealsDamage = false;
    protected bool m_bActionHeals = false;
    protected bool m_bActionHasAffix = false;

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

    public bool GetDoesActionDamage()
    {
        return m_bActionDealsDamage;
    }

    public bool GetDoesActionHeal()
    {
        return m_bActionHeals;
    }

    public bool GetDoesActionHaveAffix()
    {
        return m_bActionHasAffix;
    }
}
