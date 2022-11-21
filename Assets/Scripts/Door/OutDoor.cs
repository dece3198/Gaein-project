using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutDoor : Door
{
    [SerializeField] private bool isOutDoor = false;

    private void Awake()
    {
        GKeyImage.gameObject.SetActive(false);
    }

    public override void Update()
    {
        OpenDoor();
        if (isOutDoor)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                animator.SetBool("DoorB", true);
                StartCoroutine(DoorCo());
            }
        }
    }

    public override void OpenDoor()
    {
        Collider[] target = Physics.OverlapBox(transform.position, range, transform.rotation, layerMask);

        if (target.Length <= 0)
        {
            GKeyImage.gameObject.SetActive(false);
            return;
        }
        else if (target.Length > 0)
        {
            GKeyImage.gameObject.SetActive(true);
            for (int i = 0; i < target.Length; i++)
            {
                Guest guest = target[i].GetComponent<Guest>();
                PlayerController playerControllerB = target[i].GetComponent<PlayerController>();

                if (guest != null)
                {
                    animator.SetBool("DoorB", true);
                    StartCoroutine(DoorCo());
                }
                if (playerControllerB != null)
                { 
                    isOutDoor = true;
                }
            }
        }
    }

    public override IEnumerator DoorCo()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("DoorB", false);
        isOutDoor = false;
    }
}
