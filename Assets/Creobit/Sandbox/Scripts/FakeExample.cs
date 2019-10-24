using UnityEngine;

namespace Creobit.Advertising.Sandbox
{
    public sealed class FakeExample : Example
    {
        #region MonoBehaviour

        protected override void Awake()
        {
            var configuration = new FakeConfiguration()
            {
                AdvertisementMap = new (string AdvertisementId, string Tag)[]
                {
                    ("Example", "rewardedVideo")
                }
            };

            configuration.ExceptionDetected += exception => Debug.LogException(exception);

            Promoter = new FakePromoter(configuration);
        }

        #endregion
    }
}
