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
        Debug.DrawRay(transform.position, transform.forward * 1f,Color.red);
        if (Physics.SphereCast(transform.position,0.2f, transform.forward, out hitInfo,1f, layerMask))
        {
            if(hitInfo.transform.GetComponent<Guest>() != null)
            {
                if(isHold)
                {
                    if(Input.GetKeyDown(KeyCode.G))
                    {
                        if(hitInfo.transform.GetComponent<Guest>().guestState == GUEST_STATE.Order)
                        {
                            if(hitInfo.transform.GetComponent<Guest>().foodImage.sprite == playerHand.GetChild(0).GetComponent<FoodPickUp>().food.foodImage)
                            {
                                playerHand.GetChild(0).gameObject.transform.parent = hitInfo.transform.GetComponent<Guest>().foodPos.transform;
                                hitInfo.transform.GetComponent<Guest>().ChangeState(GUEST_STATE.Eat);
                                isHold = false;
                            }
                            else
                            {
                                return;
                            }
                        }

                        if(hitInfo.transform.GetComponent<Guest>().guestState == GUEST_STATE.DrinkOrder)
                        {
                            if (hitInfo.transform.GetComponent<Guest>().foodImage.sprite == playerHand.GetChild(0).GetComponent<FoodPickUp>().food.foodImage)
                            {
                                playerHand.GetChild(0).gameObject.transform.parent = hitInfo.transform.GetComponent<Guest>().foodPos.transform;
                                hitInfo.transform.GetComponent<Guest>().ChangeState(GUEST_STATE.DrinkEat);
                                isHold = false;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }
            if(hitInfo.transform.GetComponent<FoodPickUp>() != null)
            {
                Debug.Log("음식 발견");
                if(Input.GetKeyDown(KeyCode.G))
                {
                    if(hitInfo.transform.GetComponent<FoodPickUp>().isEat)
                    {
                        hitInfo.transform.GetComponent<FoodPickUp>().isEat = false;
                        hitInfo.transform.GetComponent<FoodPickUp>().gameObject.transform.parent = playerHand;
                        playerHand.GetChild(0).gameObject.transform.localPosition = handPos;
                        isHold = true;
                    }
                }
            }
            if(hitInfo.transform.GetComponent<NPC>() != null)
            {
                if(ConversationController.Instance.isShopOn)
                {
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        hitInfo.transform.GetComponent<NPC>().conversationController.canvas.gameObject.SetActive(true);
                        hitInfo.transform.GetComponent<NPC>().Interaction();
                    }
                }
            }
        }
    }

    private void Update()
    {
        Interaction();
    }
}
