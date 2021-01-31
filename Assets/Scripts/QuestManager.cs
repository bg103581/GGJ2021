using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [HideInInspector] public static QuestManager instance;
    [SerializeField] public List<Quest> m_ongoingQuests = new List<Quest>();
    [SerializeField] private List<string> questIdList = new List<string>();
    [SerializeField] private List<string> questHouseList = new List<string>();

    private void Awake() {
        instance = this;
    }

    public void AddQuest(Quest quest) {
        m_ongoingQuests.Add(quest);
    }

    public void FinishCurrentQuest(GameObject house, Cat cat) {
        questIdList.Clear();
        questIdList.AddRange(Array.ConvertAll(m_ongoingQuests.ToArray(), x => x.Id));

        questHouseList.Clear();
        questHouseList.AddRange(Array.ConvertAll(m_ongoingQuests.ToArray(), x => x.getHouseId()));

        House houseComponent = house.GetComponent<House>();

        if (!questIdList.Contains(cat.Id) && !questHouseList.Contains(houseComponent.getHouseNumber())) {
            StrayCatHandler(cat);
        } else {
            if (questIdList.Contains(cat.Id)) {
                int questIndex = questIdList.IndexOf(cat.Id);

                if (m_ongoingQuests[questIndex].getHouseId().Equals(houseComponent.getHouseNumber()))
                    QuestValidated(cat, questIndex, true);
                else
                    QuestValidated(cat, questIndex, false);
            } else {
                int questIndex = questHouseList.IndexOf(cat.Id);
                QuestValidated(cat, questIndex, false);
            }
        }
    }

    private void QuestValidated(Cat cat, int questIndex, bool validated) {
        if (validated) {
            Debug.LogError("QUEST VALIDATED !");
            ReputationBar.current.AddReputation(ReputationBar.current.catScore);
        } else
            Debug.LogError("QUEST FAILED :( Make sure to properly read the description next time !");

        /* Add updateReputation here */
        Destroy(UiManager.current.OngoingQuestButtonList[questIndex]);

        int ongoingQuestIndex = Spawn.instance.AllQuestId.IndexOf(cat.Id);
        Destroy(Spawn.instance.QuestList[ongoingQuestIndex].gameObject);

        Spawn.instance.QuestList.RemoveAt(ongoingQuestIndex);
        Spawn.instance.AllQuestId.RemoveAt(ongoingQuestIndex);
        m_ongoingQuests.RemoveAt(questIndex);

        Destroy(cat.gameObject);
    }

    private void StrayCatHandler(Cat cat) {
        Debug.LogError("This cat is a stray. It doesn't belong to the owner of this house.");

        if (Spawn.instance.AllQuestId.Contains(cat.Id)) {

            int catIndex = Spawn.instance.AllQuestId.IndexOf(cat.Id);

            Destroy(Spawn.instance.QuestList[catIndex].gameObject);

            Spawn.instance.QuestList.RemoveAt(catIndex);
            Spawn.instance.AllQuestId.RemoveAt(catIndex);

            Destroy(cat.gameObject);
        } else {
            Destroy(cat.gameObject);
        }
    }
}
