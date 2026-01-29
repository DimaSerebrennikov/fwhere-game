using UnityEngine;
using GoogleMobileAds.Api;

public class AdS : MonoBehaviour
{
    [SerializeField] PremiumActivation _premiumActivation;
    public static float timerAd = 300f;
    public GameObject AdLoadedStatus;
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    private const string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_ANDROID
    private const string _adUnitId = "ca-app-pub-3626396842312444/5012660701";
#elif UNITY_IPHONE
        private const string _adUnitId = "ca-app-pub-3626396842312444/9442932052";
#else
        private const string _adUnitId = "unused";
#endif
    private InterstitialAd _interstitialAd;
    public bool isClosed;
    private void Awake()
    {
        if (!_premiumActivation.IsPremium)
        {
            LoadAd();
        }
    }
    private void Update()
    {
        timerAd += Time.unscaledDeltaTime;
    }
    public void LoadAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            DestroyAd();
        }

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            // If the operation failed with a reason.
            if (error != null)
            {
                return;
            }
            // If the operation failed for unknown reasons.
            // This is an unexpected error, please report this bug if it happens.
            if (ad == null)
            {
                return;
            }

            // The operation completed successfully.
            _interstitialAd = ad;

            // Register to ad events to extend functionality.
            RegisterEventHandlers(ad);
        });
    }
    public void ShowAd()
    {
        if (timerAd >= 300f && !_premiumActivation.IsPremium)
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                timerAd = 0f;
                _interstitialAd.Show();
            }
            else
            {
                isClosed = true;
            }
            // Inform the UI that the ad is not ready.
            AdLoadedStatus?.SetActive(false);
        }
        else
        {
            isClosed = true;
        }
    }
    public void DestroyAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }
        // Inform the UI that the ad is not ready.
        AdLoadedStatus?.SetActive(false);
    }
    public void LogResponseInfo()
    {
        if (_interstitialAd != null)
        {
            var responseInfo = _interstitialAd.GetResponseInfo();
            UnityEngine.Debug.Log(responseInfo);
        }
    }
    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            isClosed = true;
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            isClosed = true;
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            isClosed = true;
        };
    }
}
