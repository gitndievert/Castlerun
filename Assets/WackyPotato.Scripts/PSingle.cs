using UnityEngine;
using System.Collections;

namespace SBK.Unity
{
    /// <summary>
    /// Persistant Singletons through Unity Scenes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PSingle<T> : MonoBehaviour where T : PSingle<T>
    {
        private static T _instance;

        public static T Instance
        {
            get { return _instance; }
        }

        void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            if (gameObject.transform.parent == null)
                DontDestroyOnLoad(gameObject);
            _instance = (T)this;
            PAwake();
        }

        void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
            PDestroy();
        }
        
        protected abstract void PAwake();
        protected abstract void PDestroy();
    }
}
