using System;
using UnityEngine;
using GoogleMobileAds.Api;

[CreateAssetMenu(fileName = "AdMobAuth", menuName = "Configs/PlatformAuth/AdMob")]
public class AdMobAuth : PlatformAuth
{
    #region PlatformAuth

    public override bool IsInitialized => _isInitialized;

    protected override AdvertisementPlatforms Platform => AdvertisementPlatforms.AdMob;

    public override void Initialize(Action onSuccess, Action onFailed)
    {
        MobileAds.Initialize(_appId);
        _isInitialized = true;
        onSuccess?.Invoke();
    }

    #endregion

    private bool _isInitialized = false;

    [SerializeField]
    private string _appId;
}



