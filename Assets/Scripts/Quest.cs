using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private string[] descriptionStyles =
        { "J'ai perdu mon chat {0}, il est de couleur {1} avec une personnalité {2}."};

    private string description;
    private Cat catQuest;
    private Transform ownerHouse;

    public Quest(Cat cat, Transform house) {
        description = descriptionStyles[getRandomDescriptionIndex()];
        catQuest = cat;
        ownerHouse = house;
    }

    private int getRandomDescriptionIndex() { return Random.Range(0, descriptionStyles.Length); }

    #region GETTERS AND SETTERS
    public string getDescription() { return description; }
    public Cat getCat() { return catQuest; }

    #endregion
}
