﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PamisuKit.Framework
{
    public abstract class AppDirector : MonoBehaviour, IDirector
    {
        
        protected Dictionary<Type, ISystem> SystemDict { get; set; }
        protected Dictionary<Type, object> ServiceDict { get; set; }
        
        public Transform Trans { get; private set; }
        public GameObject Go { get; private set; }
        public Ticker Ticker { get; private set; }
        public Region Region { get; private set; }
        public Director Director { get; private set; }

        protected virtual void Awake()
        {
            Go = gameObject;
            Trans = Go.transform;
            SystemDict = new Dictionary<Type, ISystem>();
            ServiceDict = new Dictionary<Type, object>();
            Ticker = gameObject.AddComponent<Ticker>();
            Region = gameObject.AddComponent<Region>();
            Region.Init(Ticker, this);
            OnCreate();
        }

        protected virtual void OnCreate()
        {
            Director = FindFirstObjectByType<Director>();
            Debug.Assert(Director != null, "There must be one director in the scene.");
            if (Director.Mode == DirectorMode.Global)
                Director.transform.SetParent(transform);
            Director.Setup(this);

            if (Director.Mode != DirectorMode.Global)
                SceneManager.sceneLoaded += OnSceneLoaded;
        }

        protected void OnDestroy()
        {
            if (SystemDict == null)
                return;
            foreach (var it in SystemDict.Values)
            {
                if (it is MonoSystem monoSystem)
                    Destroy(monoSystem.gameObject);
            }
            SystemDict.Clear();
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode == LoadSceneMode.Additive)
                return;
            
            var director = FindFirstObjectByType<Director>();
            if (director != null && director != Director)
            {
                Director = director;
                Director.Setup(this);
                if (Director.Mode == DirectorMode.Global)
                {
                    Director.transform.SetParent(transform);
                    SceneManager.sceneLoaded -= OnSceneLoaded;
                }
            }
        }

        public virtual TSystem CreateMonoSystem<TSystem>(IDirector owner = null) where TSystem : MonoSystem
        {
            var type = typeof(TSystem);
            if (SystemDict.ContainsKey(type))
            {
                Debug.LogError($"{GetType().Name} The system({type}) you want to create already exists.");
                return null;
            }
            
            if (owner == null)
                owner = this;
            var system = owner.Trans.GetComponentInChildren<TSystem>();
            if (system == null)
            {
                var go = new GameObject(typeof(TSystem).Name);
                go.transform.SetParent(owner.Trans);
                system = go.AddComponent<TSystem>();
            }
            SystemDict.Add(type, system);
            system.Setup(owner.Region);
            return system;
        }

        public TSystem GetSystem<TSystem>() where TSystem : class, ISystem
        {
            if (SystemDict.TryGetValue(typeof(TSystem), out var system))
                return system as TSystem;
            return null;
        }

        public ISystem GetSystem(Type type)
        {
            if (SystemDict.TryGetValue(type, out var system))
                return system;
            return default;
        }

        public virtual void DestroySystem(ISystem system)
        {
            SystemDict.Remove(system.GetType());
            if (system is MonoSystem monoSystem)
                Destroy(monoSystem.gameObject);
        }

        public void RegisterService<TService>(TService service) where TService : class
        {
            ServiceDict[typeof(TService)] = service;
        }

        public TService GetService<TService>() where TService : class
        {
            if (ServiceDict.TryGetValue(typeof(TService), out var service))
                return service as TService;
            return null;
        }

        public bool RemoveService<TService>()
        {
            return ServiceDict.Remove(typeof(TService));
        }

        public bool RemoveService(object service)
        {
            return ServiceDict.Remove(service.GetType());
        }
        
    }
    
    public abstract class AppDirector<T> : AppDirector where T : AppDirector
    {

        public static T Instance { get; private set; }

        [SerializeField]
        private bool _dontDestroyOnLoad = false;

        protected override void Awake()
        {
            if (Instance != null)
            {
                // Destroy(gameObject);
                DestroyImmediate(gameObject);
                return;
            }
            Instance = GetComponent<T>();
            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
            base.Awake();
        }
        
    }
    
}