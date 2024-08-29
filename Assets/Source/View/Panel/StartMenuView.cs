using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuView : MonoBehaviour 
{
    [SerializeField] private Button _exit;
    [SerializeField] private Button _start;
    [SerializeField] private TextMeshProUGUI _score;

    public void Init(Action exit,Action start,int score)
    {
        _exit.onClick.RemoveAllListeners();
        _start.onClick.RemoveAllListeners();

        _exit.onClick.AddListener(() => exit());
        _start.onClick.AddListener(() => start());

        _score.text = "MyBest: " + score.ToString();
    }
}
