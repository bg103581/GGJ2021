using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private int nbQuests = 5;
    private int catScore = 75;
    private int bonusScore = 25;

    private void Start() {
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
}
