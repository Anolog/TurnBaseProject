using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Character_Proper_1 : GenericCharacterController
{
    public void Init()
    {
        GenericCharacter genericCharacter = new GenericCharacter();
        genericCharacter.SetIsPlayerControlled(true);
        genericCharacter.SetCharacterName("Argonn");
        genericCharacter.SetCharacterMana(50);
        genericCharacter.SetSpriteFileName("elf_male.png");
        genericCharacter.SetSpriteFilePath("Test_Assets");
        genericCharacter.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.HEAL_TARGET);
        genericCharacter.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.MULTI_STRIKE);
        genericCharacter.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.STRIKE);

        SetCharacterStats(genericCharacter);

        GameManager.GetPlayerManager.AddCharacterToList(genericCharacter);
    }
}
