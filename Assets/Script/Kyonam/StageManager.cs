using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 세로 가로 세로 가로 세로
 * 평원 동굴 평원 동굴 평원
 * 중간 중간 중간 중간 최종
 * 
 */

public class StageManager : Singleton<StageManager>
{
    public int stage = 0;
    [SerializeField]
    private ChangeViewManage theChangeViewManage;
    [SerializeField]
    private Boss[] theBosses;
    public GameObject[] places;
    public GameObject scoreUI;

    public void PlayerDead()
    {
        scoreUI.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void NextStage()
    {
        MonsterManager.Instance.IsAbleToMakeMonster = true;
        MonsterManager.Instance.MonsterKillCount = 0;
        stage++;
        ChangePlaces(stage % 2);
        theChangeViewManage.Flip(1 - stage % 2);
    }
    public void SpawnBoss()
    {
        MonsterManager.Instance.IsAbleToMakeMonster = false;
        theBosses[stage].InitValue(MonsterManager.Instance, 100, theBosses[stage].transform, MonsterManager.Instance.TheTarget);
        theBosses[stage].gameObject.SetActive(true);
    }
    private void ChangePlaces(int _number)
    {
        places[_number].SetActive(true);
        places[1 - _number].SetActive(false);
    }
}
