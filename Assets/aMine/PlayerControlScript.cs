using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    bool[] holdingIsActive;
    Vector2[] distBtwnPointAndObj;
    Transform[] castingTransformObject;
    Transform[] eyeCastingTransformObject;
    Vector2[] lastTouch;
    //=========================================================== Редактор
    public SpamScript spamScript;
    public BalanceScript balance;
    public AudioS audioS;
    //=========================================================== Редактор
    private void Awake()
    {
        holdingIsActive = new bool[10];
        distBtwnPointAndObj = new Vector2[10];
        castingTransformObject = new Transform[10];
        eyeCastingTransformObject = new Transform[10];
        lastTouch = new Vector2[10];
    }
    void Update()
    {
        for (int a = 0; a < Input.touchCount && a < 10 && Time.timeScale > 0f; a++)
        {
            Touch touch = Input.GetTouch(a);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.tag == "eye")
                {
                    audioS.PlayRandomSound();
                    GameObject parentObject = hit.collider.gameObject.transform.parent.gameObject;
                    holdingIsActive[a] = true;
                    distBtwnPointAndObj[a] = (Vector2)parentObject.transform.position - touchPosition;
                    castingTransformObject[a] = parentObject.transform;
                    eyeCastingTransformObject[a] = hit.collider.transform;
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (castingTransformObject != null && holdingIsActive[a])
                {
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    //=========================================================== Ограничение на движение глаза за края арены
                    //----------------------------------------------------------- Размеры белого глаза
                    float eyeScaleXHalf = eyeCastingTransformObject[a].lossyScale.x / 2f;
                    //----------------------------------------------------------- Размеры белого глаза
                    Vector2 futurePosition = touchPosition + distBtwnPointAndObj[a];
                    //----------------------------------------------------------- Пропустит ли верхняя стена
                    if (futurePosition.y >= spamScript.os - eyeScaleXHalf)
                    {
                        futurePosition.y = spamScript.os - eyeScaleXHalf;
                    }
                    //----------------------------------------------------------- Пропустит ли верхняя стена
                    //----------------------------------------------------------- Пропустит ли нижняя стена
                    if (futurePosition.y <= -spamScript.os + eyeScaleXHalf)
                    {
                        futurePosition.y = -spamScript.os + eyeScaleXHalf;
                    }
                    //----------------------------------------------------------- Пропустит ли нижняя стена
                    //----------------------------------------------------------- Пропустит ли правая стена
                    if (futurePosition.x >= spamScript.osa - eyeScaleXHalf)
                    {
                        futurePosition.x = spamScript.osa - eyeScaleXHalf;
                    }
                    //----------------------------------------------------------- Пропустит ли правая стена
                    //----------------------------------------------------------- Пропустит ли левая стена
                    if (futurePosition.x <= -spamScript.osa + eyeScaleXHalf)
                    {
                        futurePosition.x = -spamScript.osa + eyeScaleXHalf;
                    }
                    //----------------------------------------------------------- Пропустит ли левая стена
                    balance.PassDistance(Vector2.Distance(futurePosition, castingTransformObject[a].position));
                    castingTransformObject[a].position = futurePosition;
                    //=========================================================== Ограничение на движение глаза за края арены
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                holdingIsActive[a] = false;
            }
        }
    }
}
