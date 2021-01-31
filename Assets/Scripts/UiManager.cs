using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [HideInInspector] public static UiManager current;
    [SerializeField] private GameObject m_catInteractionCanvas;
    [SerializeField] private GameObject m_questInteractionCanvas;
    [SerializeField] private TextMeshProUGUI m_questDescription;
    [SerializeField] private GameObject pickupButton;
    [SerializeField] private GameObject letGoButton;
    [SerializeField] private Player player;

    private void Awake() {
        current = this;
    }

    #region Player Actions
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
    #endregion

    #region Interaction Button
    public void ShowInteractionButton(Image image, bool show) {
        image.enabled = show;
    }
    #endregion

    #region Canvas Management
    public void EnablePickUpButton(bool show) {
        pickupButton.SetActive(show);
        letGoButton.SetActive(!show);
    }

    public void ShowCatInteractionCanvas(bool show) {
        m_catInteractionCanvas.SetActive(show);
    }

    public void ShowQuestCanvas(bool show, Quest quest = null) {
        m_questInteractionCanvas.SetActive(show);

        if (show && quest != null) {
            m_questDescription.text = quest.getDescription();
        } else
            m_questDescription.text = "";
    }

    public void AcceptQuest(bool accept) {
        if (accept) {
            ShowQuestCanvas(false);
            QuestManager.instance.AddQuest(player.quest);
            player.quest.gameObject.SetActive(false);
            player.ShowQuestInteraction(false);
        } else {
            player.ShowQuestInteraction(false);
        }
    }

    public void OpenOngoingQuest() {

    }
    #endregion
}
