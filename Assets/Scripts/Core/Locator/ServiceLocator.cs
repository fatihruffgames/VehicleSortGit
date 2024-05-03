using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Locator
{
    public class ServiceLocator
    {
        private ServiceLocator() { }

        private readonly Dictionary<string, IService> _services = new();
        public static ServiceLocator Instance { get; private set; }

        public static void Initialize()
        {
            Instance = new ServiceLocator();
        }

        public T Resolve<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)_services[key];
        }

        public void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.TryAdd(key, service))
            {
                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }
            Debug.Log("Registered Service: " + key);
        }

        public void Unregister<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }

            _services.Remove(key);
        }
        
    }
}
