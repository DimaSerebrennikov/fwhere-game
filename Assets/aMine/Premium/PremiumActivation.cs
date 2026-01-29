using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PremiumActivation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _premiumText;
    [SerializeField] TextMeshProUGUI _tmpIAP;
    bool _isPremium;
    public bool IsPremium
    {
        get
        {
            return _isPremium;
        }
        set
        {
            _isPremium = value;
            if (value == true)
            {
                PlayerPrefs.SetInt("Premium", 1);
                _premiumText.text = "ads & netcheck were disabled";
                ChangeNameIap();
            }
            else
            {
                PlayerPrefs.DeleteKey("Premium");
            }
        }
    }
    void ChangeNameIap()
    {
        _tmpIAP.text = "^^";
    }
    private void Awake()
    {
        if (PlayerPrefs.GetInt("record") >= 200)
        {
            IsPremium = true;
        }
        if (PlayerPrefs.HasKey("Premium"))
        {
            IsPremium = true;
        }
    }
}
