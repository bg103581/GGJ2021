using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float jumpVelocity;

    private Rigidbody rb;

    [HideInInspector] public bool isGrounded = true;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Walk();

        if (rb.velocity.y < 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Walk() {
        Vector2 dir = InputManager.current.direction;
        rb.velocity = new Vector3(dir.x * speed, rb.velocity.y, dir.y * speed);
    }

    public void Jump() {
        if (isGrounded) {
            isGrounded = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity += Vector3.up * jumpVelocity;
        }
    }
}
