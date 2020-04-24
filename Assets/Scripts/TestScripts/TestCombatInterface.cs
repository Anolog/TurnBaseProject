using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCombatInterface : MonoBehaviour
{
    public Canvas m_Canvas;
    public Font m_Font;

    public int CharCount = 0;

	// Use this for initialization
	void Start ()
    { 
        if (m_Canvas == null)
        {
            m_Canvas = new Canvas();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CreateCharacterInformationDisplay(GenericCharacter genericCharacter)
    {
        GameObject textManager = new GameObject("TextManager Charno: " + CharCount);
        textManager.transform.SetParent(m_Canvas.transform);
        Text nameHealthMana = textManager.AddComponent<Text>();
        nameHealthMana.font = m_Font;
        nameHealthMana.text = "Name: " + genericCharacter.GetCharacterName() +
                              " Health: " + genericCharacter.GetCharacterHealth() +
                              " Mana: " + genericCharacter.GetCharacterMana();

        if (GameManager.GetCombatManager.m_ActionUsersWithAffixes.ContainsKey(genericCharacter))
        {
            if (GameManager.GetCombatManager.m_ActionUsersWithAffixes[genericCharacter] != null)
            {
                nameHealthMana.text += " Affixes: " + GameManager.GetCombatManager.m_ActionUsersWithAffixes[genericCharacter][0];
            }
        }

        if (CharCount == 0)
        {
            nameHealthMana.rectTransform.localPosition = new Vector3(-400, 300, 0);
        }
        else
        {
            nameHealthMana.rectTransform.localPosition = new Vector3(400, 300, 0);
        }

        CharCount++;

    }
}
