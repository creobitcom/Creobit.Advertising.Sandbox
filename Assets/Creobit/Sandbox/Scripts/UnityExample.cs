using UnityEngine;

namespace Creobit.Advertising.Sandbox
{
    public sealed class UnityExample : Example
    {
        #region MonoBehaviour

#if CREOBIT_ADVERTISING_UNITY && (UNITY_ANDROID || UNITY_IOS)
        protected override void Awake()
        {
            var configuration = new UnityPromoterConfiguration(GameId, _debugMode, _testMode)
            {
                AdvertisementMap = new (string AdvertisementId, string PlacementId)[]
                {
                    ("Example", "rewardedVideo")
                }
            };

            Promoter = new UnityPromoter(configuration);
            Promoter.ExceptionDetected += exception => Debug.LogException(exception);
        }
#endif

        #endregion
        #region UnityExample

        [Header("Unity")]

        [SerializeField]
        private string _androidGameId;

        [SerializeField]
        private string _iOSGameId;

        [SerializeField]
        private bool _debugMode;

        [SerializeField]
        private bool _testMode;

        private string GameId
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        return _androidGameId;
                    case RuntimePlatform.IPhonePlayer:
                        return _iOSGameId;
                    default:
                        return string.Empty;
                }
            }
        }

        #endregion
    }
}
