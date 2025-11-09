using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float walkspeed = 2f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float maxVelocityChange = 10f;
    [SerializeField] private float airControlValue = 0.3f;
    [SerializeField] private Vector2 moveInput; // Anoush - Used to just debug value in editor (not useful for assigning value)
    [Space]
    [Header("Player Jump Settings")]
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;


    private Rigidbody rb;
    private bool isRunning;
    private float moveSpeed;
    private bool isPlayerGrounded;

    #region Built-in Methods
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x, y;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        moveInput = new Vector2(x, y);
        moveInput.Normalize();

        isPlayerGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkspeed;

        if (Input.GetKeyDown(jumpKey) && isPlayerGrounded)
        {
            PlayerJump();
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(CalculateMovement(isPlayerGrounded ? moveSpeed : moveSpeed * airControlValue), ForceMode.VelocityChange); // Anoush - Apply movement force while grounded or in air with reduced control
    }
    #endregion

    #region Custom Methods
    Vector3 CalculateMovement(float speed)
    {
        Vector3 targetVelocity = new Vector3(moveInput.x, 0, moveInput.y);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;
        Vector3 velocity = rb.velocity;

        if (moveInput.magnitude > 0.5f)
        {
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            return velocityChange;
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }
    private void PlayerJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
