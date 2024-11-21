using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] backgroundSprite = new SpriteRenderer[4];
    [SerializeField]
    float[] backgroundScrollSpeed = new float[4];

    private void Update()
    {
        backgroundSprite[0].size += new Vector2(backgroundScrollSpeed[0] * Time.deltaTime, 0);
        backgroundSprite[1].size += new Vector2(backgroundScrollSpeed[1] * Time.deltaTime, 0);
        backgroundSprite[2].size += new Vector2(backgroundScrollSpeed[2] * Time.deltaTime, 0);
        backgroundSprite[3].size += new Vector2(backgroundScrollSpeed[3] * Time.deltaTime, 0);
    }
}
