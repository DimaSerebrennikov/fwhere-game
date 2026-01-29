using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyLogoScript : MonoBehaviour
{
    //=========================================================== Editor
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image imageLogo;
    [SerializeField] Image darker;
    [SerializeField] bool isStopAfterFinish;
    //=========================================================== Editor
    public Action onDestroy;
    float _timeBetweenFrames;
    float _targetTimeBetweenFrames;
    int _currentFrame;
    static bool showLogo = true;
    bool _darkerOn;
    bool _darkerOff;
    bool _logoOn;
    bool _nextFrame;
    private void Awake()
    {
        _nextFrame = false;
        _darkerOn = false;
        _logoOn = false;
        _darkerOff = true;
        _currentFrame = 0;
        _targetTimeBetweenFrames = 0.016f;
        _timeBetweenFrames = 0f;
        //=========================================================== Once in game
        if (!showLogo)
        {
            Destroy(gameObject);
        }
        else
        {
            Time.timeScale = 0f;
            showLogo = false;
        }
        //=========================================================== Once in game
    }
    private void Update()
    {

        _timeBetweenFrames += Time.unscaledDeltaTime;
        while (_timeBetweenFrames >= _targetTimeBetweenFrames)
        {
            _nextFrame = true;
            _timeBetweenFrames -= _targetTimeBetweenFrames;
        }
        //=========================================================== Unfading
        if (_darkerOff)
        {
            if (_nextFrame)
            {
                _nextFrame = false;
                float nextColor = darker.color.a - 0.016f;
                darker.color = new Color(0, 0, 0, nextColor);
                if (nextColor <= 0)
                {
                    _darkerOff = false;
                    _logoOn = true;
                }
            }
        }
        //=========================================================== Unfading
        //=========================================================== Logo animation
        if (_logoOn)
        {
            if (_nextFrame
            && _currentFrame < sprites.Length)
            {
                _nextFrame = false;
                imageLogo.sprite = sprites[_currentFrame];
                _currentFrame++;
            }
            if (_currentFrame >= sprites.Length)
            {
                _logoOn = false;
                _darkerOn = true;
            }
        }
        //=========================================================== Logo animation
        //=========================================================== Fading
        if (_darkerOn)
        {
            if (_nextFrame)
            {
                _nextFrame = false;
                float nextColor = darker.color.a + 0.016f;
                darker.color = new Color(0, 0, 0, nextColor);
                if (nextColor >= 1)
                {
                    FinalClosing();
                }
            }
        }
        //=========================================================== Fading
    }
    void FinalClosing()
    {
        if (isStopAfterFinish)
        {
            Time.timeScale = 0f;
        }
        onDestroy?.Invoke();
        Destroy(gameObject);
    }
}