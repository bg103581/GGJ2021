using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private string[] catNames = {"Pepito", "Lulu", "Michelle", "Jacquie", "Braveheart", "Pebbles", 
        "Joselito", "Maimai", "Yoda", "Jeanine", "Java", "Millie", "Rourou", "Boomer", "Bianca", 
        "Mousse", "Nova", "Lily", "JUL", "Sardosh", "Pookie", "Bubble", "Muffin", "Ballz", "Deez",
        "BabyYoda", "Kylo", "Ren", "Thrall", "Arthas", "Tyriande", "Malfurion", "Illidan", "Sylvanas", "ZogZog"};

    private string description;
    private Cat catQuest;
    private string ownerHouse;
    private int radomNameIndex = 0;

    [SerializeField] public string Id;

    public void SetQuest(Cat cat, string houseNumber) {
        string name = getRandomName();

        description = string.Format("My cat {0} is missing !\n\n" +
            "He is {1} with white spots on his {2}, and wears a {3} collar.\n\n" +
            "He is quite a {4} cat.\n\n" +
            "If you find him, please return him at house {5}. Thank you !", name, cat.getFur(), cat.getPattern(), cat.getCollar(), cat.getPersonality(), houseNumber);

        catQuest = cat;
        ownerHouse = houseNumber;
    }

    private string getRandomName() {
        radomNameIndex = Random.Range(0, catNames.Length);
        return catNames[radomNameIndex]; }

    #region GETTERS AND SETTERS
    public string getDescription() { return description; }
    public Cat getCat() { return catQuest; }
    public string getHouseId() { return ownerHouse; } 

    #endregion
}
