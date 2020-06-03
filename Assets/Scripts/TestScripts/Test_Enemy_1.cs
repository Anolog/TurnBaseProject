using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Enemy_1 : GenericCharacterController
{
    public void Init()
    {
        GenericCharacter genericCharacter = new GenericCharacter();
        genericCharacter.SetIsPlayerControlled(false);
        genericCharacter.SetCharacterName("Enemy");
        genericCharacter.SetCharacterMana(50);
        genericCharacter.SetSpriteFileName("elf_male.png");
        genericCharacter.SetSpriteFilePath("Test_Assets");
        genericCharacter.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.HEAL_TARGET);
        genericCharacter.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.MULTI_STRIKE);
        genericCharacter.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.STRIKE);

        SetCharacterStats(genericCharacter);

        GameManager.GetPlayerManager.AddCharacterToList(genericCharacter);
    }

    //List of action order to use
    //tracker for last action used
    //reset back to beginning if at the end
    //Simplest AI in that regards
}
