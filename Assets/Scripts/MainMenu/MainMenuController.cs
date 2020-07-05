using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnCompletelyDeterminedButtonPressed()
    {
        Debug.Log("Completely Determined Pressed - Now subscribing to button event in GameManager and starting to load the scene.");
        SceneManager.sceneLoaded += GameManager.GetGameManager.OnCompletelyDeterminedButtonPressed;
        SceneManager.LoadScene("MapScene");

    }
}
