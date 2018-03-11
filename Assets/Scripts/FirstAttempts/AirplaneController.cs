using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    Airport nextAirport;
    Vector3 target;
    public float speed;

    private void Start()
    {
        Transform AirplaneCamera = GameObject.Find("AirplaneCamera").transform;
        if (AirplaneCamera.parent == null)
        {
            AirplaneCamera.parent = transform;
            AirplaneCamera.position = transform.position + Vector3.up * 2f;
            AirplaneCamera.LookAt(transform.position + Vector3.forward * 0.5f);
        }
        speed = MapManager.Instance.tileSize;
        SelectMidPointAndNextAirport();
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, target) > 2f)
        {
            transform.LookAt(target);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
            SelectNextTarget();
    }

    void SelectNextTarget()
    {
        if (target == nextAirport.location)
        {
            SelectMidPointAndNextAirport();
        }
        else
        {
            target = nextAirport.location;
        }
        transform.LookAt(target);
    }
    void SelectMidPointAndNextAirport()
    {
        Airport nextOne = nextAirport;
        while (nextOne == nextAirport)
            nextAirport = MapManager.Instance.airports.ToArray()[Random.Range(0, MapManager.Instance.airports.ToArray().Length)];
        Vector3 midPoint = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(nextAirport.location.x, 0, nextAirport.location.z);
        target = midPoint + Vector3.up * 20;
    }


}