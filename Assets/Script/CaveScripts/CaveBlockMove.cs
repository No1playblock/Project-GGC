using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBlockMove : MonoBehaviour
{
    float speed = 4.5f;

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Equals("Up"))
        {
            CaveMonster.Instastance.isTopTrigger = true;
        }
        else if (collision.gameObject.name.Equals("Down"))
        {
            CaveMonster.Instastance.isTopTrigger = false;
        }

    }
}
