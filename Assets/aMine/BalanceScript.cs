using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScript : MonoBehaviour
{
    public int killedEnemies;
    public float freqSpam;
    float freqSpamStep;
    float os;
    public double velocitySword;
    double velocitySwordK;
    public float regen;
    float regenStep;
    //=========================================================== Редактор
    public GameObject eye;
    //=========================================================== Редактор
    private void Awake()
    {
        regen = 13f;
        regenStep = 1f;
        velocitySword = 0.27f;
        velocitySwordK = 0.0002f;
        freqSpam = 1.74f;
        freqSpamStep = 0.9965f;
        killedEnemies = 0;
        os = Camera.main.orthographicSize;
    }
    public void ThroughTime() //в Update
    {
        freqSpam = freqSpam * freqSpamStep;
    }
    public void PassDistance(float newDistance)
    {
        double scaledDistace = newDistance * os * velocitySwordK;
        velocitySword += scaledDistace;
    }
    public void GetHit()
    {
        regen += regenStep;
    }

}
