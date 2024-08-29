using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ExitMenuView : MonoBehaviour
    {
        [SerializeField] private Button _exit;
        [SerializeField] private Button _back;
        [SerializeField] private TextMeshProUGUI _score;

        public void Init(Action exit, Action back)
        {
            _exit.onClick.RemoveAllListeners();
            _back.onClick.RemoveAllListeners();

            _exit.onClick.AddListener(() =>
            {
                Disable();
                exit();
            });
            _back.onClick.AddListener(() =>
            {
                Disable();
                back();
            });
        }

        public void Enable(int score)
        {
            gameObject.SetActive(true);
            _score.text = "Ваш Счет: " + score.ToString();
        }

        private void Disable()
        => gameObject.SetActive(false);
    }
}
