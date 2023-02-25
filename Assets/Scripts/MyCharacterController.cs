using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = .5f;
    [Tooltip("Turn speed in degrees/second, left (+) or right (-)")]
    public float turnSpeed = 200f;
    public float ForwardInput { get; set; }
    public float TurnInput { get; set; }
    new private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        ProcessActions();
    }
    /// <summary>
    /// Processes input actions and converts them into movement
    /// </summary>
    private void ProcessActions()
    {
        // Process Turning
        if (TurnInput != 0f)
        {
            float angle = Mathf.Clamp(TurnInput, -1f, 1f) * turnSpeed;
            transform.Rotate(Vector3.up, Time.fixedDeltaTime * angle);
        }
        // Process Movement
        // Apply a forward or backward velocity based on player input
         rigidbody.velocity += transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) * moveSpeed;
   
    }
    private void Update()
    {
        if (ForwardInput != 0f)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
        animator.SetFloat("speed", rigidbody.velocity.magnitude);

    }
}
