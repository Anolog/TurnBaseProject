using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class MapManager : MonoBehaviour
{
    private JSONNode m_JSONMapData = "";

    private List<GameObject> m_RoomNodeList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetJSONMapData(string aJSONData)
    {
        m_JSONMapData = aJSONData;
    }

    public string GetJSONMapData()
    {
        return m_JSONMapData;
    }

    public void LoadJSONFromFilePath(string aFilePath)
    {
        TextAsset file = Resources.Load(aFilePath) as TextAsset;
        m_JSONMapData = JSON.Parse(file.ToString());

    }

    public void CreateRoomNodesWithJSON()
    {
        if (!m_JSONMapData.IsNull)
        {
            //instantiate prefab for the node
        }
        else
        {
            Debug.Log("Error: JSON is null.");
        }
    }

    //TODO: do this later when random generation is there
    public void CreateRandomNode()
    {

    }
}
