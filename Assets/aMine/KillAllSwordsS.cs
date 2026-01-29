using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KillAllSwordsS : MonoBehaviour
{
    BalanceScript balanceScript;
    float margin;
    //=========================================================== Редактор
    public SpamScript spamScript;
    //=========================================================== Редактор
    private void Awake()
    {
        margin = 3f;
        balanceScript = FindAnyObjectByType<BalanceScript>();
    }
    private void FixedUpdate()
    {
        SwordScript[] allObjects = FindObjectsByType<SwordScript>(FindObjectsSortMode.None);
        for (int a = 0; a < allObjects.Length; a++)
        {
            Vector2 curObjPos = allObjects[a].transform.position;
            if (curObjPos.y >= spamScript.os + margin)
            {
                Destroy(allObjects[a].gameObject);
                balanceScript.killedEnemies++;
            }
            else if (curObjPos.y <= -spamScript.os - margin)
            {
                Destroy(allObjects[a].gameObject);
                balanceScript.killedEnemies++;
            } 
            else if (curObjPos.x >= spamScript.osa + margin)
            {
                Destroy(allObjects[a].gameObject);
                balanceScript.killedEnemies++;
            }
            else if (curObjPos.x <= -spamScript.osa - margin)
            {
                Destroy(allObjects[a].gameObject);
                balanceScript.killedEnemies++;
            }
        }
    }
}
