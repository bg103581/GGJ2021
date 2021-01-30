﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject m_playerPrefab = null;
    [SerializeField] private GameObject m_catPrefab = null;
    [SerializeField] private GameObject m_questPrefab = null;
    [SerializeField] private HouseManager m_houseManager;

    [Header("Spawn Positions")]
    [SerializeField] private List<Transform> m_catSpawnAreas = new List<Transform>();
    [SerializeField] private List<Transform> m_questSpawnAreas = new List<Transform>();

    [Header("Initial Values")]
    [SerializeField] private int totalCats;
    [SerializeField] private int totalQuests;

    /* Id list */
    private List<string> catIdList = new List<string>(); // To make sure cat IDs are unique.
    private List<string> questCatIdList = new List<string>(); // To make sure quest cats are unique.
    private List<string> questHouseList = new List<string>();

    /* Class list */
    private List<Cat> catList = new List<Cat>(); // Used to associate quests with their corresponding cats.
    private List<Quest> questList = new List<Quest>();

    /* Possible spawn area list */
    private List<Transform> chasseurPosList = new List<Transform>();
    private List<Transform> craintifDormeurPosList = new List<Transform>();
    private List<Transform> joueurPosList = new List<Transform>();

    private void Awake() {

        if (totalCats < totalQuests) {
            Debug.LogError("Total number of quests depasses the number of total cats ! Make sure you have at least the same number of both.");
            return;
        } else {
            chasseurPosList.AddRange(System.Array.FindAll(m_catSpawnAreas.ToArray(), x => x.CompareTag("Tree") || x.CompareTag("Bush")));
            craintifDormeurPosList.AddRange(System.Array.FindAll(m_catSpawnAreas.ToArray(), x => x.CompareTag("Carton") || x.CompareTag("Car") || x.CompareTag("Bench") || x.CompareTag("CarRoof")));
            joueurPosList.AddRange(System.Array.FindAll(m_catSpawnAreas.ToArray(), x => x.CompareTag("Trashcan")));

            while (catIdList.Count < totalCats) {
                SpawnObject(SpawnObjects.CAT);
            }

            while (questList.Count < totalQuests) {
                SpawnObject(SpawnObjects.QUEST);
            }
        }
    }

    public void SpawnObject(SpawnObjects obj) {
        switch (obj) {
            case (SpawnObjects.PLAYER):
                break;

            case (SpawnObjects.CAT):
                CatColor color = (CatColor)Random.Range(0, System.Enum.GetValues(typeof(CatColor)).Length);
                CatMark mark = (CatMark)Random.Range(0, System.Enum.GetValues(typeof(CatMark)).Length); ;
                CatNeckline neckline = (CatNeckline)Random.Range(0, System.Enum.GetValues(typeof(CatNeckline)).Length);
                CatBehavior behavior = (CatBehavior)Random.Range(0, System.Enum.GetValues(typeof(CatBehavior)).Length);

                string catId = ((int)color).ToString() + ((int)mark).ToString() + ((int)neckline).ToString() + ((int)behavior).ToString();

                if (!catIdList.Contains(catId)) {

                    int randomSpawnIndex;
                    Transform position;

                    switch (behavior) {
                        case (CatBehavior.CHASSEUR):
                            if (chasseurPosList.Count > 0) {
                                randomSpawnIndex = Random.Range(0, chasseurPosList.Count);
                                position = chasseurPosList[randomSpawnIndex];
                                chasseurPosList.RemoveAt(randomSpawnIndex);
                            } else {
                                Debug.LogError("Not enough transform position for behavior CHASSEUR !");
                                return;
                            }
                            break;

                        case (CatBehavior.JOUEUR):
                            if (joueurPosList.Count > 0) {
                                randomSpawnIndex = Random.Range(0, joueurPosList.Count);
                                position = joueurPosList[randomSpawnIndex];
                                joueurPosList.RemoveAt(randomSpawnIndex);
                            } else {
                                Debug.LogError("Not enough transform position for behavior JOUEUR !");
                                return;
                            }
                            break;

                        default:
                            if (craintifDormeurPosList.Count > 0) {
                                randomSpawnIndex = Random.Range(0, craintifDormeurPosList.Count);
                                position = craintifDormeurPosList[randomSpawnIndex];
                                craintifDormeurPosList.RemoveAt(randomSpawnIndex);
                            } else {
                                Debug.LogError("Not enough transform position for behavior CRAINTIF/DORMEUR !");
                                return;
                            }
                            break;
                    }

                    catIdList.Add(catId);
                    Cat cat = new Cat(color, mark, neckline, behavior) {
                        Id = catId
                    };
                    catList.Add(cat);

                    /*TO DO : Change color, mark, neckline and behavior texture of CatPrefab to be the same as cat*/

                    GameObject go = Instantiate(m_catPrefab, position);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localRotation = position.rotation;
                }

                break;

            case (SpawnObjects.QUEST):
                List<string> houses = m_houseManager.getHouseNumbers();

                int catIndex = Random.Range(0, catIdList.Count);
                int houseIndex = Random.Range(0, houses.Count);
                
                if (catIdList.Count > 0 && houses.Count > 0 && !questCatIdList.Contains(catIdList[catIndex]) && !questHouseList.Contains(houses[houseIndex])){
                    questCatIdList.Add(catIdList[catIndex]);
                    questHouseList.Add(houses[houseIndex]);

                    Cat questCat = catList[catIndex];
                    Quest quest = new Quest(questCat, houses[houseIndex]);
                    questList.Add(quest);

                    /*TO DO : Update quest description*/

                    int randomSpawnIndex = Random.Range(0, m_questSpawnAreas.Count);
                    Transform position = m_questSpawnAreas[randomSpawnIndex];

                    GameObject go = Instantiate(m_questPrefab, position);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localRotation = new Quaternion(0,0,0,0);

                    m_questSpawnAreas.RemoveAt(randomSpawnIndex);
                }

                break;

            default:
                break;
        }
    }
}
