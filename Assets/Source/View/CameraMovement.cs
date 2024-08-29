using Model;
using System;
using UnityEngine;

namespace View
{
    public class CameraMovement
    {
        private readonly Transform _camera;
        private readonly Transform _player;
        private readonly Vector3 _offset;
        private readonly Quaternion _startRotation;

        public CameraMovement(Transform camera,Transform player)
        {
            _camera = camera;
            _player = player;
            _startRotation = camera.rotation;
            _offset = _camera.position - _player.position;
        }

        public void Update()
        { 
            _camera.position = _player.position + _offset;
            _camera.rotation = _startRotation;
        }
    }
}