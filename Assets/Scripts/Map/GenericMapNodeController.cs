using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class GenericMapNodeController : MonoBehaviour
{
    protected SpriteRenderer m_SpriteRenderer;

    protected JSONNode m_RoomJSONDataModel;

    protected bool m_HasVisited = false;

    protected GameObject m_LeftNode;
    protected GameObject m_RightNode;

    protected int m_NodeID;

	// Use this for initialization
	void Start () 
    {
		if (m_SpriteRenderer == null)
        {
            m_SpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

            if (m_SpriteRenderer == null)
            {
                m_SpriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void PopulateNodeWithJSON()
    {

    }

    public void SetRoomJSONDataModel(JSONNode aData)
    {
        m_RoomJSONDataModel = aData;
    }

    public bool GetHasVisited()
    {
        return m_HasVisited;
    }

    public void SetHasVisited(bool aHasVisited)
    {
        m_HasVisited = aHasVisited;
    }

    public GameObject GetLeftNode()
    {
        return m_LeftNode;
    }

    public void SetLeftNode(GameObject aNode)
    {
        m_LeftNode = aNode;
    }

    public GameObject GetRightNode()
    {
        return m_RightNode;
    }

    public void SetRightNode(GameObject aNode)
    {
        m_RightNode = aNode;
    }

    public int GetNodeID() 
    {
        return m_NodeID;
    }

    public void SetNodeID(int aNode) 
    {
        m_NodeID = aNode;
    }

    private void OnMouseUpAsButton()
    {
        //Load the scene with the proper info from this.
        Debug.Log("Mapnode was clicked.");
        
        if (m_RoomJSONDataModel.IsNull)
        {
            //Use procedural generation
        }
        else
        {
            //Do other checks for if visited before
            if (m_HasVisited == false)
            {

                Debug.Log("Subscribing to mapnodeclicked in gamemanager to load the combat scene.");
                SceneManager.sceneLoaded += GameManager.GetGameManager.OnMapNodeSelected;
                SceneManager.LoadScene("CombatScene", LoadSceneMode.Additive);

            }
        }

    }

    private void OnMouseOver()
    {
        //Hover display information about what is needed

        /*
        Vector3 scale = new Vector3(
            Mathf.Abs((Mathf.Sin(Time.time) - 0.25f)) + 0.65f,
            Mathf.Abs((Mathf.Sin(Time.time) - 0.25f)) + 0.65f,
            this.transform.localScale.z);

        this.transform.localScale = scale;
        */
    }

    private void OnMouseExit()
    {
        //this.transform.localScale = Vector3.one;
    }
}
