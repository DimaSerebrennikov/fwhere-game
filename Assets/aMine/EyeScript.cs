using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    GameObject target;
    SpamScript spam;
    float os;
    //=========================================================== Редактор
    public Transform iris;
    //=========================================================== Редактор
    private void Awake()
    {
        target = null;
        spam = FindAnyObjectByType<SpamScript>();
        os = Camera.main.orthographicSize;
    }
    private void Update()
    {
        //=========================================================== Нахождение противников
        SwordScript[] allObjects = FindObjectsByType<SwordScript>(FindObjectsSortMode.None);
        float closestDistance = Mathf.Infinity;
        for (int a = 0; a < allObjects.Length; a++)
        {
            if (Vector2.Distance(allObjects[a].GetComponent<Transform>().position, transform.position) < closestDistance)
            {
                closestDistance = Vector2.Distance(allObjects[a].GetComponent<Transform>().position, transform.position);
                target = allObjects[a].gameObject;
            }
        }
        //=========================================================== Нахождение противников
        if (target != null)
        {
            Vector3 direction = (target.transform.position - gameObject.transform.position).normalized;
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            float aspectResult = closestDistance / os;

            float positionResult = 0.5f * aspectResult;
            if (positionResult > 0.5f)
            {
                positionResult = 0.5f;
            }
            float stabilization = (0.25f - positionResult) / 3f;
            positionResult = positionResult + stabilization;
             
            float scaleResult = (1f - aspectResult)/2;
            if (scaleResult < 0)
            {
                scaleResult = 0;
            }
            stabilization = (0.4f - scaleResult) / 4f;
            scaleResult = scaleResult + stabilization;

            iris.localScale = new Vector2(scaleResult, scaleResult);
            iris.localPosition = new Vector2(positionResult, 0);
        }
    }
}
