using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Guest>() != null)
        {
            if(other.GetComponent<Guest>().guestState == GUEST_STATE.Return)
            {
                GuestPool.Instance.guestList.Add(other.gameObject);
                other.GetComponent<Guest>().ChangeState(GUEST_STATE.Walk);
                other.gameObject.SetActive(false);
                GuestPool.Instance.guestIndex--;
            }
        }
    }
}
