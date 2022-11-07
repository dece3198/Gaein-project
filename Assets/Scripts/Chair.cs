using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chair : SeatManager
{
    public override void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Guest>() != null)
        {
            other.GetComponent<NavMeshAgent>().ResetPath();
            other.GetComponent<Animator>().Play("Sit");
            other.transform.parent = this.gameObject.transform.parent;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Guest>() != null)
        {
            other.transform.position = this.transform.position + new Vector3(0,3f,0);
            other.transform.rotation = this.transform.rotation;
        }
    }
}
