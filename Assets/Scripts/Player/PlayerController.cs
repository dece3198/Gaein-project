using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private CharacterController controller;
    private Vector3 moveVec;
    private float moveY;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        moveVec = (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward) * moveSpeed;
        controller.Move(moveVec * Time.deltaTime);
    }
}
