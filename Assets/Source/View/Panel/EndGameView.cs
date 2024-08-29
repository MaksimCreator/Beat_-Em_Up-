using Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace View
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private Button _restart;
        [SerializeField] private Button _menu;
        [SerializeField] private TextMeshProUGUI _score;

        private void Awake()
        {
            _restart.onClick.RemoveAllListeners();
            _menu.onClick.RemoveAllListeners();

            _restart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
            _menu.onClick.AddListener(() => SceneManager.LoadScene(Constant.StartMenuScenes));
        }

        public void Enable(int score)
        { 
            _score.text = "Ваш Счет: " + score.ToString();
            gameObject.SetActive(true);
        }
    }
}
