using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesManager : MonoBehaviour {

    public string type;
    public float amount;

    private void Awake()
    {
        if(transform.parent.name == "Stone")
        {
            type = "stone";
        }
        if(transform.parent.name == "Berries")
        {
            type = "berries";
        }
        if(transform.parent.name == "Wood")
        {
            type = "wood";
        }
        if(transform.parent.name == "Gold")
        {
            type = "gold";
        }
        
        amount = 500f;
    }
    // Use this for initialization
    void Start () {
        Debug.Log(type);
	}
	
	// Update is called once per frame
	void Update () {
		if(amount <= 0)
        {
            Destroy(gameObject);
        }
	}
}
