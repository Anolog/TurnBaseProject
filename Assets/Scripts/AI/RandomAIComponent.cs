using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAIComponent : GenericAIComponent
{
    public override void DecideActionToUseAlgorithm()
    {
        //Holy shit look at this call.
        m_CurrentSelectedActionID = GetComponentInParent<GenericCharacterController>().GetCharacterStats().GetUsableActionIDList()
        [Random.Range(0, GetComponentInParent<GenericCharacterController>().GetCharacterStats().GetUsableActionIDList().Count)];

        Debug.Log("DEBUG: An AI has randomly picked the action: " + m_CurrentSelectedActionID);
    }
}
