using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class RecordTimerS : MonoBehaviour
{
    float timer;
    public TextMeshPro tmpText;
    //=========================================================== Редактор
    public BalanceScript balanceScript;
    //=========================================================== Редактор
    private void Awake()
    {
        timer = 0f;
        StartCoroutine(onceInSecond());
    }
    IEnumerator onceInSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (PlayerPrefs.GetInt("record") < timer)
            {
                PlayerPrefs.SetInt("record", (int)timer);
            }
            balanceScript.ThroughTime();
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        int iTimer = (int)timer;
        tmpText.text = $"{iTimer}";
    }

}
