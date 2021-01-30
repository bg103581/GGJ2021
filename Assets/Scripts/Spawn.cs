using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject m_playerPrefab = null;
    [SerializeField] private GameObject m_catPrefab = null;
    [SerializeField] private GameObject m_questPrefab = null;
    [SerializeField] private HouseManager m_houseManager;
    [SerializeField] private List<Transform> m_spawnAreas;

    private List<int> spawnAreaIndex = new List<int>();
    private List<string> catIdList = new List<string>();
    private List<string> questCatIdList = new List<string>();
    private List<string> questHouseList = new List<string>();

    private List<Cat> catList = new List<Cat>();
    private List<Quest> questList = new List<Quest>();

    private void Awake() {
        int randomSpawnIndex;

        for(int i = 0; i < 3; i++) {
            randomSpawnIndex = Random.Range(0, m_spawnAreas.Count);
            
            while (spawnAreaIndex.Contains(randomSpawnIndex)) {
                randomSpawnIndex = Random.Range(0, m_spawnAreas.Count);
            }

            spawnAreaIndex.Add(randomSpawnIndex);
            SpawnObject(SpawnObjects.CAT, m_spawnAreas[randomSpawnIndex]);
        }
    }

    public void SpawnObject(SpawnObjects obj, Transform position) {
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

                if (!questCatIdList.Contains(catIdList[catIndex]) && !questHouseList.Contains(houses[houseIndex])) {
                    questCatIdList.Add(catIdList[catIndex]);
                    questHouseList.Add(houses[houseIndex]);

                    Cat questCat = catList[catIndex];
                    Quest quest = new Quest(questCat, houses[houseIndex]);
                    questList.Add(quest);

                    /*TO DO : Update quest description*/

                    Instantiate(m_questPrefab, position);
                }


                break;

            default:
                break;
        }
    }
}
