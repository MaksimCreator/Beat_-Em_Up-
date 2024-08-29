using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace View.Panel
{
    public class TutorialAttackView : MonoBehaviour
    {
        [SerializeField] private Button _attack;
        [SerializeField] private TextMeshProUGUI _attackText;

        public void Init(Action onEnd)
        {
            _attack.onClick.RemoveAllListeners();
            _attack.onClick.AddListener(() => onEnd());
        }

        public void DisableText()
        => _attackText.enabled = false;

        public void EnableText()
        => _attackText.enabled = true;

        public void Enable()
        => gameObject.SetActive(true);

        public void Disable()
        => gameObject.SetActive(false);
    }
}
