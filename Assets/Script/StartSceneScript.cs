using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartSceneScript : MonoBehaviour
{
    [SerializeField]
    private Text playText = null;
   
    public GameObject player;
    public static bool isStart = false;
    private bool firstInput = true;
    public Camera maincamera;

    public GameObject Logo;

    public PlayerMoveScript thePlayerMove;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(ContinueClick());
    }

    private void Update()
    {
        if (Input.anyKeyDown && firstInput)
        {
            //StartCoroutine(OpenCurtain(!isCurtainOpen));
            //player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 10.0f), 0.01f);
            firstInput = false;
            
            StartCoroutine(MoveUP());
            
            //StartCoroutine(MoveDown());
        }
    }
    IEnumerator MoveUP()
    {
        while (player.transform.position.y<= 1.87)
        {
            yield return null;
            player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(player.transform.position.x, 1.99f), 0.05f);

        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(MoveDown());
    }
    IEnumerator MoveDown()
    {
        Logo.gameObject.SetActive(false);
        while (player.transform.position.y >= -4.0f)
        {
            yield return null;
            player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(player.transform.position.x, -4.17f), 0.05f);
            maincamera.orthographicSize += 0.05f;
            Debug.Log("d");
        }
        isStart = true;
        thePlayerMove.ModeSelect();
        StartCoroutine(GameObject.Find("Illust").GetComponent<IllustMoveScript>().IllustWide_ViewMove());

    }
    private IEnumerator ContinueClick()
    {
        while (firstInput)
        {
            yield return new WaitForSeconds(0.5f);
            playText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            playText.gameObject.SetActive(true);
        }
        playText.gameObject.SetActive(false);
    }

    
}
