using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class MapManager : MonoBehaviour
{
    private enum MAP_GENERATION_TYPE
    {
        COMPLETELY_DETERMINED,
        PREMADE_NODES,
        COMPLETELY_RANDOM,

        NONE
    }

    private MAP_GENERATION_TYPE m_MapGenerationType = MAP_GENERATION_TYPE.NONE;
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

    public void InitMapGeneration()
    {
        if (m_MapGenerationType == MAP_GENERATION_TYPE.COMPLETELY_DETERMINED)
        {
            //CreateRoomNodesWithJSON();
        }
        else if (m_MapGenerationType == MAP_GENERATION_TYPE.COMPLETELY_RANDOM)
        {
            GenerateMapWithRandomNodes();
        }
        else if (m_MapGenerationType == MAP_GENERATION_TYPE.PREMADE_NODES)
        {

        }
        else
        {
            Debug.Log("Error: No type of map generation set. Defaulting to completely random and calling function InitMapGeneration() again.");
            m_MapGenerationType = MAP_GENERATION_TYPE.COMPLETELY_RANDOM;
            InitMapGeneration();
        }
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
            for (int i = 0; i < m_JSONMapData["roomNodes"].AsArray.Count; i++)
            {
                GameObject mapNode;
                //TODO: Replace path with static path
                mapNode = Instantiate(Resources.Load("Prefabs/MapNode_Prefab")) as GameObject;
                mapNode.GetComponent<GenericMapNodeController>().SetHasVisited(false);
                mapNode.GetComponent<GenericMapNodeController>().SetRoomJSONDataModel(m_JSONMapData["roomNodes"][i]);

                m_RoomNodeList.Add(mapNode);
            }
        }
        else
        {
            Debug.Log("Error: JSON is null.");
        }
    }

    //TODO: Create some sorta recursive thing around this? :thonking:
    public GameObject CreateRoomNodeObject()
    {
        GameObject node = new GameObject();



        return node;
    }

    //TODO: do this later when random generation is there
    public void CreateRandomNode()
    {

    }

    public void GenerateMapWithRandomNodes()
    {
        //TODO: For pre-alpha, eventually, make a binary tree  for the map, but have it so the tree is reversed.
        //Start of the tree is the end of the map. so you work your way up the tree from the bottom
        if (m_RoomNodeList.Count != 0)
        {
            bool isFirstIteration = true;
            
            for (int i = 0; i < m_RoomNodeList.Count; i++)
            {

            }
        }
        else
        {
            Debug.Log("Error: Trying to generate the map with a bunch of nodes, but the list of nodes is empty.");
        }
    }
}
