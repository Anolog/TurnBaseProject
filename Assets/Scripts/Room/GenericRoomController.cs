using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericRoomController : MonoBehaviour
{
    [SerializeField]
    private GenericRoomDataModel m_DataModel = new GenericRoomDataModel();

    //private Scene m_RoomScene;


	// Use this for initialization
	void Start ()
    {
		if (m_DataModel == null)
        {
            m_DataModel = new GenericRoomDataModel();
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetRoomDataModel(GenericRoomDataModel aDataModel)
    {
        m_DataModel = aDataModel;
    }

    public GenericRoomDataModel GetRoomDataModel()
    {
        return m_DataModel;
    }
    
    public void LoadRoom()
    {
        if (m_DataModel != null)
        {
            //Be careful with this, might need to move this in a specific way, depending if I have some scenes
            //or parts of the map that aren't actually a room, and are just an overlay, but are a "room" wink wink
            //sorta like a choice popup like STS
            //Or just make another function for it to load determined on the node type when clicked or something like that...

            if (m_DataModel.GetRoomType() == GenericRoomDataModel.ROOM_TYPE.ENCOUNTER)
            {
                //TODO: NOTE
                //Load scene additive? Not sure which to do but need to experiment later.
                Debug.Log("SceneLoaded subscribed to OnCombatSceneLoaded()");
                SceneManager.sceneLoaded += OnCombatSceneLoaded;
                SceneManager.LoadSceneAsync(GameManager.COMBAT_SCENE_NAME);

            }
            else if (m_DataModel.GetRoomType() == GenericRoomDataModel.ROOM_TYPE.REST)
            {
                Debug.Log("SceneLoaded subscribed to OnRestSceneLoaded()");
                SceneManager.sceneLoaded += OnRestSceneLoaded;
                SceneManager.LoadSceneAsync(GameManager.REST_SCENE_NAME);
            }
            else if (m_DataModel.GetRoomType() == GenericRoomDataModel.ROOM_TYPE.SHOP)
            {
                Debug.Log("SceneLoaded subscribed to OnRestSceneLoaded()");
                SceneManager.sceneLoaded += OnShopSceneLoaded;
                SceneManager.LoadSceneAsync(GameManager.SHOP_SCENE_NAME);
            }

        }
    }

    private void OnCombatSceneLoaded(Scene aScene, LoadSceneMode aSceneLoadMode)
    {
        Debug.Log("Room Scene Loaded: " + aScene.name);
        SceneManager.sceneLoaded -= OnCombatSceneLoaded;
        Debug.Log("SceneLoaded unsubscribed to OnCombatSceneLoaded()");

        //TODO: Refactor later when the ID's are implemneted, so we actually create the objects with the ID and add them to a list or whatever to create in here

        List<GameObject> enemyList = ((EncounterRoomDataModel)m_DataModel).GetListOfEnemiesToSpawn();

        //Spawn enemies
        if (enemyList != null)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                //TODO: Get rid of this shitty check, implement something better
                if (i > 3)
                {
                    break;
                }

                GameObject spawnLoc = GameObject.Find("Enemy_Spawn_Loc_" + i);

                if (spawnLoc != null)
                {
                    enemyList[i].SetActive(true);
                    enemyList[i].transform.position = spawnLoc.transform.position;
                    //TODO: Need to refactor this to have getter and setter / adder and remover
                    GameManager.GetCombatManager.m_CharacterCombatList.Add(enemyList[i]);
                }
            }
        }


        //TODO: I Was here

        //Spawn players
        //GameManager.GetPlayerManager.GetCharacterList();

        //Instantiate new character game object based on the data of the generic character

    }

    private void OnShopSceneLoaded(Scene aScene, LoadSceneMode aSceneLoadMode)
    {
        Debug.Log("Room Scene Loaded: " + aScene.name);
        SceneManager.sceneLoaded -= OnShopSceneLoaded;
        Debug.Log("SceneLoaded unsubscribed to OnRestSceneLoaded()");
    }

    private void OnRestSceneLoaded(Scene aScene, LoadSceneMode aSceneLoadMode)
    {
        Debug.Log("Room Scene Loaded: " + aScene.name);
        SceneManager.sceneLoaded -= OnRestSceneLoaded;
        Debug.Log("SceneLoaded unsubscribed to OnRestSceneLoaded()");
    }
}
