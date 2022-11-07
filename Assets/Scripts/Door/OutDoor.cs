using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutDoor : Door
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            isDoor = true;
            GKeyImage.SetActive(true);
        }

        if (other.GetComponent<Guest>() != null)
        {
            animator.SetBool("DoorB", true);
            StartCoroutine(DoorCo());
        }
    }


    public override void Update()
    {
        if (isDoor)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                animator.SetBool("DoorB", true);
                StartCoroutine(DoorCo());
            }
        }
    }

    public override IEnumerator DoorCo()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("DoorB",false);
    }
}
