using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollision : MonoBehaviour
{
    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Ground") {
            player.isGrounded = true;
        }
    }


}
