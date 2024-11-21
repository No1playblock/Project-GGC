using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    [SerializeField]
    private int shootingCount = 10;

    public bool IsMiddleBoss = true;
    
    public Transform n, n1, n2 = null;
    public Transform[] upperPoints = null;
    public GameObject bomb1, bomb2;
    public GetItemScript myReward;

    
    void Start()
    {
        theTarget = theMonsterManager.TheTarget;
        bomb1.SetActive(false);
        bomb2.SetActive(false);
        StartCoroutine(LiveLifeCycle());
    }
    public new IEnumerator LiveLifeCycle()
    {
        if (IsMiddleBoss)
        {
            Debug.Log("Middle");
            StartCoroutine(Pattern4());
            yield return new WaitForSeconds(10f);
            StartCoroutine(Pattern5());
            yield return new WaitForSeconds(10f);
        }
        else
        {
            Debug.Log("Mega");
            StartCoroutine(Pattern1());
            yield return new WaitForSeconds(10f);
            StartCoroutine(Pattern2());
            yield return new WaitForSeconds(10f);
            StartCoroutine(Pattern3());
            yield return new WaitForSeconds(15f);
        }
        StartCoroutine(LiveLifeCycle());
        yield return null;
    }

    private IEnumerator Pattern1()
    {
        StartCoroutine(ShootBulletFewTimes(4, 1f));
        yield return StartCoroutine(GoToDestination(transform, n.position, 4.0f));
        yield return StartCoroutine(GoToDestination(transform, n1.position, 4.0f));
    }

    private IEnumerator Pattern2()
    {
        yield return StartCoroutine(GoToDestination(transform, n2.position, 2.0f));
        yield return StartCoroutine(ScatterBullet(transform.position, 2));
        yield return new WaitForSeconds(3.0f);
        yield return StartCoroutine(GoToDestination(transform, n1.position, 2.0f));
    }

    private IEnumerator Pattern3()
    {
        yield return StartCoroutine(GoToDestination(transform, upperPoints[0].position, 2.0f));
        yield return StartCoroutine(GoToDestination(transform, upperPoints[1].position, 2.0f));
        StartCoroutine(ThrowBomb(bomb1));
        yield return StartCoroutine(GoToDestination(transform, upperPoints[2].position, 2.0f));
        StartCoroutine(ThrowBomb(bomb2));
        yield return StartCoroutine(GoToDestination(transform, upperPoints[3].position, 2.0f));
        yield return StartCoroutine(GoToDestination(transform, upperPoints[4].position, 2.0f));
        yield return StartCoroutine(GoToDestination(transform, n1.position, 3.0f));
    }


    private IEnumerator Pattern4()
    {
        StartCoroutine(GoToDestination(transform, n.position, 1f ));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ScatterBulletOnTick(transform.position, 1, 0));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ScatterBulletOnTick(transform.position, 1, 2));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ScatterBulletOnTick(transform.position, 1, 4));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ScatterBulletOnTick(transform.position, 1, 6));
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(ScatterBulletOnTick(transform.position, 1, 8));
    }
    private IEnumerator Pattern5()
    {
        StartCoroutine(ScatterBulletOnePath(transform.position, Random.Range(-2, 3)));
        yield return null;
    }
    public IEnumerator GoToDestination(Transform _targetTransform, Vector3 _position, float _time)
    {
        Vector3 temp = _targetTransform.position;
        for (float f = 0; f < _time; f += Time.deltaTime)
        {
            _targetTransform.position = Vector3.Lerp(temp, _position, f / _time);
            yield return null;
        }
        transform.position = _position;
        yield return null;
    }
    public IEnumerator ShootBulletFewTimes(int _count, float _time)
    {
        yield return null;
        for(int i = 0; i < _count; i++)
        {
            ShootBullet();
            yield return new WaitForSeconds(_time);
        }
    }
    public IEnumerator ScatterBullet(Vector3 _startPos, int _count)
    {
        float angle = 360 / shootingCount;
        float tempAngle = 180;

        yield return null;
        for (int i = 0; i < _count; i++)
        {
            angle = 360 / shootingCount;
            for (int j = 0; j < shootingCount; j++)
            {
                MonsterBulletPool.Instance.GetObject().StartShootFront(_startPos, new Vector3(0, 0, tempAngle), theTarget, 0);
                tempAngle -= angle;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
    public IEnumerator ScatterBulletOnTick(Vector3 _startPos, int _count, int _seed)
    {
        float angle = 360 / shootingCount;
        float tempAngle = 180 + _seed;

        yield return null;
        for (int i = 0; i < _count; i++)
        {
            angle = 360 / shootingCount;
            for (int j = 0; j < shootingCount; j++)
            {
                MonsterBulletPool.Instance.GetObject().StartShootFront(_startPos, new Vector3(0, 0, tempAngle), theTarget, 0);
                tempAngle -= angle;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    public IEnumerator ScatterBulletOnePath(Vector3 _startPos, int _seed)
    {
        float angle = 360 / 180;
        float tempAngle = _seed;

        angle = 360 / 180;

        for (int j = 2; j < 180; j++)
        {
            MonsterBulletPool.Instance.GetObject().StartShootFront(_startPos, new Vector3(0, 0, tempAngle), theTarget, 0);
            tempAngle -= angle;
        }
        yield return null;
    }
    public IEnumerator ThrowBomb(GameObject bomb)
    {
        bomb.SetActive(true);
        bomb.transform.position = transform.position;
        yield return StartCoroutine(GoToDestination(bomb.transform, theTarget.transform.position, 3.0f));
        StartCoroutine(ScatterBulletOnTick(bomb.transform.position, 1, 0));
        bomb.SetActive(false);
        yield return null;
    }
    public void OnDisable()
    {
        DropItem();
    }
    private void DropItem()
    {
        if (ChangeViewManage.Instastance.isTopView)
            myReward.transform.position = Vector3.down * 8;
        else
            myReward.transform.position = Vector3.down * 4;
        myReward.gameObject.SetActive(true);
    }
}