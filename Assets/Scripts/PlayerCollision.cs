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
        if (other.CompareTag("Cat")) { // || other.tag == "Quest" || other.tag == "House") {
            //if (!player.isCatCarried) {
            //    UiManager.current.ShowInteractionButton(other.gameObject.GetComponent<InteractionButton>().interactionButton);
            //    player.canInteract = true;
            //    player.interactableObjectName = other.tag;
            //    player.interactableObjectNear = other.gameObject;
            //}

            player.objectIsInteractable = true;

            if (!player.isCatCarried) {
                Image interactionButton = other.gameObject.GetComponent<InteractionButton>().interactionButton;
                UiManager.current.ShowInteractionButton(interactionButton);
                player.interactableObjectNear = other.gameObject;
            }

        Debug.LogError(other.tag);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Cat")) {
            if (player.isCatCarried) {
                Image interactionButton = other.gameObject.GetComponent<InteractionButton>().interactionButton;
                UiManager.current.HideInteractionButton(interactionButton);
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Cat") {// || other.tag == "Quest" || other.tag == "House") {
            UiManager.current.HideInteractionButton(other.gameObject.GetComponent<InteractionButton>().interactionButton);

            player.objectIsInteractable = false;
            player.interactableObjectNear = null;
        }
    }
}
