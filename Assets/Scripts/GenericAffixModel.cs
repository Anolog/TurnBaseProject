using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAffixModel : Action
{
    protected string m_Description = "This is a test description";

    //TODO: Add this for GenericActionModel as well ?
    protected ActionData.AFFIX_LIST_ID m_ID;

    protected bool m_IsStackable = false;
    protected int m_StackAmount = 1;

    protected static int DEFAULT_AFFIX_USES = 0;
    protected int m_CurrentAffixUses = DEFAULT_AFFIX_USES;

    public void RefreshAffix()
    {
        //TODO: Create this one all the values are in place
        m_CurrentAffixUses = DEFAULT_AFFIX_USES;
        m_StackAmount = 1;
    }

    public int GetStackAmount()
    {
        return m_StackAmount;
    }

    public void AddStackAmount(int aStack)
    {
        m_StackAmount += aStack;
    }

    public void SetStackAmount(int aStack)
    {
        m_StackAmount = aStack;
    }

    public int GetActionGainAmount()
    {
        return m_ActionGainAmount;
    }

    public void SetAffixUses(int aAffixUses)
    {
        m_CurrentAffixUses = aAffixUses;
    }

    public int GetAffixUses()
    {
        return m_CurrentAffixUses;
    }

    public void AddToAffixUses(int aAffixUses)
    {
        m_CurrentAffixUses += aAffixUses;
    }

    public void SetDefaultAffixUses(int aDefaultAffixUses)
    {
        DEFAULT_AFFIX_USES = aDefaultAffixUses;
    }

    public int GetDefaultAffixUses()
    {
        return DEFAULT_AFFIX_USES;
    }

    public bool GetIsStackable()
    {
        return m_IsStackable;
    }

    public void SetIsStackable(bool aIsStackable)
    {
        m_IsStackable = aIsStackable;
    }

    public string GetDescription()
    {
        return m_Description;
    }

    public ActionData.AFFIX_LIST_ID GetAffixID()
    {
        return m_ID;
    }

    public void SetAffixID(ActionData.AFFIX_LIST_ID aID)
    {
        m_ID = aID;
    }
}
