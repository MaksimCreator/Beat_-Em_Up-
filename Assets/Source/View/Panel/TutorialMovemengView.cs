using TMPro;
using UnityEngine;

namespace View.Panel
{
    public class TutorialMovemengView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _info;

        public void EnableText()
        => _info.enabled = true;

        public void DisableText()
        => _info.enabled = false;

        public void Enable()
        => gameObject.SetActive(true);

        public void Disable()
        => gameObject.SetActive(false);
    }
}
