using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    SpaceS space;
    BalanceScript balance;
    private void Awake()
    {
        space = FindAnyObjectByType<SpaceS>();
        balance = FindAnyObjectByType<BalanceScript>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "eye")
        {
            BlackEyeS eyeScriptObj = collision.gameObject.GetComponent<BlackEyeS>();
            if (eyeScriptObj.health == 2)
            {
                balance.GetHit();
                eyeScriptObj.health = 1;
                eyeScriptObj.CheckHp();
                Destroy(gameObject);
            }
            else if (eyeScriptObj.health == 1)
            {
                eyeScriptObj.health = 0;
                eyeScriptObj.CheckHp();
                space.ShowDeath();
            }
        }
    }
}
