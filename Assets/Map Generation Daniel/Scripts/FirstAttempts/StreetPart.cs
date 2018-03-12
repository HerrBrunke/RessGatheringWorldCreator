using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetPart : MonoBehaviour {

    public Type type;

    Vector3 rightLaneOffset;
    Vector3 rightLaneStart;
    Vector3 rightLaneEnd;


    public enum Type
    {
        HorizontalWest,
        HorizontalEast,
        VerticalNorth,
        VerticalSouth,
        Crossing
    }


    // Use this for initialization
    void Start()
    {
        switch (type)
        {
            case Type.HorizontalWest:
                rightLaneOffset = Vector3.forward * 0.5f;
                rightLaneStart = rightLaneOffset + Vector3.right;
                rightLaneEnd = rightLaneOffset + Vector3.left;
                break;
            case Type.HorizontalEast:
                rightLaneOffset = Vector3.back * 0.5f;
                rightLaneStart = rightLaneOffset + Vector3.left;
                rightLaneEnd = rightLaneOffset + Vector3.right;
                break;
            case Type.VerticalNorth:
                rightLaneOffset = Vector3.right * 0.5f;
                rightLaneStart = rightLaneOffset + Vector3.back;
                rightLaneEnd = rightLaneOffset + Vector3.forward;
                break;
            case Type.VerticalSouth:
                rightLaneOffset = Vector3.left * 0.5f;
                rightLaneStart = rightLaneOffset + Vector3.forward;
                rightLaneEnd = rightLaneOffset + Vector3.back;
                break;
            case Type.Crossing:
                rightLaneOffset = Vector3.left * 0.5f;
                rightLaneStart = rightLaneOffset + Vector3.forward;
                rightLaneEnd = rightLaneOffset + Vector3.back;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + rightLaneStart + Vector3.up * 0.5f, transform.position + rightLaneEnd + Vector3.up * 0.5f);
    }
}
