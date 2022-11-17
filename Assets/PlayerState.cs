using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_STATE
{


}


public class PlayerState : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TimeScA()
    {
        Time.timeScale = 0.5f;
    }
    public void TimeScB()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Avoid");
        }
    }
}
