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

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Cat" || other.tag == "Quete" || other.tag == "House") {

            UiManager.current.ShowInteractionButton(other.gameObject.GetComponent<InteractionButton>().interactionButton);
            player.canInteract = true;
            player.interactableObjectName = other.tag;
            player.interactableObjectNear = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Cat" || other.tag == "Quete" || other.tag == "House") {

            UiManager.current.CloseInteractionButton(other.gameObject.GetComponent<InteractionButton>().interactionButton);
            player.canInteract = false;
            player.interactableObjectName = null;
            player.interactableObjectNear = null;
        }
    }
}
