﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemScript : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 1.0f)
        {
            PlayerShootScript.getItem = true;
            gameObject.SetActive(false);
            StageManager.Instance.NextStage();
        }
    }
}
