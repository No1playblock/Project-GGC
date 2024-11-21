using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    private GameObject[] leftbullet;        //풀링 담을 오브젝트 배열
    private GameObject[] middlebullet;        //풀링 담을 오브젝트 배열
    private GameObject[] rightbullet;        //풀링 담을 오브젝트 배열
    public GameObject playerbullet;          //총알 프리팹

    public static bool getItem = false;

    int maxCount = 20;                  //풀링 맥스값
    int count = 0;                      //총알 count    
    [SerializeField]
    bool isShooting = true;             //총알 텀을 주기위한 bool
    public GameObject player;           //플레이어 좌표를 위해(플레이어 좌표에서 총알생성)

    public float BULLETUM=0.2f;

    // Start is called before the first frame update
    void Start()
    {
        leftbulletSpawn();              //풀링
        middlebulletSpawn();
        rightbulletSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z) && isShooting && PlayerMoveScript.Spawning)             //텀마다 실행
        {
            Debug.Log("");
            SoundManager.Instastance.OnPlayShotEffect();
            StartCoroutine(shot());
        }
        else if (Input.GetKeyUp(KeyCode.Z))
            SoundManager.Instastance.OffShotEffect();
    }
    public void bullet_Triple_shoot()                 //총알 하나를 쏘는 함수
    {

        leftbullet[count].SetActive(true);
        leftbullet[count].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z +0.1f);
        StartCoroutine(leftbullet[count].GetComponent<PlayerBulletScript>().Horizonshoot());        //횡스크롤 총알 나가기
        middlebullet[count].SetActive(true);
        middlebullet[count].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 0.1f);
        StartCoroutine(middlebullet[count].GetComponent<PlayerBulletScript>().Horizonshoot());        //횡스크롤 총알 나가기
        rightbullet[count].SetActive(true);
        rightbullet[count].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 0.1f);
        StartCoroutine(rightbullet[count].GetComponent<PlayerBulletScript>().Horizonshoot());        //횡스크롤 총알 나가기



        //StartCoroutine(shoot());
        count++;
        if (count == maxCount)
        {
            count = 0;
        }
    }
    public void bullet_shoot()                 //총알 하나를 쏘는 함수
    {

        
        middlebullet[count].SetActive(true);
        middlebullet[count].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 0.1f);
        StartCoroutine(middlebullet[count].GetComponent<PlayerBulletScript>().Horizonshoot());        //횡스크롤 총알 나가기
       


        //StartCoroutine(shoot());
        count++;
        if (count == maxCount)
        {
            count = 0;
        }
    }


    IEnumerator shot()      //2초마다 총알이 나가게
    {
        isShooting = false;
        if (getItem)
        {
            bullet_Triple_shoot();
        }
        else
        {
            bullet_shoot();             //총알쏘는 함수
        }
        
        yield return new WaitForSeconds(BULLETUM);  //BULLETUM있다가 다시 실행
        isShooting = true;

    }
   
    public void leftbulletSpawn()
    {
        leftbullet = new GameObject[maxCount];               //오브젝트가 저장되어있는 변수
        for (int j = 0; j < maxCount; j++)
        {

            GameObject gm = (GameObject)Instantiate(playerbullet, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);      //z 16
            gm.gameObject.transform.rotation = Quaternion.Euler(0, 0, 16);

            gm.SetActive(false);                     //Active를 비활성화   
            leftbullet[j] = gm;                         //전역변수 _pool에 집어넣는다.
        }
    }
    public void middlebulletSpawn()
    {
        middlebullet = new GameObject[maxCount];               //오브젝트가 저장되어있는 변수
        for (int j = 0; j < maxCount; j++)
        {

            GameObject gm = (GameObject)Instantiate(playerbullet, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);


            gm.SetActive(false);                     //Active를 비활성화   
            middlebullet[j] = gm;                         //전역변수 _pool에 집어넣는다.
        }
    }
    public void rightbulletSpawn()
    {
        rightbullet = new GameObject[maxCount];               //오브젝트가 저장되어있는 변수
        for (int j = 0; j < maxCount; j++)
        {

            GameObject gm = (GameObject)Instantiate(playerbullet, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);       //z -16
            gm.gameObject.transform.rotation = Quaternion.Euler(0, 0, -16);

            gm.SetActive(false);                     //Active를 비활성화   
            rightbullet[j] = gm;                         //전역변수 _pool에 집어넣는다.
        }
    }

}
