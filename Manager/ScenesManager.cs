//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [ Date  ]:           Sunday, May 01, 2016
//--------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class ScenesManager : SingletonMonobehavior<ScenesManager> {

    public delegate void ProgressHandler (float _percent);
    public event ProgressHandler ProgressEvent;

    #region Fields

    static DateTime startTime;
    static DateTime endTime;

    private string currentSceneName;
    public string CurrentSceneName {
        get {
            return currentSceneName;
        }
    }

    private SceneType lastType;
    public SceneType LastType {
        get {
            return lastType;
        }
    }

    private SceneType currentType;
    public SceneType CurrentType {
        get {
            return currentType;
        }
    }

    #endregion


    public void LoadLevel (string _sceneName, SceneType _type, Action _beginCallBack, Action _endCallBack) {

        startTime = DateTime.Now;

        currentSceneName = _sceneName;
        lastType = currentType;
        currentType = _type;

        if (_beginCallBack != null) {
            _beginCallBack();
        }

        SceneManager.LoadSceneAsync("Empty", LoadSceneMode.Single);
        System.GC.Collect();
        Resources.UnloadUnusedAssets();

        StartCoroutine(Co_LoadScene(LoadSceneMode.Single, _endCallBack));
    }

    public void LoadLevelAdditive (string _sceneName, SceneType _type, Action _beginCallBack, Action _endCallBack) {

        startTime = DateTime.Now;

        currentSceneName = _sceneName;
        lastType = currentType;
        currentType = _type;

        if (_beginCallBack != null) {
            _beginCallBack();
        }

        StartCoroutine(Co_LoadScene(LoadSceneMode.Additive, _endCallBack));
    }

    IEnumerator Co_LoadScene (LoadSceneMode _mode, Action _endCallBack) {

        Application.targetFrameRate = 1000;

        if (_mode == LoadSceneMode.Single) {
            BroadcastProgress(10);
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(currentSceneName, _mode);
        while (asyncOperation != null && !asyncOperation.isDone) {
            if (_mode == LoadSceneMode.Single) {
                BroadcastProgress((int)(10 + 79 * asyncOperation.progress));
            }
            yield return null;
        }

        if (_mode == LoadSceneMode.Single) {
            BroadcastProgress(100);
        }

        if (_mode == LoadSceneMode.Single) {
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
        }

        if (_endCallBack != null) {
            _endCallBack();
        }

        endTime = DateTime.Now;
        WDebug.Log(string.Format("Load {0} use<color=yellow> {1} </color>second", currentSceneName, (endTime - startTime).TotalSeconds));
    }

    private void BroadcastProgress (int _progress) {

        WDebug.Log(string.Format("场景加载进度 ：{0}", _progress));

        if (ProgressEvent != null) {
            ProgressEvent(_progress * 0.01f);
        }
    }
}



