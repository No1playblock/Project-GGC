using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //싱글톤
    #region
    private static SoundManager instance;
    public static SoundManager Instastance { get { return instance; } }
    #endregion

    [SerializeField]
    AudioSource playerHitEffect;
    [SerializeField]
    GameObject shotEffect;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void OnPlayerHitEffect()
    {
        playerHitEffect.Play();
    }

    public void OnPlayShotEffect()
    {
        shotEffect.SetActive(true);
    }

    public void OffShotEffect()
    {
        shotEffect.SetActive(false);
    }
}
