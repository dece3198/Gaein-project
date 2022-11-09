using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.ParticleSystem;

public class Chair : MonoBehaviour
{
    [SerializeField] private Guest guest = null;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private LayerMask layerMask;
    public bool isSit = true;
    public bool isGuest = true;
    public bool beforeSit = false;

    private void Update()
    {
        ChairCollider();
        if (guest != null)
        {
            guest.transform.position = this.transform.position;
            guest.transform.rotation = this.transform.rotation;
            if (guest.guestState == GUEST_STATE.Return)
            {
                isSit = true;
                beforeSit = false;
            }
        }
    }
    public void ChairCollider()
    {
        Collider[] target = Physics.OverlapBox(transform.position, boxSize, transform.rotation, layerMask);

        if(target.Length <= 0)
        {
            isGuest = true;
            guest = null;
            return;
        }
        else if(target.Length >= 2)
        {
            return;
        }
        if (isGuest && beforeSit)
        {
            guest = target[0].GetComponent<Guest>();
            guest.gameObject.transform.parent = this.transform.parent;
            guest.ChangeState(new SitState());
            isGuest = false;
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
