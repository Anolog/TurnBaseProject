﻿using System.Collections;
using System.Collections.Generic;

public class GenericItem
{
    protected ItemData.ITEM_TYPE m_ItemType = ItemData.ITEM_TYPE.NONE;
    protected string m_Name = "";
    protected string m_Description = "";

    protected int m_Health = 0;
    protected int m_HealthRegen = 0;
    protected int m_Mana = 0;
    protected int m_ManaRegen = 0;
    protected int m_Strength = 0;
    protected int m_ShieldAmount = 0;

    public ItemData.ITEM_TYPE GetItemType()
    {
        return m_ItemType;
    }

    public string GetName()
    {
        return m_Name;
    }

    public string GetDescription()
    {
        return m_Description;
    }

    public int GetHealth()
    {
        return m_Health;
    }

    public int GetHealthRegen()
    {
        return m_HealthRegen;
    }

    public int GetMana()
    {
        return m_Mana;
    }

    public int GetManaRegen()
    {
        return m_ManaRegen;
    }

    public int GetStrength()
    {
        return m_Strength;
    }

    public int GetShield()
    {
        return m_ShieldAmount;
    }
}
