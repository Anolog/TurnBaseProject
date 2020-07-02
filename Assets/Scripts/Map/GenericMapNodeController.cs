using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GenericMapNodeController : MonoBehaviour
{
    protected SpriteRenderer m_SpriteRenderer;

    protected JSONNode jsonDataModel;

    protected bool m_HasVisited = false;

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

    public void PopulateNodeWithJSON(string aJSON)
    {

    }

    public bool GetHasVisited()
    {
        return m_HasVisited;
    }

    public void SetHasVisited(bool aHasVisited)
    {
        m_HasVisited = aHasVisited;
    }

    private void OnMouseUpAsButton()
    {
        //Load the scene with the proper info from this.
        
        if (jsonDataModel.IsNull)
        {
            //Use procedural generation
        }
        else
        {

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
