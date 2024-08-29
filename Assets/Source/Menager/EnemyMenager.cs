using Model;
using UnityEngine;
using System;

namespace Menager
{
    public class EnemyMenager : MonoBehaviour
    {
        private EnemyController _controller;
        private bool _isInitialized;

        public void Init(EnemyController controller)
        {
            _controller = controller;
            _isInitialized = true;
        }

        public void Enable()
        {
            if (_isInitialized == false)
                throw new InvalidOperationException(nameof(_isInitialized));

            enabled = true;
        }

        public void Disable()
        => enabled = false;

        public void AllStop()
        => _controller.AllStop();

        private void Update()
        => _controller.Update(Time.deltaTime);

        private void OnEnable()
        => _controller.Enable();

        private void OnDisable()
        => _controller.Disable();
    }
}