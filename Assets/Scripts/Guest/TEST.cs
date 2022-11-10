using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TEST : MonoBehaviour
{
    [SerializeField] List<Transform> seatPos = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Guest>() != null)
        {
            while(true)
            {
                int rand = Random.Range(0, seatPos.Count);
                if (seatPos[rand].GetComponent<Chair>().isSit)
                {
                    seatPos[rand].GetComponent<Chair>().isSit = false;
                    seatPos[rand].GetComponent<Chair>().guest = other.GetComponent<Guest>();
                    other.GetComponent<NavMeshAgent>().SetDestination(seatPos[rand].position);
                    Debug.Log(rand);
                    break;
                }
            }
        }
    }

}
