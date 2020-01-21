using System.Collections;
using System.Collections.Generic;

public class PerformActionDataModel 
{
    private ActionData.ACTION_LIST_ID m_ActionID;
    private GenericActionModel.ACTION_TARGET_AMOUNT m_ActionTargetAmount;
    private GenericCharacter m_AttackerData;
    private GenericCharacter m_DefenderData;

    public PerformActionDataModel()
    {
        m_ActionID = ActionData.ACTION_LIST_ID.NONE;
        m_ActionTargetAmount = GenericActionModel.ACTION_TARGET_AMOUNT.NO_TARGETS;
        m_AttackerData = null;
        m_DefenderData = null;
    }

    public PerformActionDataModel(ActionData.ACTION_LIST_ID aActionID, 
                                  GenericActionModel.ACTION_TARGET_AMOUNT aTargetAmount,
                                  GenericCharacter aAttacker,
                                  GenericCharacter aDefender)
    {
        m_ActionID = aActionID;
        m_ActionTargetAmount = aTargetAmount;
        m_AttackerData = aAttacker;
        m_DefenderData = aDefender;
    }

    public PerformActionDataModel(ActionData.ACTION_LIST_ID aActionID,
                              GenericActionModel.ACTION_TARGET_AMOUNT aTargetAmount,
                              GenericCharacter aAttacker)
    {
        m_ActionID = aActionID;
        m_ActionTargetAmount = aTargetAmount;
        m_AttackerData = aAttacker;
        m_DefenderData = aAttacker;
    }

    public ActionData.ACTION_LIST_ID GetActionID()
    {
        return m_ActionID;
    }

    public void SetActionID(ActionData.ACTION_LIST_ID aActionID)
    {
        m_ActionID = aActionID;
    }

    public GenericActionModel.ACTION_TARGET_AMOUNT GetTargetAmount()
    {
        return m_ActionTargetAmount;
    }

    public void SetTargetAmount(GenericActionModel.ACTION_TARGET_AMOUNT aTargetAmount)
    {
        m_ActionTargetAmount = aTargetAmount;
    }

    public GenericCharacter GetAttackerData()
    {
        return m_AttackerData;
    }

    public void SetAttackerData(GenericCharacter aAttackerData)
    {
        m_AttackerData = aAttackerData;
    }

    public GenericCharacter GetDefenderData()
    {
        return m_DefenderData;
    }

    public void SetDefenderData(GenericCharacter aAttackerData)
    {
        m_AttackerData = aAttackerData;
    }
}
