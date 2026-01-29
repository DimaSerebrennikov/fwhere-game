using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpamScript : MonoBehaviour
{
    public float os;
    public float aspect;
    public float osa;
    float oneF;
    //Vector2 upRightAng;
    //Vector2 upLeftAng;
    //Vector2 downRightAng;
    //Vector2 downLeftAng;
    //=========================================================== Для цели
    //Vector2 upRightAngUnMargin;
    //Vector2 upLeftAngUnMargin;
    //Vector2 downRightAngUnMargin;
    //Vector2 downLeftAngUnMargin;
    //=========================================================== Для цели
    Vector2 spamPosition;
    int counter;
    //=========================================================== Редактор
    public GameObject enemy;
    public BalanceScript balance;
    //=========================================================== Редактор
    private void Awake()
    {
        oneF = 1f;
        counter = 1;
        aspect = (float)Screen.width / (float)Screen.height;
        os = Camera.main.orthographicSize;
        osa = os * aspect;
        //upRightAng = new Vector2(osa + margin, os + margin);
        //upLeftAng = new Vector2(-osa - margin, os + margin);
        //downRightAng = new Vector2(osa + margin, -os - margin);
        //downLeftAng = new Vector2(-osa - margin, -os - margin);
        //=========================================================== Для цели
        //upRightAngUnMargin = new Vector2(os, os);
        //upLeftAngUnMargin = new Vector2(-os, os);
        //downRightAngUnMargin = new Vector2(os, -os);
        //downLeftAngUnMargin = new Vector2(-os, -os);
        //=========================================================== Для цели 
        spamPosition = Vector2.zero;
    }
    private void Start()
    {
        StartCoroutine(SpamContinuos());
    }
    IEnumerator SpamContinuos()
    {
        while (true)
        {
            SpamEnemy();
            yield return new WaitForSeconds(balance.freqSpam);
        }
    }
    public void SpamEnemy()
    {
        Vector2 targetPosition = Vector2.zero;
        int a = Random.Range(0, 4);
        if (counter%4 == 0)
        {
            targetPosition = new Vector2(oneF, Random.Range(-oneF, oneF));
            counter = 0;
        }
        else if (counter % 3 == 0)
        {
            targetPosition = new Vector2(-oneF, Random.Range(-oneF, oneF));
        }
        else if (counter % 2 == 0)
        {
            targetPosition = new Vector2(Random.Range(-oneF, oneF), oneF);
        }
        else
        {
            targetPosition = new Vector2(Random.Range(-oneF, oneF),-oneF);
        }
        GameObject newEnemy = Instantiate(enemy, spamPosition, Quaternion.identity);
        Vector2 direction = targetPosition - spamPosition;
        newEnemy.GetComponent<Rigidbody2D>().AddForce(direction * (float)balance.velocitySword, ForceMode2D.Impulse);
        newEnemy.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        counter++;
    }
}
