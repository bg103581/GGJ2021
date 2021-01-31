using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        switch (other.tag) {
            case ("Cat"):

                player.objectIsInteractable = true;

                if (!player.isCatCarried) {
                    Image interactionButton = other.gameObject.GetComponent<InteractionButton>().interactionButton;
                    UiManager.current.ShowInteractionButton(interactionButton, true);
                    player.interactableObjectNear = other.gameObject;
                }

                break;

            case ("Quest"):

                player.objectIsInteractable = true;

                if (!player.isCatCarried) {
                    Image interactionButton = other.gameObject.GetComponent<InteractionButton>().interactionButton;
                    UiManager.current.ShowInteractionButton(interactionButton, true);
                    player.interactableObjectNear = other.gameObject;
                }
                break;

            default: break;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Cat")) {
            if (player.isCatCarried) {
                Image interactionButton = other.gameObject.GetComponent<InteractionButton>().interactionButton;
                UiManager.current.ShowInteractionButton(interactionButton, false);
            }
        }

        player.interactableObjectNear = other.gameObject;
    }
    private void OnTriggerExit(Collider other) {
        Image interactionButton;

        switch (other.tag) {
            case ("Cat"):
                interactionButton = other.gameObject.GetComponent<InteractionButton>().interactionButton;
                UiManager.current.ShowInteractionButton(interactionButton, false);

                player.objectIsInteractable = false;
                player.interactableObjectNear = null;
                break;

            case ("Quest"):
                interactionButton = other.gameObject.GetComponent<InteractionButton>().interactionButton;
                UiManager.current.ShowInteractionButton(interactionButton, false);

                player.objectIsInteractable = false;
                player.interactableObjectNear = null;

                break;
            default: break;
        }
    }
}
