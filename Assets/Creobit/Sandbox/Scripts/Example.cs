using System.Linq;
using UnityEngine;

namespace Creobit.Advertising.Sandbox
{
    [DisallowMultipleComponent]
    public abstract class Example : MonoBehaviour
    {
        #region MonoBehaviour

        protected virtual void Awake()
        {
        }

        private async void Start()
        {
            Debug.Log($"=> {nameof(IPromoter.Initialize)}Promoter");

            await Promoter.InitializeAsync();

            LogAdvertisements();
        }

        #endregion
        #region Example

        protected virtual IPromoter Promoter
        {
            get;
            set;
        }

        private void LogAdvertisements()
        {
            Debug.Log($"{nameof(IPromoter.Advertisements)}:");

            foreach (var advertisement in Promoter.Advertisements)
            {
                Debug.Log(advertisement);
            }
        }

        public async void Prepare()
        {
            Debug.Log($"=> {nameof(Prepare)}");

            var advertisement = Promoter.Advertisements
                .FirstOrDefault(x => x.Id == "Example");

            if (advertisement != null)
            {
                await advertisement.PrepareAsync();
            }

            LogAdvertisements();
        }

        public async void Show()
        {
            Debug.Log($"=> {nameof(Show)}");

            var advertisement = Promoter.Advertisements
                .FirstOrDefault(x => x.Id == "Example");

            if (advertisement != null)
            {
                await advertisement.ShowAsync();
            }

            LogAdvertisements();
        }

        #endregion
    }
}
