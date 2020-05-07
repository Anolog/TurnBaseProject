using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        //Figure this out better, later on, for the state
        if (GameManager.GetCombatManager)
        {

        }

        var raycastHit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y),
                          Vector2.zero, 0.0f);

        if (raycastHit)
        {
            Debug.Log("RAYCAST - MOUSE DOWN: Hit Object Named - " + raycastHit.transform.gameObject.name);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
