using System.Collections;
using UnityEngine;

public class MonsterBullet : MonoBehaviour{

    #region Values
    private Vector3 startPos;   // 총알 출발 좌표
    [SerializeField]
    private float speed = 20f;  // 총알 날아가는 속도
    private int damage;         // 총알 데미지
    #endregion

    #region Components
    [SerializeField]
    private Vector3 targetPos;    // 향하는 좌표
    private Vector3 normalizedTargetPos;    // 향하는 좌표의 정규화된 값 * 5
    [SerializeField]
    private GameObject targetObj;   // 공격할 객체
    #endregion
    public static bool isHit = false;

    public void StartShooting(Vector3 _startPos, Vector3 _targetPos, GameObject _target, int _damage)
    {
        //speed = 7f;
        transform.eulerAngles = Vector3.zero;
        startPos = _startPos;
        targetPos = _targetPos;
        normalizedTargetPos = targetPos.normalized * 3;
        targetObj = _target;
        damage = _damage;

        StartChasingTarget();

        return;
    }
    public void StartShootFront(Vector3 _startPos, Vector3 _rotation, GameObject _target, int _damage)
    {
        startPos = _startPos;
        transform.eulerAngles = _rotation;
        targetPos = Vector3.up;
        normalizedTargetPos = targetPos.normalized * 3;
        targetObj = _target;
        damage = _damage;

        StartChasingTarget();

        return;
    }
    private void StartChasingTarget()
    {
        if (startPos == null || targetPos == null)
            return;
        
        StartCoroutine(StartBulletSequence());
        StartCoroutine(TimeLimit());
    }

    private IEnumerator StartBulletSequence()
    {
        yield return StartCoroutine(ChaseTarget()); // 대상 쫒아가기 시작. 쫒아가는게 끝날 때 까지 코루틴이 멈춰있음.
                                                    //targetObj.Damage(damage);
        if (PlayerMoveScript.Spawning)
        {
            GameObject.Find("Player").GetComponent<PlayerMoveScript>().playerHit();
            PlayerMoveScript.Spawning = false;
        }
        DestroyBullet();
    }

    private IEnumerator TimeLimit()
    {
        yield return new WaitForSeconds(7.0f);
        DestroyBullet();
    }

    private IEnumerator ChaseTarget()
    {
        transform.position = startPos;

        Vector3 temp = targetPos - transform.position;

        while (CalculateDistance(targetObj.transform.position) >= 0.2f)
        {
            transform.Translate(temp.normalized * Time.deltaTime * speed);
            yield return null;
        }
       
    }
    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
    private float CalculateDistance(Vector3 _target)
    {
        return Vector3.Distance(transform.position, _target);
    }
    private void OnBecameInvisible()
    {
        DestroyBullet();
    }
}
