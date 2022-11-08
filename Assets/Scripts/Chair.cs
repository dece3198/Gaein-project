using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.ParticleSystem;

public class Chair : MonoBehaviour
{
    [SerializeField] GameObject guest = null;
    public bool isSeat = true;
    public bool isGuest = true;

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Guest>() != null)
        {
            if(isGuest)
            {
                guest = other.gameObject;
                guest.transform.parent = this.gameObject.transform.parent;
                guest.GetComponent<Guest>().ChangeState(new SitState());
                isGuest = false;
            }
        }
    }

    private void Update()
    {
        if(guest != null)
        {
            guest.transform.position = this.transform.position;
            guest.transform.rotation = this.transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(guest != null)
        {
            isSeat = true;
            isGuest = true;
            guest = null;
        }
    }
}
