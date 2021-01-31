using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private string[] descriptionStyles =
        { "J'ai perdu mon chat {0}, il est de couleur {1} avec une personnalité {2}."};

    private string description;
    private Cat catQuest;
    private string ownerHouse;

    [SerializeField] public string Id;

    public void SetQuest(Cat cat, string houseNumber) {
        description = descriptionStyles[getRandomDescriptionIndex()];
        catQuest = cat;
        ownerHouse = houseNumber;
    }

    private int getRandomDescriptionIndex() { return Random.Range(0, descriptionStyles.Length); }

    #region GETTERS AND SETTERS
    public string getDescription() { return description; }
    public Cat getCat() { return catQuest; }
    public string getOwnerHouser() { return ownerHouse; } 

    #endregion
}
