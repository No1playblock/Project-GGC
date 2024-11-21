using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{

    private float bulletspeed = 2.0f;
    private bool stop;

    private void Start()
    {
        bulletspeed = 10.0f;
    }
    
    public IEnumerator Horizonshoot()
    {
        stop = true;
        StartCoroutine(count());
        while (stop)
        {
            yield return null;
            this.transform.Translate(new Vector2(0, bulletspeed * Time.deltaTime));
        }
    }
    IEnumerator count()
    {

        yield return new WaitForSeconds(3.0f);
        stop = false;
        this.gameObject.SetActive(false);
    }

}
