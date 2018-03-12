using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class oldMap : MonoBehaviour {
    public static oldMap Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
}

public class Node
{
    public Vector3 Position;

    public List<Edge> Connections { get; set; }

}

public class Edge
{
    public float Length { get; set; }
    public float Cost { get; set; }
    public Node ConnectedNode { get; set; }
}
