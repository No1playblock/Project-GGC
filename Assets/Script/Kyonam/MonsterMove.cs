using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    private Monster theTargetMonster = null;
    private Vector3 theDestination = Vector3.zero;

    private int speed = 3;
    private bool isTurn = false;
    
    public IEnumerator GoDown()
    {
        float distance = 100f;
        float halfDistance = Vector3.Distance(theTargetMonster.TheDestination.transform.position, transform.position) / 2f;

        while (distance > 1.0f)
        {
            distance = Vector3.Distance(theTargetMonster.TheDestination.transform.position, transform.position);

            if (distance <= halfDistance && !isTurn)
            {
                isTurn = true;
                theTargetMonster.GetNewDestination();
            }

            Vector3 temp = theTargetMonster.TheDestination.transform.position - transform.position;
            transform.Translate(temp.normalized * Time.deltaTime * speed);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
