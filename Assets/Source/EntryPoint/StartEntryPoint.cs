using Model;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEntryPoint : MonoBehaviour
{
    [SerializeField] private StartMenuView _menuView;

    private void Awake()
    {
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        DataSave saveService = new DataSave();
        Data data = saveService.Load();

        Action start = data == null || data.isStart == false ? () => SceneManager.LoadScene(Constant.TytorialScenes) : () => SceneManager.LoadScene(Constant.GamplayScenes);
        int score = data == null ? 0 : data.MyBest;
        _menuView.Init(Application.Quit, start, score);
    }
}
