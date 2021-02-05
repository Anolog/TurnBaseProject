using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class MapManager : MonoBehaviour
{
    public enum NODE_PLACEMENT_DIRECTION 
    {
        LEFT,
        RIGHT,
        MIDDLE,

        NONE
    };

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

    public void LoadJSONFromFilePathAndInitMapManager(string aFilePath)
    {
        LoadJSONFromFilePath(aFilePath);

        m_MapGenerationType = (MAP_GENERATION_TYPE)System.Enum.Parse(typeof(MAP_GENERATION_TYPE), m_JSONMapData["mapGenerationType"]);

        CreateRoomNodesWithJSON();
        LinkNodes();
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
                mapNode.transform.SetParent(this.transform);
                mapNode.GetComponent<GenericMapNodeController>().SetHasVisited(false);
                mapNode.GetComponent<GenericMapNodeController>().SetRoomJSONDataModel(m_JSONMapData["roomNodes"][i]);
                //TODO: I WAS LAST HERE - Make it parse the node json info into the node itself.
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

    public void LinkNodes() 
    {
        //TODO: Change this to the initial point where we want the map to end/start?
        m_RoomNodeList[0].gameObject.transform.position = new Vector3(0, 0, m_RoomNodeList[0].gameObject.transform.position.z);
        for (int i = 0; i < m_RoomNodeList.Count; i++) 
        {
            GenericMapNodeController node = m_RoomNodeList[i].GetComponent<GenericMapNodeController>();
            NODE_PLACEMENT_DIRECTION drawDirection = NODE_PLACEMENT_DIRECTION.NONE;
            if ((node.GetRightNodeID() != -1 && node.GetLeftNodeID() == -1) ||
                (node.GetRightNodeID() == -1 && node.GetLeftNodeID() != -1)) 
            {
                drawDirection = NODE_PLACEMENT_DIRECTION.MIDDLE;
                int nodeID = node.GetRightNodeID() == -1 ? node.GetLeftNodeID() : node.GetRightNodeID();
                MoveLinkedNodePosition(m_RoomNodeList[i], GetNodeWithID(nodeID), drawDirection);
            }
            else if (node.GetRightNodeID() != -1 && node.GetLeftNodeID() == -1) 
            {
                drawDirection = NODE_PLACEMENT_DIRECTION.RIGHT;
                MoveLinkedNodePosition(m_RoomNodeList[i], GetNodeWithID(node.GetRightNodeID()), drawDirection);
            } else if (node.GetRightNodeID() == -1 && node.GetLeftNodeID() != -1) 
            {
                drawDirection = NODE_PLACEMENT_DIRECTION.LEFT;
                MoveLinkedNodePosition(m_RoomNodeList[i], GetNodeWithID(node.GetLeftNodeID()), drawDirection);
            } else if (node.GetRightNodeID() != -1 && node.GetLeftNodeID() != -1) 
            {
                MoveLinkedNodePosition(m_RoomNodeList[i], GetNodeWithID(node.GetRightNodeID()), NODE_PLACEMENT_DIRECTION.RIGHT);
                MoveLinkedNodePosition(m_RoomNodeList[i], GetNodeWithID(node.GetLeftNodeID()), NODE_PLACEMENT_DIRECTION.LEFT);
            }


        }
    }

    public GameObject GetNodeWithID(int aNodeID)
    {
        GameObject node = null;
        for (int i = 0; i < m_RoomNodeList.Count; i++) 
        {
            node = (m_RoomNodeList[i].GetComponent<GenericMapNodeController>().GetNodeID() == aNodeID) ? m_RoomNodeList[i] : null;
        }

        return node;
    }

    public void MoveLinkedNodePosition(GameObject aStationaryNode, GameObject linkedNode, NODE_PLACEMENT_DIRECTION direction) 
    {
        float distance = 20.0f;
        float xMovement;

        if(direction != NODE_PLACEMENT_DIRECTION.NONE) 
        {
            if(direction == NODE_PLACEMENT_DIRECTION.MIDDLE) 
            {
                xMovement = 0.0f;
            } 
            else 
            {
                xMovement = direction == NODE_PLACEMENT_DIRECTION.RIGHT ? distance/2 : -distance/2;
            }

            Vector3 movementVector = new Vector3(xMovement, distance, 0);

            linkedNode.transform.position = aStationaryNode.transform.position + movementVector;

            LineRenderer line = GetComponent<LineRenderer>() == null ? gameObject.AddComponent<LineRenderer>() : GetComponent<LineRenderer>();

            line.SetPosition(0, aStationaryNode.transform.position);
            line.SetPosition(1, linkedNode.transform.position);
            line.startColor = Color.blue;
            line.endColor = Color.red;
            line.startWidth = 2.0f;
            line.endWidth = 2.0f;
            line.enabled = true;
        }
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
