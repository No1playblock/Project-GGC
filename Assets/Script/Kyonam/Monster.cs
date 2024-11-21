using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    protected MonsterManager theMonsterManager = null;
    [SerializeField]
    protected MonsterMove theMonsterMove = null;
    [SerializeField]
    protected Monster theFollowingMonster = null;
    protected GameObject theTarget = null;
    private Transform theDestination = null;

    [SerializeField]
    private int hp = 2;
    private int hpAsStat = 2;

    public Transform TheDestination { get => theDestination; }

    public void InitValue(MonsterManager _theMonsterManager, int _hp, Transform _theDestination, GameObject _theTarget)
    {
        theMonsterManager = _theMonsterManager;
        hp = _hp;
        hpAsStat = _hp;
        theDestination = _theDestination;
        theTarget = _theTarget;
    }
    public void AddFollowingMonster(Monster _theFollowingMonster, Vector3 _position)
    {
        if (this == _theFollowingMonster)
            return;

        theFollowingMonster = _theFollowingMonster;
        
        transform.parent = _theFollowingMonster.transform;
        transform.localPosition = _position;
    }
    public IEnumerator LiveLifeCycle()
    {
        gameObject.SetActive(true);

        StartCoroutine(ShootBulletSequence());

        if (theFollowingMonster == null)
            StartCoroutine(theMonsterMove.GoDown());
        else
        {
            yield return new WaitWhile(() => theFollowingMonster.gameObject.activeInHierarchy == true);
            transform.SetParent(theMonsterManager.TheMonsterParent.transform);
            gameObject.SetActive(false);
        }
    }

    public void GetNewDestination()
    {
        theDestination = theMonsterManager.TheDestPoint[Random.Range(0, theMonsterManager.TheDestPoint.Length)];
    }

    private IEnumerator ShootBulletSequence()
    {
        yield return new WaitForSeconds(3f);
        ShootBullet();
        StartCoroutine(ShootBulletSequence());
    }
    protected void ShootBullet()
    {
        MonsterBulletPool.Instance.GetObject().StartShooting(transform.position, theTarget.transform.position, theTarget, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            hp--;
            if (hp <= 0)
                Dead();
            other.gameObject.SetActive(false);
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        hp = hpAsStat;

        if (this is Boss)
        {
            return;
        }

        MonsterManager.Instance.MonsterKillCount++;
        if (MonsterManager.Instance.MonsterKillCount >= 15)
            StageManager.Instance.SpawnBoss();
        ScoreManager.Instance.AddScore(100);
    }
}
