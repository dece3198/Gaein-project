using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected GameObject GKeyImage;
    [SerializeField] protected Animator animator;
    [SerializeField] private bool isDoor = false;

    public virtual void Update()
    {
        if (isDoor)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                animator.SetBool("DoorA", true);
                StartCoroutine(DoorCo());
            }
        }
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            isDoor = true;
            GKeyImage.gameObject.SetActive(true);
        }
    }

    public virtual IEnumerator DoorCo()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("DoorA", false);
        isDoor = false;
    }

}
