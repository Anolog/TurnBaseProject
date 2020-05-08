using System.Collections;
using System.Collections.Generic;

public class ActionData
{
    public enum ACTION_LIST_ID
    {
        NONE,
        ATTACK_BASIC,

        ATTACK_ONE,
        ATTACK_TWO,
        ATTACK_THREE,
        ATTACK_FOUR,

        SPELL_ONE,
        SPELL_TWO,
        SPELL_THREE,

        SHIELD_ONE,
        SHIELD_TWO,

        SPELL_HEAL_ONE,
        SPELL_HEAL_TWO,

        STRIKE,
        HEAL_TARGET,
        MULTI_STRIKE
    }

    //Master list of the abilities
    //Why was this a List<Dictionary>> before? :thonking:
    public static Dictionary<ACTION_LIST_ID, GenericActionModel> ABILITY_DICTIONARY = new Dictionary<ACTION_LIST_ID, GenericActionModel>();

    public enum AFFIX_LIST_ID
    {
        NONE,
        EXHAUST,
        DOUBLE_ATTACK,
        DOT_BASIC,


    }

    //Master list of all affixes
    public static Dictionary<AFFIX_LIST_ID, GenericAffixModel> AFFIX_DICTIONARY = new Dictionary<AFFIX_LIST_ID, GenericAffixModel>();
}
