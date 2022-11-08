using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutDoor : Door
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 range;

    private void Molra()
    {
        Collider[] target = Physics.OverlapBox(transform.position, range, transform.rotation, layerMask);

        if(target.Length <= 0)
        {
            return;
        }
        else if(target.Length > 0)
        {
            for(int i = 0; i < target.Length; i++)
            {
                Guest guest = target[i].GetComponent<Guest>();
                PlayerController playerController = target[i].GetComponent<PlayerController>();

                if(guest != null)
                {
                    animator.SetBool("DoorB", true);
                    StartCoroutine(DoorCo());
                    guest = null;
                }
                if(playerController != null)
                {
                    GKeyImage.gameObject.SetActive(true);
                    isDoor = true;
                    playerController = null;
                }
            }
        }
    }




    public override void Update()
    {

        Molra();
        if (isDoor)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GKeyImage.gameObject.SetActive(false);
                animator.SetBool("DoorB", true);
                StartCoroutine(DoorCo());
            }
        }
    }

    public override IEnumerator DoorCo()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("DoorB",false);
        isDoor = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, range);
    }
}
