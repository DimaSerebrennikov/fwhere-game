using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceS : MonoBehaviour
{
    SpriteRenderer sr;
    float timer;
    float targetTime;
    bool done;
    bool anywayRestart;
    bool isShowedAd;
    //=========================================================== Редактор
    public Sprite whiteSword;
    public AdS ad;
    //=========================================================== Редактор
    private void Awake()
    {
        anywayRestart = false;
        targetTime = 10f;
        done = false;
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!done)
        {
            timer += Time.deltaTime;
            if (timer >= targetTime)
            {
                sr.color = Color.black;
                done = true;
            }
        }
        if (anywayRestart && Input.touchCount > 0)
        {
            if (ad.isClosed)
            {
                SceneManager.LoadScene("Main");
            }
            for (int a = 0; a < Input.touchCount; a++)
            {
                Touch touch = Input.GetTouch(a);
                if (touch.phase == TouchPhase.Began)
                {
                    if (!isShowedAd)
                    {
                        isShowedAd = true;
                        ad.ShowAd();
                    }
                }
            }
        }
    }
    public void ShowDeath()
    {
        SwordScript[] swords = FindObjectsByType<SwordScript>(FindObjectsSortMode.None); 
        foreach(SwordScript sword in swords)
        {
            sword.GetComponent<SpriteRenderer>().sprite = whiteSword;
        }
        anywayRestart = true;
        Time.timeScale = 0f;
    }
}
