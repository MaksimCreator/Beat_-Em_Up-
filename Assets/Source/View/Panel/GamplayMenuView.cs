using Model;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class GamplayMenuView : MonoBehaviour
    {
        [SerializeField] private Slider _health;
        [SerializeField] private Button _exit;
        [SerializeField] private TextMeshProUGUI _score;

        private Mediator _mediator;
        private bool _isInitialized;

        public void Init(Action exit, Mediator mediator)
        {
            _mediator = mediator;
            _exit.onClick.RemoveAllListeners();

            _exit.onClick.AddListener(() =>
            {
                Disable();
                exit();
            });

            _score.text = _mediator.Score.ToString();
            _health.maxValue = mediator.MaxHealth;
            _health.minValue = mediator.MinHealth;
            _health.value = mediator.CurentHealth;

            _isInitialized = true;
        }

        public void Enable()
        {
            if (_isInitialized == false)
                throw new InvalidOperationException(nameof(_isInitialized));

            if (enabled != true)
            {
                enabled = true;
                OnEnable();
            }
        }

        public void Disable()
        { 
            if (enabled != false)
            {
                OnDisable();
                enabled = false;
            }
        }

        private void OnEnable()
        {
            gameObject.SetActive(true);
            StartCoroutine(Updateble());
        }

        private void OnDisable()
        {
            gameObject.SetActive(false);
            StopCoroutine(Updateble());
        }

        private IEnumerator Updateble()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.4f);

                if (_mediator != null)
                {
                    _score.text = _mediator.Score.ToString();
                    _health.value = _mediator.CurentHealth;
                }
            }
        }
    }
}
