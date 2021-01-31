using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationBar : MonoBehaviour
{
    public static ReputationBar current;

    [SerializeField] private Slider slider;

    public int nbQuests;
    public int catScore = 75;
    public int bonusScore = 25;

    private void Awake() {
        current = this;
    }

    private void Start() {
        nbQuests = Spawn.instance.QuestList.Count;
        SetMaxReputation(nbQuests * (catScore + bonusScore));
    }

    public void SetMaxReputation(int reput) {
        slider.maxValue = reput;
        slider.value = 0;
    }

    public void SetReputation(int reput) {
        slider.value = reput;
    }

    public void AddReputation(int reput) {
        int sum = (int)slider.value + reput;
        if (sum <= slider.maxValue)
            slider.value += reput;
    }

    public void SetNbQuests(int nb) {
        nbQuests = nb;
    }
}
