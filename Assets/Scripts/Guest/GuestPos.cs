using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestPos : MonoBehaviour
{
    [SerializeField] private Transform PosB;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Guest>() != null)
        {
            other.GetComponent<Guest>().nav.SetDestination(PosB.position);
        }
    }
}
