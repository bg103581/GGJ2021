using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [HideInInspector] public static UiManager current;
    [Header("Cat UI")]
    [SerializeField] private GameObject m_catInteractionCanvas;
    [SerializeField] private GameObject pickupButton;
    [SerializeField] private GameObject letGoButton;
    [Header("Quest UI")]
    [SerializeField] private GameObject m_questInteractionCanvas;
    [SerializeField] private GameObject m_ongoingQuestCanvas;
    [SerializeField] private GameObject m_activeQuestCanvas;
    [SerializeField] private TextMeshProUGUI m_activeQuestText;
    [SerializeField] private Transform m_ongoingQuestHolder;
    [SerializeField] private GameObject m_ongoingQuestButtonPrefab;
    [SerializeField] private TextMeshProUGUI m_questDescription;
    [Header("Player")]
    [SerializeField] private Player player;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject reputation;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelGameFinish;

    public List<GameObject> OngoingQuestButtonList { get; set; } = new List<GameObject>();
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
            GameObject go = Instantiate(m_ongoingQuestButtonPrefab, m_ongoingQuestHolder);
            go.GetComponent<QuestButton>().ButtonIndex = OngoingQuestButtonList.Count;
            OngoingQuestButtonList.Add(go);

            player.quest.gameObject.SetActive(false);
            player.ShowQuestInteraction(false);
        } else {
            player.ShowQuestInteraction(false);
        }
    }

    public void OpenOngoingQuestList() {
        m_ongoingQuestCanvas.SetActive(!m_ongoingQuestCanvas.activeInHierarchy);

        if (m_ongoingQuestCanvas.activeInHierarchy)
            m_activeQuestCanvas.SetActive(false);
    }

    public void OpenQuest(int i) {
        m_activeQuestText.text = "";
        m_activeQuestCanvas.SetActive(true);
        m_ongoingQuestCanvas.SetActive(false);

        m_activeQuestText.text = QuestManager.instance.m_ongoingQuests[i].getDescription();
    }

    public void MainToInGame() {
        panelMainMenu.SetActive(false);
        hud.SetActive(true);
        reputation.SetActive(true);
    }

    public void QuitButton() {
        Application.Quit();
    }

    public void InGameToPause() {
        panelPause.SetActive(true);
        hud.SetActive(false);
    }

    public void PauseToInGame() {
        panelPause.SetActive(false);
        hud.SetActive(true);
    }

    public void InGameToFinish() {
        hud.SetActive(false);
        panelGameFinish.SetActive(true);
    }
    #endregion
}
