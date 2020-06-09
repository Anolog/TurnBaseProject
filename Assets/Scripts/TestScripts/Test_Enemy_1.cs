using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Enemy_1 : GenericCharacterController
{
    public void Init()
    {
        GenericCharacter genericCharacter = new GenericCharacter();
        genericCharacter.SetIsPlayerControlled(false);
        genericCharacter.SetCharacterName("Enemy_Wolf");
        genericCharacter.SetCharacterMana(40);
        genericCharacter.SetCharacterHealth(500);
        genericCharacter.SetSpriteFileName("wolf.png");
        genericCharacter.SetSpriteFilePath("Test_Assets");
        genericCharacter.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.MULTI_STRIKE);
        genericCharacter.AddActionIDToUsableActionList(ActionData.ACTION_LIST_ID.STRIKE);

        SetCharacterStats(genericCharacter);

        //We do not do this with enemies. Add it in combat manager
        //GameManager.GetPlayerManager.AddCharacterToList(genericCharacter);
    }
}
