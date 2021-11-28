    using System;
    using UnityEngine;

    public class TimedDestroy : MonoBehaviour
    {
        public float delay;

        private void Start()
        {
            Destroy(gameObject);
        }
    }