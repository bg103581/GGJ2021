using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    [SerializeField] public int ButtonIndex;

    private Button thisButton;

    private void Awake() {
        thisButton = GetComponent<Button>();
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(() => OpenQuest());
    }

    private void OpenQuest() {
        UiManager.current.OpenQuest(ButtonIndex);
    }

}
