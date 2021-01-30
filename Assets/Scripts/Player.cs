using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    [SerializeField] private float speed;

    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    private void Walk() {
        Vector2 dir = InputManager.current.direction;
        rb.velocity = new Vector3(dir.x, 0, dir.y).normalized * speed * Time.deltaTime;
    }
}
