using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterRoomDataModel : GenericRoomDataModel
{
    //TODO:
    //Refactor this to use ID's of enemies in order to save memory.
    private List<GameObject> m_ListOfEnemiesToSpawn = new List<GameObject>();

    private List<ItemData.ITEM_ID> m_ListOfItemDrops = new List<ItemData.ITEM_ID>();

    private int m_CurrencyDropAmount = 0;

    //TODO: Refactor when ID's for enemies are added
    public void AddEnemyToSpawnList(GameObject aEnemyObject)
    {
        m_ListOfEnemiesToSpawn.Add(aEnemyObject);
    }

    //Later will change this with ID's as well
    public List<GameObject> GetListOfEnemiesToSpawn()
    {
        return m_ListOfEnemiesToSpawn;
    }

    //Use json to create/populate later at some point
    public void JSON_CreateEncounterRoomDataModel()
    {
        //Not in use right now cause json not implemented
    }

    public void AddItemToDropList(ItemData.ITEM_ID aItemID)
    {
        m_ListOfItemDrops.Add(aItemID);
    }

    public void SetCurrencyDropAmount(int aAmount)
    {
        m_CurrencyDropAmount = aAmount;
    }

    public int GetCurrencyDropAmount()
    {
        return m_CurrencyDropAmount;
    }

    public void SetCurrencyDropRandomAmountBetweenValues(int aMin, int aMax)
    {
        m_CurrencyDropAmount = Random.Range(aMin, aMax);
    }
}
