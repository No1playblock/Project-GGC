using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float Movespeed = 2.0f;
    public float Upforce = 2.0f;
    public Rigidbody2D rig;
    [SerializeField]
    private string mode;

    public static bool Spawning = true;
    public float godTime = 2.0f;

    private bool isdeath = false;

    public float speedSize = 3.5f;
    Vector3 sizeTotalAmountDistance;

    public GameObject potion1;
    public GameObject potion2;
    public GameObject potion3;
    private int lifecount = 3;


    public float speedRotation = 3.5f;
    public float speedTotalAmountDistance = 0.0f;

    bool isMove = true;
    bool isRotate = true;
    bool isScale = true;

    private Vector3 HorizonStartPosition = new Vector3(-4.0f, -7.8f, 0);
    private Vector3 TopStartPosition = new Vector3(-0.2f, -3.9f, 0);

    // Start is called before the first frame update
    void Start()
    {
        rig = this.GetComponent<Rigidbody2D>();
    }

    public void ModeSelect()
    {
        if (ChangeViewManage.Instastance.isTopView)            //횡스크롤일떄
        {
            mode = "H";
            this.gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Char_Side1") as RuntimeAnimatorController;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);

            StopCoroutine(Top_View());
            StartCoroutine(HorizonMode());

        }
        else if (!ChangeViewManage.Instastance.isTopView)            //Top view 일때
        {
            mode = "T";
            this.gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("PlayerAnimator") as RuntimeAnimatorController;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            StopCoroutine(HorizonMode());
            StartCoroutine(Top_View());
        }
    }
    IEnumerator Top_View()
    {
        StartCoroutine(TopStartVector());
        StartCoroutine(Pause());

        rig.gravityScale = 0.0f;//
        rig.simulated = false;
        while (mode.Equals("T"))
        {
            yield return null;
            if (!isdeath)
            {
                if (gameObject.transform.position.y < 4.5)
                {
                    if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                        this.transform.Translate(new Vector2(0, Movespeed * Time.deltaTime));
                }
                if (gameObject.transform.position.y > -4.55)
                {
                    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                        this.transform.Translate(new Vector2(0, -Movespeed * Time.deltaTime));
                }
                if (gameObject.transform.position.x > -2.12)
                {
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                        this.transform.Translate(new Vector2(-Movespeed * Time.deltaTime, 0));
                }
                if (gameObject.transform.position.x < 2.11)
                {
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                        this.transform.Translate(new Vector2(Movespeed * Time.deltaTime, 0));
                }
            }
        }
    }
    IEnumerator TopStartVector()
    {
        while (isMove)
        {
            yield return null;
            this.transform.position = Vector3.Lerp(this.transform.position, TopStartPosition, 0.1f);
        }
        isMove = true;

    }
    IEnumerator HorizonMode()
    {
        rig.simulated = true;

        StartCoroutine(HorizonStartVector());
        StartCoroutine(Pause());

        while (mode.Equals("H"))
        {
            yield return null;
            if (Input.GetKey(KeyCode.X)&&!isdeath)                        //횡스크롤일때 input 값
            {
                Debug.Log("JUMP");
                rig.AddForce(new Vector2(-Upforce * Time.deltaTime, 0));
            }
        }
    }
    IEnumerator HorizonStartVector()
    {
        while (isMove)
        {
            //Debug.Log("이동");
            yield return null;
            this.transform.position = Vector3.Lerp(this.transform.position, HorizonStartPosition, 0.1f);
        }
        isMove = true;

    }
    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(1.5f);
        isMove = false;
        Time.timeScale = 2;
       // rig.gravityScale = 0.3f;
        //

        if(mode.Equals("T"))
        {
            rig.gravityScale = 0.0f;
        }
        else if (mode.Equals("H"))
        {
            rig.gravityScale = 0.3f;
        }

    }
    public void playerHit()
    {
        switch (lifecount)
        {
            case 3:
                potion3.SetActive(false);
                lifecount--;
                break;
            case 2:
                potion2.SetActive(false);
                lifecount--;
                break;
            case 1:
                potion1.SetActive(false);
                lifecount--;
                break;
        }
        SoundManager.Instastance.OnPlayerHitEffect();
        sizeTotalAmountDistance = gameObject.transform.localScale;
        isdeath = true;
        StartCoroutine(death());
    }
    IEnumerator death()
    {
        StartCoroutine(deathCount());
        rig.simulated = false;
        while (isdeath)
        {
            yield return null;
            gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, speedTotalAmountDistance += speedRotation);

            transform.localScale = new Vector2(sizeTotalAmountDistance.x -= speedSize * Time.deltaTime,
                sizeTotalAmountDistance.y -= speedSize * Time.deltaTime);
        }
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(spawn());

    }
    IEnumerator deathCount()
    {
        yield return new WaitForSeconds(1.2f);
        isdeath = false;
    }
    IEnumerator spawn()
    {
        if (lifecount != 0)
        {
            if (mode.Equals("T"))
            {
                Debug.Log("T");
                this.transform.position = new Vector3(-0.2f, -9.19f, 0);
                transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                while (this.transform.position.y <= -3.91f)
                {
                    yield return null;
                    this.transform.position = Vector3.Lerp(this.transform.position, TopStartPosition, 0.1f);
                }
            }
            else if (mode.Equals("H"))
            {
                Debug.Log("H시작");

                this.transform.position = new Vector3(-4.0f, -15.19f, 0);
                transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                this.transform.rotation = Quaternion.Euler(0, 0, 90);
                while (this.transform.position.y <= -7.9f)
                {
                    yield return null;
                    this.transform.position = Vector3.Lerp(this.transform.position, HorizonStartPosition, 0.1f);
                }
                Debug.Log("H끝");
                yield return new WaitForSeconds(2.0f);
                rig.simulated = true;
            }

            yield return new WaitForSeconds(2.0f);

            Spawning = true;                //무적시간
            isdeath = false;
        }
        else
            StageManager.Instance.PlayerDead();
    }
    
    
}