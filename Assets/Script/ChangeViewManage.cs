using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeViewManage : MonoBehaviour
{
    //싱글톤
    #region
    private static ChangeViewManage instance;
    public static ChangeViewManage Instastance { get { return instance; } }
    #endregion

    public bool isTopView;     //탑뷰와 사이드뷰를 구분하기 위해 사용
    GameObject mainCamera;

    Animator viewAnimator;

    public static int NUMBER;


    [SerializeField]
    PlayerMoveScript thePlayerMove;

    [SerializeField]
    GameObject caveBackground;

    private void Start()
    {
        isTopView = false;

        viewAnimator = gameObject.GetComponent<Animator>();
        mainCamera = GameObject.Find("Main Camera");
        viewAnimator.SetBool("isTopView", true);

        if (instance == null)
            instance = this;

        //thePlayerMove.ModeSelect();        //모드에 따른 플레이어 이동
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {

    //        Debug.Log("isTopView : " + isTopView);
    //        if (isTopView)
    //        {
    //            isTopView = false;
    //            caveBackground.SetActive(false);
    //            StartCoroutine(FlipToTopView());
    //        }
    //        else if (!isTopView)
    //        {
    //            isTopView = true;
    //            caveBackground.SetActive(true);
    //            StartCoroutine(FlipToWideView());
    //        }
    //    }

    //}
    public void Flip(int _number)
    {
        PlayerMoveScript.Spawning = false;
        if (isTopView)
        {
            isTopView = false;
            
            StartCoroutine(FlipToTopView());
            Debug.Log("TOPVEIW");
            StartCoroutine(GameObject.Find("Illust").GetComponent<IllustMoveScript>().IllustWide_ViewMove());

            //thePlayerMove.ModeSelect();        //모드에 따른 플레이어 이동
        }
        else if(!isTopView)
        {
            StartCoroutine(FlipToWideView());
            isTopView = true;
            StartCoroutine(GameObject.Find("Illust").GetComponent<IllustMoveScript>().IllustTop_ViewMove());
            //thePlayerMove.ModeSelect();        //모드에 따른 플레이어 이동
        }

        PlayerMoveScript.Spawning = true;

        thePlayerMove.ModeSelect();
    }
    private IEnumerator FlipToTopView()
    {
        viewAnimator.SetBool("isTopView", true);
        for (int i = 0; i < 16; i++)
        {
            yield return new WaitForSeconds(0.025f);
            mainCamera.gameObject.transform.rotation = Quaternion.Lerp(mainCamera.gameObject.transform.rotation, Quaternion.Euler(0, 0, 0), 0.5f);
        }
        //mainCamera.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private IEnumerator FlipToWideView()
    {
        viewAnimator.SetBool("isTopView", false);
        for (int i = 0; i < 16; i++)
        {
            yield return new WaitForSeconds(0.025f);
            mainCamera.gameObject.transform.rotation = Quaternion.Lerp(mainCamera.gameObject.transform.rotation, Quaternion.Euler(0, 0, 90), 0.5f);
        }
        //mainCamera.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
    }
}