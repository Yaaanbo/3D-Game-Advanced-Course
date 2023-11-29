using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Component")]
    [SerializeField] private CharacterController controller;

    [Header("Player Value")]
    [SerializeField] private float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        MovementHandler();
    }

    private void MovementHandler()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;
        controller.Move(Time.deltaTime * moveSpeed * move);
    }
}
