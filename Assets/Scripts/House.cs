using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private string houseNumber = "";
    public string getHouseNumber() { return houseNumber; }
    public void setHouseNumber(string id) { houseNumber = id; }
}
