using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCaveBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌");
        Instantiate(collision.gameObject, new Vector3(15f, collision.gameObject.transform.position.y
            , -6), Quaternion.identity);
        Destroy(collision.gameObject);
    }
}
