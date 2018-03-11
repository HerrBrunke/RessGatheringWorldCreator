using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    public TrafficNode currentNode;
    public TrafficNode previousNode;
    public float CurrentSpeed = 4.0f;//{ get; set; }
    float MaxSpeed = 20f;//{ get; set; }

    float minDist = 0.2f;

    Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

    }
	
    

	// Update is called once per frame
	void Update () {
		if(DistanceToNode(currentNode) > minDist)
        {
            transform.LookAt(currentNode.transform.position);
            DriveToNode(currentNode);
        }
        else
        {
            currentNode = GetNextNode(currentNode);
        }
	}
    void TurnToNode(TrafficNode node)
    {
        Quaternion myRotation = transform.rotation;
        Vector3 direction = (node.transform.position - transform.position).normalized;
        transform.rotation.SetFromToRotation(transform.position, direction);
    }
    void DriveToNode(TrafficNode node)
    {
        transform.Translate(Vector3.forward * CurrentSpeed * Time.deltaTime);
        //rigidBody.AddForce(transform.forward * CurrentSpeed);
        //rigidBody.velocity += (transform.forward * CurrentSpeed);
    }
    float DistanceToNode(TrafficNode node)
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(node.transform.position.x, 0, node.transform.position.z));
        return distance;
    }

    TrafficNode GetNextNode(TrafficNode node)
    {
        List<Transform> nextPossibleNodes = new List<Transform>(node.connectedNodes);   
        nextPossibleNodes.Remove(previousNode.transform);

        int randomNodeIndex = Random.Range(0, nextPossibleNodes.ToArray().Length);
        Debug.Log(randomNodeIndex);
        TrafficNode nextNode = nextPossibleNodes[randomNodeIndex].GetComponent<TrafficNode>();

        previousNode = node;

        if (nextNode != null)
        {
            return nextNode;
        }
        else
        {
            Debug.LogWarning("No next node found continuing on old node");
            return previousNode;
        }
    }
}
