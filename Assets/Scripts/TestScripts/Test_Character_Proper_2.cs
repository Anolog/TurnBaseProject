using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Character_Proper_2 : GenericCharacterController
{
    public void Init()
    {
        GenericCharacter genericCharacter = new GenericCharacter();
        genericCharacter.SetIsPlayerControlled(true);
        genericCharacter.SetCharacterName("Abbarath");
        genericCharacter.SetCharacterMana(50);
        genericCharacter.SetSpriteFileName("wizard_1_attack-a_001.png");
        genericCharacter.SetSpriteFilePath("Test_Assets");
        genericCharacter.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.HEAL_TARGET);
        genericCharacter.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.MULTI_STRIKE);

        SetCharacterStats(genericCharacter);

        GameManager.GetPlayerManager.AddCharacterToList(genericCharacter);
    }
}
