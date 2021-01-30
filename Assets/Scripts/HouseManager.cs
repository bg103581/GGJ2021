using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [SerializeField] private List<House> m_houses;

    private List<string> houseNumbers = new List<string>();
    private string[] Alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    #region Unity methods
    private void Awake() {

        string id;

        foreach (House house in m_houses) {
            id = Random.Range(100, 200).ToString() + Alphabet[Random.Range(0, Alphabet.Length)];

            while (houseNumbers.Contains(id)) {
                id = Random.Range(100, 200).ToString() + Alphabet[Random.Range(0, Alphabet.Length)];
            }

            houseNumbers.Add(id);
            house.setHouseNumber(id);
        }

    }
    #endregion

    #region GETTERS AND SETTERS
    public List<string> getHouseNumbers() { return houseNumbers; }
    #endregion
}
