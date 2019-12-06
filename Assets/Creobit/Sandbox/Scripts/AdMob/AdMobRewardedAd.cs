using System;
using GoogleMobileAds.Api;
using UnityEngine;

[CreateAssetMenu(fileName ="AdMobRewardedAd", menuName = "Configs/Advertisement/AdMobRewardedAd")]
public class AdMobRewardedAd : Advertisement
{
    #region Advertisement

    protected override AdvertisementPlatforms Platform => AdvertisementPlatforms.AdMob;

    public override bool IsReady { get => _adObject!=null && _adObject.IsLoaded(); }

    public override void Prepare(Action onComplete, Action onFailure)
    {
        if (_adObject == null)
        {
            _adObject = new RewardedAd(_adUnitId);
        }

        if (_adObject.IsLoaded())
        {
            onComplete?.Invoke();
            return;
        }

        var request = new AdRequest.Builder().Build();
        _adObject.OnAdLoaded += OnAdLoadedHandle;
        _adObject.OnAdFailedToLoad += OnAdFaliedToLoadHandle;
        _adObject.LoadAd(request);

        void OnAdLoadedHandle(object sender, EventArgs eventArgs)
        {
            _adObject.OnAdLoaded -= OnAdLoadedHandle;
            _adObject.OnAdFailedToLoad -= OnAdFaliedToLoadHandle;

            onComplete?.Invoke();
        }

        void OnAdFaliedToLoadHandle(object sender, AdErrorEventArgs eventArgs)
        {
            _adObject.OnAdLoaded -= OnAdLoadedHandle;
            _adObject.OnAdFailedToLoad -= OnAdFaliedToLoadHandle;

            onFailure?.Invoke();
        }
    }

    public override void Show(Action onComplete, Action onSkip, Action onFailure)
    {
        if (!IsReady)
        {
            onFailure?.Invoke();
            return;
        }

        _adObject.OnAdOpening += OnAdOpeningHandle;
        _adObject.OnAdFailedToShow += OnAdFailedShow;
        _adObject.Show();

        void OnAdOpeningHandle(object sender, EventArgs eventArgs)
        {
            _adObject.OnAdOpening -= OnAdOpeningHandle;

            _adObject.OnUserEarnedReward += OnRewardGaining;
            _adObject.OnAdClosed += OnAdSkippedClosed;
        }

        void OnRewardGaining(object sender, Reward eventArgs)
        {
            _adObject.OnUserEarnedReward -= OnRewardGaining;
            _adObject.OnAdClosed -= OnAdSkippedClosed;

            _adObject.OnAdClosed += OnAdSuccesfulClosed;
        }

        void OnAdSuccesfulClosed(object sender, EventArgs eventArgs)
        {
            _adObject.OnAdClosed -= OnAdSuccesfulClosed;

            _adObject = null;
            onComplete?.Invoke();
        }

        void OnAdSkippedClosed(object sender, EventArgs eventArgs)
        {
            _adObject.OnAdClosed -= OnAdSkippedClosed;
            _adObject.OnUserEarnedReward -= OnRewardGaining;

            _adObject = null;
            onSkip?.Invoke();
        }

        void OnAdFailedShow(object sender, AdErrorEventArgs eventArgs)
        {
            _adObject = null;
            onFailure?.Invoke();
        }
    }

    #endregion

    private RewardedAd _adObject;

    [SerializeField]
    private string _adUnitId;
}
