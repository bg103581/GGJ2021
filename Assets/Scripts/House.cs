using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private string houseNumber = "";
    [SerializeField] public TextMeshProUGUI houseNumberText = null;
    public string getHouseNumber() { return houseNumber; }
    public void setHouseNumber(string id) { houseNumber = id; }
}
