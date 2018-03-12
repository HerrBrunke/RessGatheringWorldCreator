using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficNode : MonoBehaviour {

    public List<Transform> connectedNodes;

    public List<TrafficEdge> connectedEdges;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform tn in connectedNodes)
        {
            Gizmos.DrawLine(transform.position  + Vector3.up * 0.5f,tn.position + Vector3.up * 0.5f);
        }
    }


}
public class TrafficEdge
{
    public List<TrafficNode> connectedNodes;
    public float length;
}

