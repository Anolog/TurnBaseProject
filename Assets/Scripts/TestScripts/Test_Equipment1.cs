using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Equipment1 : GenericItem
{
    public void Init()
    {
        m_Name = "Test_Weapon_1";
        m_Description = "This weapon is a test weapon.";
        m_Health = 10;
        m_Mana = 10;
        m_ManaRegen = 4;
        m_ItemType = ItemData.ITEM_TYPE.WEAPON;
    }

}
