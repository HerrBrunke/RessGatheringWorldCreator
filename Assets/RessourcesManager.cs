using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ResourceType
{
    stone,
    berries,
    wood,
    gold
}
public class RessourcesManager : MonoBehaviour
{

    public ResourceType resourceType;
    public ResourceType type;
    public float amount;

    private void Awake()
    {
        if (transform.parent.name == "Stone")
        {
            type = ResourceType.stone; //"stone";
        }
        if (transform.parent.name == "Berries")
        {
            type = ResourceType.berries;
        }
        if (transform.parent.name == "Wood")
        {
            type = ResourceType.wood;
        }
        if (transform.parent.name == "Gold")
        {
            type = ResourceType.gold;
        }

        amount = 500f;
    }
    // Use this for initialization
    void Start()
    {
        Debug.Log(type);
    }

    // Update is called once per frame
    void Update()
    {
        if (amount <= 0)
        {
            Destroy(gameObject);
        }
    }
}