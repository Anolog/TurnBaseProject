using System.Collections;
using System.Collections.Generic;

public class ItemData
{ 
    public enum ITEM_TYPE
    {
        NONE,
        TRINKET,
        WEAPON,
        CHEST,
        HELM,
        RING
    }

    public enum ITEM_ID
    {
        NONE,
        TEST_ITEM_WEAPON,
    }

    //Master list of all the items
    public static Dictionary<ITEM_ID, GenericItem> ITEM_DICTIONARY = new Dictionary<ITEM_ID, GenericItem>();
}
