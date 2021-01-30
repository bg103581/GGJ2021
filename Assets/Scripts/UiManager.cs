using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [HideInInspector] public static UiManager current;
    [SerializeField] private GameObject m_catInteractionCanvas;
    [SerializeField] private GameObject m_questInteractionCanvas;
    [SerializeField] private Button pickupButton;

    private void Awake() {
        current = this;
    }

    public void PickUp() {
        Debug.Log("pick up cat");
        GameEvents.current.PickUp();
    }

    public void ShowInteractionButton(Image image) {
        image.enabled = true;
    }

    public void CloseInteractionButton(Image image) {
        image.enabled = false;
    }

    public void DisablePickUpButton() {
        pickupButton.interactable = false;
    }

    public void EnablePickUpButton() {
        pickupButton.interactable = true;
    }

    public void ShowCatInteractionCanvas() {
        m_catInteractionCanvas.SetActive(true);
    }

    public void HideCatInteractionCanvas() {
        m_catInteractionCanvas.SetActive(false);
    }
}
