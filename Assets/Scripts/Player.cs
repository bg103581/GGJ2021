using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform camera;

    [SerializeField] private float speed;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Rigidbody rb;
    private Vector2 dir;
    private Vector3 moveDir;

    [HideInInspector] public bool isGrounded = true;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        rb.velocity = Vector3.zero;
    }

    private void Update() {
        dir = InputManager.current.direction.normalized;
        Rotate();
    }

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
        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);
    }

    public void Jump() {
        if (isGrounded) {
            isGrounded = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity += Vector3.up * jumpVelocity;
        }
    }

    private void Rotate() {
        if (dir.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else {
            moveDir = Vector3.zero;
        }
    }
}
