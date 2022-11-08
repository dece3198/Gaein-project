using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected GameObject GKeyImage;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected Vector3 range;
    [SerializeField] private bool isDoor = false;

    public virtual void Update()
    {
        OpenDoor();
        if (isDoor)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                animator.SetBool("DoorA", true);
                StartCoroutine(DoorCo());
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoorA") || animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoorB"))
        {
            GKeyImage.gameObject.SetActive(false);
        }
    }

    public virtual void OpenDoor()
    {
        Collider[] target = Physics.OverlapBox(transform.position, range, transform.rotation, layerMask);

        if (target.Length <= 0)
        {
            return;
        }
        else if (target.Length > 0)
        {
            for (int i = 0; i < target.Length; i++)
            {
                Guest guest = target[i].GetComponent<Guest>();
                PlayerController playerControllerA = target[i].GetComponent<PlayerController>();

                if (guest != null)
                {
                    animator.SetBool("DoorA", true);
                    StartCoroutine(DoorCo());
                }
                if (playerControllerA != null)
                {
                    GKeyImage.gameObject.SetActive(true);
                    isDoor = true;
                }
            }
        }
    }

    public virtual IEnumerator DoorCo()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("DoorA", false);
        isDoor = false;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, range);
    }
}
