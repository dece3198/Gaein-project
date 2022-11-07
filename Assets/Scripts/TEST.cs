using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TEST : MonoBehaviour
{
    [SerializeField] List<Transform> sitDownPos = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Guest>() != null)
        {
            int rand = Random.Range(0, 8);
            switch (rand)
            {
                case 0: other.GetComponent<NavMeshAgent>().SetDestination(sitDownPos[0].position); break;
                    case 1: other.GetComponent<NavMeshAgent>().SetDestination(sitDownPos[1].position); break;
                case 2: other.GetComponent<NavMeshAgent>().SetDestination(sitDownPos[2].position); break;
                case 3: other.GetComponent<NavMeshAgent>().SetDestination(sitDownPos[3].position); break;
                case 4: other.GetComponent<NavMeshAgent>().SetDestination(sitDownPos[4].position); break;
                case 5: other.GetComponent<NavMeshAgent>().SetDestination(sitDownPos[5].position); break;
                case 6: other.GetComponent<NavMeshAgent>().SetDestination(sitDownPos[6].position); break;
                case 7: other.GetComponent<NavMeshAgent>().SetDestination(sitDownPos[7].position); break;
            }
        }
    }
}
