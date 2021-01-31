using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [HideInInspector] public static QuestManager instance;
    [SerializeField] private List<Quest> m_ongoingQuests = new List<Quest>();

    private void Awake() {
        instance = this;
    }

    public void AddQuest(Quest quest) {
        m_ongoingQuests.Add(quest);
    }
}
