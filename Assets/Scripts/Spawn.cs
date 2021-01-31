using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [HideInInspector] public static Spawn instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject m_playerPrefab = null;
    [SerializeField] private GameObject m_catPrefab = null;
    [SerializeField] private GameObject m_questPrefab = null;
    [SerializeField] private HouseManager m_houseManager;

    [Header("Spawn Positions")]
    [SerializeField] private Transform m_catSpawnAreaHolder = null;
    [SerializeField] private Transform m_questSpawnAreaHolder = null;
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

    /* Possible spawn area list */
    private List<Transform> chasseurPosList = new List<Transform>();
    private List<Transform> craintifDormeurPosList = new List<Transform>();
    private List<Transform> joueurPosList = new List<Transform>();

    private void Awake() {
        instance = this;

        foreach(Transform catSpawn in m_catSpawnAreaHolder)
            m_catSpawnAreas.Add(catSpawn);

        foreach (Transform questSpawn in m_questSpawnAreaHolder)
            m_questSpawnAreas.Add(questSpawn);

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

            while (QuestList.Count < totalQuests) {
                SpawnObject(SpawnObjects.QUEST);
            }
        }
    }

    public void SpawnObject(SpawnObjects obj) {
        switch (obj) {
            case (SpawnObjects.PLAYER):
                break;

            case (SpawnObjects.CAT):
                Fur fur = (Fur)Random.Range(0, System.Enum.GetValues(typeof(Fur)).Length);
                Pattern pattern = (Pattern)Random.Range(0, System.Enum.GetValues(typeof(Pattern)).Length); ;
                Collar collar = (Collar)Random.Range(0, System.Enum.GetValues(typeof(Collar)).Length);
                Personality personality = (Personality)Random.Range(0, System.Enum.GetValues(typeof(Personality)).Length);

                string catId = ((int)fur).ToString() + ((int)pattern).ToString() + ((int)collar).ToString() + ((int)personality).ToString();

                if (!catIdList.Contains(catId)) {

                    int randomSpawnIndex;
                    Transform position;

                    switch (personality) {
                        case (Personality.HUNTER):
                            if (chasseurPosList.Count > 0) {
                                randomSpawnIndex = Random.Range(0, chasseurPosList.Count);
                                position = chasseurPosList[randomSpawnIndex];
                                chasseurPosList.RemoveAt(randomSpawnIndex);
                            } else {
                                Debug.LogError("Not enough transform position for behavior CHASSEUR !");
                                return;
                            }
                            break;

                        case (Personality.PLAYFUL):
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

                    GameObject go = Instantiate(m_catPrefab, position);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localRotation = position.rotation;
                    go.GetComponent<CatMaterialSetter>().SetMaterials(fur, pattern, collar);

                    catIdList.Add(catId);
                    Cat cat = go.GetComponent<Cat>();
                    cat.SetCat(fur, pattern, collar, personality);
                    cat.Id = catId;
                    catList.Add(cat);
                }

                break;

            case (SpawnObjects.QUEST):
                List<string> houses = m_houseManager.getHouseNumbers();

                int catIndex = Random.Range(0, catIdList.Count);
                int houseIndex = Random.Range(0, houses.Count);
                
                if (catIdList.Count > 0 && houses.Count > 0 && !questCatIdList.Contains(catIdList[catIndex]) && !questHouseList.Contains(houses[houseIndex])){
                    questCatIdList.Add(catIdList[catIndex]);
                    questHouseList.Add(houses[houseIndex]);

                    /*TO DO : Update quest description*/

                    int randomSpawnIndex = Random.Range(0, m_questSpawnAreas.Count);
                    Transform position = m_questSpawnAreas[randomSpawnIndex];

                    GameObject go = Instantiate(m_questPrefab, position);
                    go.transform.localPosition = Vector3.zero;

                    m_questSpawnAreas.RemoveAt(randomSpawnIndex);

                    Cat questCat = catList[catIndex];
                    Quest quest = go.GetComponent<Quest>();
                    quest.SetQuest(questCat, houses[houseIndex]);
                    quest.Id = questCat.Id;
                    QuestList.Add(quest);
                    AllQuestId.Add(quest.Id);
                }

                break;

            default:
                break;
        }
    }

    public List<string> AllQuestId { get; set; } = new List<string>();
    public List<Quest> QuestList { get; set; } = new List<Quest>();
}
