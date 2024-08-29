using UnityEngine;
using System;
using Model;

namespace Menager
{
    public class PlayerMenager : MonoBehaviour
    {
        private PlayerController _controller;
        private bool _isInitialized;

        public void Init(CharacterController characterController,Camera camera,InputRouter router,Animator animator,Transform transform,Player player)
        {
            _controller = new PlayerController(characterController,camera,router, animator, transform, player);
            _isInitialized = true;
        }

        public void Disable()
        => enabled = false;

        public void Enable()
        {
            if (_isInitialized == false)
                throw new InvalidOperationException();

            enabled = true;
        }

        public void AllStop()
        => _controller.Disable();

        private void OnDisable()
        => _controller.Disable();

        private void OnEnable()
        => _controller.Enable();

        private void Update()
        => _controller.Update(Time.deltaTime);
    }
}