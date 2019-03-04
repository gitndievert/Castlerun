using UnityEngine;
using System.Collections;

namespace SBK.Unity
{
    /// <summary>
    /// Singletons that do not persist between scene loads
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DSingle<T> : MonoBehaviour where T : DSingle<T>
    {
        private static T _instance;

        public static T Instance
        {
            get { return _instance; }
        }

        void Awake()
        {
            if (_instance != null)            
                Destroy(gameObject);                                        
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
