using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManager
{
    public class ResouresConfigsLoader : IConfigsLoader
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configsResourecesPath = new()
        {

        };

        public ResouresConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoader)
        {
            Dictionary<Type, object> loadedConfig = new();

            foreach (KeyValuePair<Type, string> configResourcesPath in _configsResourecesPath)
            {
                ScriptableObject config = _resources.Load<ScriptableObject>(configResourcesPath.Value);
                loadedConfig.Add(configResourcesPath.Key, config);
                yield return null;
            }

            onConfigsLoader?.Invoke(loadedConfig);
        }
    }
}