using UnityEngine;
using Creobit.Advertising;
using System;

public abstract class Advertisement : ScriptableObject, IAdvertisement
{
    #region IAdvertisement

    public abstract bool IsReady { get;}

    public string PlatformId => Platform.ToString();

    public string Tag => _tag;

    public abstract void Prepare(Action onComplete, Action onFailure);

    public abstract void Show(Action onComplete, Action onSkip, Action onFailure);

    #endregion

    protected abstract AdvertisementPlatforms Platform { get; }

    [SerializeField]
    private string _tag;
}
