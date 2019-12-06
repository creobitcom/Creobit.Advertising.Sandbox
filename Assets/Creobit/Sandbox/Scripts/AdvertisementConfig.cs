using System.Collections.Generic;
using UnityEngine;
using Creobit.Advertising;


[CreateAssetMenu(fileName = "AdvertisementConfig", menuName ="Configs/AdConfig")]
public class AdvertisementConfig : ScriptableObject, IAdvertisementConfiguration
{
    #region IAdvertisementConfiguration

    public IEnumerable<IAdvertisement> Advertisements => _advertisements;
    public IEnumerable<IPlatformAuth> Platforms => _platforms;

    #endregion

    [SerializeField]
    private Advertisement[] _advertisements;
    [SerializeField]
    private PlatformAuth[] _platforms;
}
