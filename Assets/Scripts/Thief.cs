using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartStab()
    {
        Time.timeScale = 0.1f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            animator.Play("Stab");
        }
    }
}
