using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected GameObject GKeyImage;
    [SerializeField] protected Animator animator;
    protected bool isDoor = false;

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            isDoor = true;
            GKeyImage.SetActive(true);
        }
        
        if(other.GetComponent<Guest>() != null)
        {
            animator.Play("OpenDoorA");
            StartCoroutine(DoorCo());
        }
    }


    public virtual void Update()
    {
        if (isDoor)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                animator.Play("OpenDoorA");
                StartCoroutine(DoorCo());
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            isDoor = false;
            GKeyImage.SetActive(false);
        }
    }

    public virtual IEnumerator DoorCo()
    {
        yield return new WaitForSeconds(5f);
        animator.Play("CloseDoorA");
    }
}
