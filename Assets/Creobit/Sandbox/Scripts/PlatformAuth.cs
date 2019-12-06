using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlatformAuth : ScriptableObject, IPlatformAuth
{
    #region IPlatformAuth

    public string Id => Platform.ToString();

    public abstract bool IsInitialized { get; }

    public abstract void Initialize(Action onSuccess, Action onFailed);

    #endregion

    protected abstract AdvertisementPlatforms Platform { get; }
}
