using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public IEnumerator IllustTop_ViewMove()
    {
        while (this.transform.position.x<=-0.05f)
        {
            yield return null;
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(0, 0, 0), 0.05f);    
        }
        Debug.Log("1차");
        yield return new WaitForSeconds(0.05f);
        while (this.transform.position.x <= 13.9f)
        {
           
            yield return null;

            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(14, 21, 0), 0.05f);
            Debug.Log("do");
        }
        this.transform.position = new Vector3(-14, -21, 0);
        

    }
    public IEnumerator IllustWide_ViewMove()
    {
        while (this.transform.position.x <= -0.05f)
        {
            yield return null;
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(0, 0, 0), 0.05f);
        }
        Debug.Log("1차");
        yield return new WaitForSeconds(0.05f);
        while (this.transform.position.x <= 20.9f)
        {

            yield return null;

            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(21, -14, 0), 0.05f);
            Debug.Log("do");
        }
        this.transform.position = new Vector3(-21, 14, 0);
    }

}
