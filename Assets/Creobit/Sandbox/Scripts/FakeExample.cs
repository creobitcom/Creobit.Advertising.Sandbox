using UnityEngine;

namespace Creobit.Advertising.Sandbox
{
    public sealed class FakeExample : Example
    {
        #region MonoBehaviour

        protected override void Awake()
        {
            var configuration = new FakePromoterConfiguration()
            {
                AdvertisementMap = new (string AdvertisementId, string Tag)[]
                {
                    ("Example", "rewardedVideo")
                }
            };

            Promoter = new FakePromoter(configuration);
            Promoter.ExceptionDetected += exception => Debug.LogException(exception);
        }

        #endregion
    }
}
