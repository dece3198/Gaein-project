using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Guest guest = null;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private LayerMask layerMask;
    public bool isSit = true;
    public bool isGuest = true;

    private void Update()
    {
        ChairCollider();
        if (guest != null)
        {
            if (guest.guestState == GUEST_STATE.Return)
            {
                isSit = true;
                guest = null;
                GuestPool.Instance.isCoolTime = true;
                GuestPool.Instance.StartCO();
                GuestPool.Instance.guestIndex--;
            }
        }
    }
    public void ChairCollider()
    {
        Collider[] target = Physics.OverlapBox(transform.position, boxSize, transform.rotation, layerMask);

        if(target.Length <= 0)
        {
            isGuest = true;
            return;
        }

        for(int i = 0; i < target.Length; i++)
        {
            if (guest != target[i].GetComponent<Guest>())
            {
                return;
            }
            else
            {
                guest.transform.position = this.transform.position;
                guest.transform.rotation = this.transform.rotation;
                if (isGuest)
                {
                    guest = target[0].GetComponent<Guest>();
                    guest.gameObject.transform.parent = this.transform.parent;
                    guest.ChangeState(GUEST_STATE.Sit);
                    isGuest = false;
                    return;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
