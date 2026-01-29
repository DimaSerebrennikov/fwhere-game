using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetCheckS : MonoBehaviour
{
    public MenuScript menu;
    [SerializeField] PremiumActivation _premiumActivation;
    private void Awake()
    {
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if(Application.internetReachability != NetworkReachability.NotReachable || _premiumActivation.IsPremium) //как только есть интернет
        {
            Destroy(gameObject);
            if (!menu.tutorialMenu.activeSelf)
            {
                Time.timeScale = 1f;
            }
        }
    }
}
