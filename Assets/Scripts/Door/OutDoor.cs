using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutDoor : Door
{
    [SerializeField] private bool isOutDoor = false;

    private void Awake()
    {
        GKeyImage.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (isOutDoor)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                animator.SetBool("DoorB", true);
                StartCoroutine(DoorCo());
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            isOutDoor = true;
            GKeyImage.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            GKeyImage.gameObject.SetActive(false);
        }
    }

    public IEnumerator DoorCo()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("DoorB", false);
        isOutDoor = false;
    }
}
