using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesConfigeLoader : IConfigsLoader
{
    private readonly ResourcesAssetsLoader _resources;

    public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoader)
    {
        throw new NotImplementedException();
    }
}
