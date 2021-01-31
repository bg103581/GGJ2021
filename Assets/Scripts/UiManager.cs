using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [HideInInspector] public static UiManager current;
    [SerializeField] private GameObject m_catInteractionCanvas;
    [SerializeField] private GameObject m_questInteractionCanvas;
    [SerializeField] private GameObject pickupButton;
    [SerializeField] private GameObject letGoButton;

    private void Awake() {
        current = this;
    }

    public void PickUp() {
        GameEvents.current.PickUp();
    }

    public void LetGo() {
        GameEvents.current.LetGo();
    }

    public void Caress() {
        GameEvents.current.Caress();
    }

    public void Play() {
        GameEvents.current.Play();
    }

    public void ShowInteractionButton(Image image) {
        image.enabled = true;
    }

    public void CloseInteractionButton(Image image) {
        image.enabled = false;
    }

    public void DisablePickUpButton() {
        pickupButton.SetActive(false);
        letGoButton.SetActive(true);
    }

    public void EnablePickUpButton() {
        pickupButton.SetActive(true);
        letGoButton.SetActive(false);
    }

    public void ShowCatInteractionCanvas() {
        m_catInteractionCanvas.SetActive(true);
    }

    public void HideCatInteractionCanvas() {
        m_catInteractionCanvas.SetActive(false);
    }
}
