using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxVelocityChange;

    private Rigidbody rb;
    [SerializeField] private Vector2 moveInput;
    // Start is called before the first frame update
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

    }
    void FixedUpdate()
    {
      
        rb.AddForce(CalculateMovement(speed), ForceMode.VelocityChange);
    }
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
            return new Vector3(0,0,0);
        }

    }
}
