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

    public override void Update()
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
    public override void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            isOutDoor = true;
            GKeyImage.gameObject.SetActive(true);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            GKeyImage.gameObject.SetActive(false);
        }
    }

    public override IEnumerator DoorCo()
    {
        yield return new WaitForSeconds(5f);
        isOutDoor = false;
    }
}
