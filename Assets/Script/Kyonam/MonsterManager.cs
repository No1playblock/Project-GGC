using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    #region Components
    [SerializeField]
    private List<Monster> theMonsterList = null;

    [SerializeField]
    private GameObject theTarget = null;

    [SerializeField]
    private Monster[] theMonsterModel = null;
    [SerializeField]
    private GameObject theMonsterParent = null;

    [SerializeField]
    private Transform[] theSpawnPoint = null;
    [SerializeField]
    private Transform[] theDestPoint = null;
    #endregion

    #region Values
    [SerializeField]
    private int activeCount;

    private int monsterKillCount = 0;
    #endregion

    #region Properties
    public int MonsterKillCount { get => monsterKillCount; set => monsterKillCount = value; }
    public bool IsBossDead { get; set; }
    public bool IsAbleToMakeMonster { get; set; }
    public Transform[] TheSpawnPoint { get => theSpawnPoint; }
    public Transform[] TheDestPoint { get => theDestPoint; }
    public GameObject TheMonsterParent { get => theMonsterParent; }
    public GameObject TheTarget { get => theTarget; }
    #endregion
    public void ImplementStart()
    {
        StartSceneScript.isStart = false;
        theMonsterParent = new GameObject();
        theMonsterParent.name = "Monsters";

        IsAbleToMakeMonster = true;

        for (int i = 0; i < 20; i++)
        {
            GameObject temp = Instantiate(theMonsterModel[Random.Range(0, theMonsterModel.Length)].gameObject, theMonsterParent.transform);
            temp.name = "Monster_" + i;
            Monster monsterScript = temp.GetComponent<Monster>();
            monsterScript.InitValue(this, 7, theDestPoint[Random.Range(0, theDestPoint.Length)], theTarget);
            monsterScript.transform.position = theSpawnPoint[Random.Range(0, theSpawnPoint.Length)].position;
            monsterScript.gameObject.SetActive(false);

            theMonsterList.Add(monsterScript);
        }
        activeCount = 0;
        StartCoroutine(MonsterSpawningCycle());

    }

    private void Update()
    {
        if (StartSceneScript.isStart)
        {
            ImplementStart();
        }
    }
    private void Start()
    {
        if (StartSceneScript.isStart)
        {
            theMonsterParent = new GameObject();
            theMonsterParent.name = "Monsters";

            IsAbleToMakeMonster = true;

            for (int i = 0; i < 20; i++)
            {
                GameObject temp = Instantiate(theMonsterModel[Random.Range(0, theMonsterModel.Length)].gameObject, theMonsterParent.transform);
                temp.name = "Monster_" + i;
                Monster monsterScript = temp.GetComponent<Monster>();
                monsterScript.InitValue(this, 2, theDestPoint[Random.Range(0, theDestPoint.Length)], theTarget);
                monsterScript.transform.position = theSpawnPoint[Random.Range(0, theSpawnPoint.Length)].position;
                monsterScript.gameObject.SetActive(false);

                theMonsterList.Add(monsterScript);
            }
            activeCount = 0;
            StartCoroutine(MonsterSpawningCycle());
        }
    }

    private IEnumerator MonsterSpawningCycle()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsAbleToMakeMonster);
            SpawnMonster();
            yield return new WaitForSeconds(2.0f);
        }
    }

    private void SpawnMonster()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
            case 1:
            case 2:
                {
                    Monster tempMonster = theMonsterList[activeCount++];

                    if (activeCount >= theMonsterList.Count)
                        activeCount = 0;

                    tempMonster.transform.position = theSpawnPoint[Random.Range(0, theSpawnPoint.Length)].position;

                    StartCoroutine(tempMonster.LiveLifeCycle());
                }
                break;
            case 3:
                {
                    Monster mainMonster = theMonsterList[activeCount++];

                    if (activeCount >= theMonsterList.Count)
                        activeCount = 0;

                    mainMonster.transform.position = theSpawnPoint[Random.Range(0, theSpawnPoint.Length)].position;

                    StartCoroutine(mainMonster.LiveLifeCycle());

                    for (int i = 0; i < 2; i++)
                    {
                        Monster tempMonster = theMonsterList[activeCount++];

                        if (activeCount >= theMonsterList.Count)
                            activeCount = 0;

                        tempMonster.transform.position = theSpawnPoint[Random.Range(0, theSpawnPoint.Length)].position;

                        tempMonster.AddFollowingMonster(mainMonster, Vector3.right * (i - 0.5f) * 2f);

                        StartCoroutine(tempMonster.LiveLifeCycle());
                    }
                }
                break;
        }
    }
}
