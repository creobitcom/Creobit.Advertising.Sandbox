using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Creobit.Advertising.Sandbox
{
    [DisallowMultipleComponent]
    public class Example : MonoBehaviour
    {
        #region MonoBehaviour

        protected virtual void Awake()
        {
            _authManager = new PlatformAuthManager(_advertisementConfig.Platforms);
            _promoter = new Promoter(_advertisementConfig.Advertisements, _authManager);

            _initializeButton.onClick.AddListener(() => _authManager.Initialize(OnAuthManagerInitialized));

            _prepareTest.onClick.AddListener(() => OnClickPrepareTest());

            _showTest.onClick.AddListener(() => OnClickShowTest());
        }

        #endregion
        #region Example

        private void LogAdvertisements()
        {
            Debug.Log($"{nameof(IPromoter.Advertisements)}:");

            foreach (var advertisement in _promoter.Advertisements)
            {
                Debug.Log(advertisement);
            }
        }

        private void OnClickPrepareTest ()
        {
            _promoter.Advertisements
                .Where(ad => ad.Tag == "Test")
                .FirstOrDefault()
                .Prepare(
                        () => OnPrepare(true),
                        () => OnPrepare(false));
        }
        private void OnClickShowTest()
        {
            _promoter.Advertisements.
                    Where(ad => ad.Tag == "Test").
                    FirstOrDefault().
                    Show(
                        () => OnShow(ShowResult.Success),
                        () => OnShow(ShowResult.Skip),
                        () => OnShow(ShowResult.Fail));
        }

        private void OnPrepare(bool result)
        {
            _prepareIndicator.color = result ? Color.green : Color.red;
            Debug.Log($"Реклама загружена {result}");
        }

        private void OnShow(ShowResult result)
        {
            Debug.Log($"Результат показа: {result}");
        }

        #endregion

        private void OnAuthManagerInitialized()
        {
            _initializedStatus.color = _authManager.IsInitialized ? Color.green : Color.red;
            foreach (var platform in _authManager.AuthenticatedPlatforms)
            {
                Debug.Log(platform);
            }
        }

        [SerializeField]
        private Button _initializeButton;
        [SerializeField]
        private Image _initializedStatus;
        [SerializeField]
        private Image _prepareIndicator;
        [SerializeField]
        private Button _prepareTest;
        [SerializeField]
        private Button _showTest;

        [SerializeField]
        private AdvertisementConfig _advertisementConfig;

        private IPromoter _promoter;
        private IPlatformAuthManager _authManager;

        private enum ShowResult
        {
            Success,
            Skip,
            Fail
        }
    }
}
