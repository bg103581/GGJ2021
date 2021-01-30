using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab = null;
    [SerializeField] private GameObject CatPrefab = null;
    [SerializeField] private GameObject QuestPrefab = null;

    private List<string> catIdList = new List<string>();
    private List<string> questCatIdList = new List<string>();
    private List<Cat> catList = new List<Cat>();

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

                    Instantiate(CatPrefab, position);
                }

                break;

            case (SpawnObjects.QUEST):
                int catIndex = Random.Range(0, catIdList.Count);

                if (!questCatIdList.Contains(catIdList[catIndex])) {
                    questCatIdList.Add(catIdList[catIndex]);

                    Cat questCat = catList[catIndex];

                    Quest quest = new Quest(questCat);
                }
               

                break;

            default:
                break;
        }
    }
}
