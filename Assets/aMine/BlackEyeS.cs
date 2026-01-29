using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEyeS : MonoBehaviour
{
    public int health;
    float timerRegen;
    Vector2 startLocalScale;
    BalanceScript balance;
    //=========================================================== Редактор
    public SpriteRenderer spriteRenderer;
    //=========================================================== Редактор
    private void Awake()
    {
        balance = FindAnyObjectByType<BalanceScript>();
        timerRegen = 0f;
        startLocalScale = transform.localScale;
        health = 2;
    }
    private void Update()
    {
        //float minDistance = Mathf.Infinity;
        //BlackEyeS[] allEyes = FindObjectsByType<BlackEyeS>(FindObjectsSortMode.None);
        //if (allEyes.Length > 1)
        //{
        //    for (int a = 0; a < allEyes.Length; a++)
        //    {
        //        if (allEyes[a].gameObject != gameObject)
        //        {
        //            float curDist = Vector2.Distance(transform.position, allEyes[a].transform.position);
        //            if (curDist < minDistance)
        //            {
        //                minDistance = curDist;
        //            }
        //        }
        //    }
        //    float osHalf = Camera.main.orthographicSize / 3f;
        //    if (minDistance > osHalf)
        //    {
        //        minDistance = osHalf;
        //    }
        //    float precentToIncrease = 100f - minDistance / (osHalf / 100f);
        //    transform.localScale = startLocalScale + startLocalScale * precentToIncrease * 0.01f;
        //}
        if (health < 2)
        {
            timerRegen += Time.deltaTime;
        }
        if (timerRegen >= balance.regen)
        {
            health = 2;
            timerRegen = 0f;
            spriteRenderer.color = Color.clear;
        }
    }
    public void CheckHp()
    {
        if (health == 1)
        {
            spriteRenderer.color = Color.white;
        }
        else if (health <= 0)
        {
            Debug.Log("Death");
        }
    }
}
