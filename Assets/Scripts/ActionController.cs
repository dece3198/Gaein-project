using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform playerHand;
    RaycastHit hitInfo;
    Vector3 handPos = new Vector3(0, 0, 0);
    bool isHold = false;
    private void Interaction()
    {
        Debug.DrawRay(transform.position, transform.forward * 5f,Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 5f, layerMask))
        {
            if(hitInfo.transform.GetComponent<Guest>() != null)
            {
                if(isHold)
                {
                    if(Input.GetKeyDown(KeyCode.G))
                    {
                        hitInfo.transform.GetComponent<Guest>().ChangeState(new EatState());
                        FoodManager.instance.stews.Add(playerHand.GetChild(0).gameObject);
                        playerHand.GetChild(0).gameObject.transform.parent = FoodManager.instance.gameObject.transform;
                    }
                }
            }

            if(hitInfo.transform.GetComponent<FoodPickUp>() != null)
            {
                Debug.Log("음식 발견");
                if(Input.GetKeyDown(KeyCode.G))
                {
                    hitInfo.transform.GetComponent<FoodPickUp>().gameObject.transform.parent = playerHand;
                    playerHand.GetChild(0).gameObject.transform.localPosition = handPos;
                    isHold = true;
                }
            }
        }
    }

    private void Update()
    {
        Interaction();
    }
}
