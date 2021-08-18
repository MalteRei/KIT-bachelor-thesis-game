using System.Collections;
using UnityEngine;


public class Island : MonoBehaviour
{
    public GameObject proximity;
    private Collider proximityCollider;

    private void Start()
    {
        proximityCollider = proximity.GetComponent<Collider>();
    }


    public bool isInProximity(Vector3 point)
    {
        return proximityCollider.bounds.Contains(point);
    }



}