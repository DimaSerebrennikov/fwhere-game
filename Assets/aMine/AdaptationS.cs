using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AdaptationS : MonoBehaviour
{
    float FloatScreenWidth;
    float FloatScrennHeight;
    float tarS;
    float aspect;
    float os;
    float osa;
    BlackEyeS[] blackEyeS;
    //=========================================================== –едактор
    public Transform wallRight;
    public Transform wallLeft;
    public Transform wallUp;
    public Transform wallDown;
    //=========================================================== –едактор
    private void Awake()
    {
        blackEyeS = FindObjectsByType<BlackEyeS>(FindObjectsSortMode.None);
        tarS = 14f;
        //=========================================================== Ёкран
        FloatScreenWidth = Screen.width;
        FloatScrennHeight = Screen.height;
        aspect = FloatScreenWidth / FloatScrennHeight;
        //=========================================================== Ёкран
        //=========================================================== ‘ормула адаптации размера арены под устройство
        // Camera.main.orthographicSize = Mathf.Sqrt(tarS / aspect); Ёто правильна€ формула, в коде она искожена дл€ баланса!!!!!!!!!!
        Camera.main.orthographicSize = Mathf.Sqrt(tarS / ((aspect + 1) / 2));
        //=========================================================== ‘ормула адаптации размера арены под устройство
        os = Camera.main.orthographicSize;
        osa = os * aspect;
        SetWalls();

        blackEyeS[0].transform.position = new Vector2(osa, os);
        blackEyeS[1].transform.position = new Vector2(-osa, os);
        blackEyeS[2].transform.position = new Vector2(osa, -os);
        blackEyeS[3].transform.position = new Vector2(-osa, -os);

    }
    void SetWalls()
    {
        wallRight.position = new Vector2(osa + wallRight.localScale.x / 2f, 0);
        wallLeft.position = new Vector2(-(osa + wallLeft.localScale.x / 2f), 0);
        wallUp.position = new Vector2(0f, os + wallUp.localScale.y / 2f);
        wallDown.position = new Vector2(0f, -(os + wallDown.localScale.y / 2f));
    }
}
