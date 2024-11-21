using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveMonster : MonoBehaviour
{
    //싱글톤
    #region
    private static CaveMonster instance;
    public static CaveMonster Instastance { get { return instance; } }
    #endregion

    public bool isTopTrigger;

    float monsterYlocation = 2.3f;

    [SerializeField]
    float speed = 5f;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (isTopTrigger)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 1.3f);
        }
        else
        {
            transform.Translate(Vector2.up * Time.deltaTime * 1.3f);
        }
    }
}
