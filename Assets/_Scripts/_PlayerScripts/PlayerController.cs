using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Component")]
    [SerializeField] private CharacterController controller;

    [Header("Player Movement")]
    [SerializeField] private Transform cam;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothRotationTime = .125f;
    private float currentVelocityRef;

    [Header("Jumping")]
    [SerializeField] private Transform groundCheckerPos;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckerRadius;
    [SerializeField] private float gravity = -9.8f;
    private Vector3 playerVelocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementHandler();
        GravityHandler();
    }

    private void MovementHandler()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;

        if(move.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetAngle, ref currentVelocityRef, smoothRotationTime);
            this.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f).normalized * Vector3.forward;
            controller.Move(Time.deltaTime * moveSpeed * moveDir);
        }
    }

    private void GravityHandler()
    {
        if(IsGrounded() && playerVelocity.y <= 0) playerVelocity.y = -2f;

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheckerPos.position, groundCheckerRadius, groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckerPos.position, groundCheckerRadius);
    }
}
