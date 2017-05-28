//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [ Date  ]:           Sunday, May 01, 2016
//--------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;


public class SceneAsyncOption {
    public float progress {
        get; set;
    }

    public bool done {
        get; set;
    }

    DateTime beginTime;
    public float time {
        get {
            return (float)(DateTime.Now - beginTime).TotalMilliseconds / 1000f;
        }
    }

    public SceneAsyncOption() {
        progress = 0f;
        done = false;
        beginTime = DateTime.Now;
    }

}


public class ScenesManager:SingletonMonobehavior<ScenesManager> {

    #region Fields

    private string m_CurrentSceneName;
    public string currentSceneName {
        get {
            return m_CurrentSceneName;
        }
        private set {
            m_CurrentSceneName = value;
        }
    }

    private SceneType m_LastType;
    public SceneType lastType {
        get {
            return m_LastType;
        }
        private set {
            m_LastType = value;
        }
    }

    private SceneType m_CurrentType;
    public SceneType currentType {
        get {
            return m_CurrentType;
        }
        private set {
            m_CurrentType = value;
        }
    }

    #endregion


    public SceneAsyncOption LoadLevel(string _sceneName,SceneType _type) {

        var async = new SceneAsyncOption();

        currentSceneName = _sceneName;
        lastType = currentType;
        currentType = _type;

        SceneManager.LoadSceneAsync("Empty",LoadSceneMode.Single);
        async.progress = 0.1f;
        System.GC.Collect();
        async.progress = 0.2f;
        Resources.UnloadUnusedAssets();
        async.progress = 0.3f;
        StartCoroutine(Co_LoadScene(LoadSceneMode.Single,async));

        return async;
    }

    public SceneAsyncOption LoadLevelAdditive(string _sceneName,SceneType _type) {

        var async = new SceneAsyncOption();
        currentSceneName = _sceneName;
        lastType = currentType;
        currentType = _type;

        StartCoroutine(Co_LoadScene(LoadSceneMode.Additive,async));
        return async;
    }

    IEnumerator Co_LoadScene(LoadSceneMode _mode,SceneAsyncOption _async) {

        var startProgress = _async.progress;
        var asyncOperation = SceneManager.LoadSceneAsync(currentSceneName,_mode);
        while(asyncOperation != null && !asyncOperation.isDone) {
            _async.progress = startProgress + asyncOperation.progress * 0.5f;
            yield return null;
        }

        if(_mode == LoadSceneMode.Single) {
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
        }

        _async.progress = 1f;
        _async.done = true;
        WDebug.Log(string.Format("Load {0} use<color=yellow> {1} </color>second",currentSceneName,_async.time));
    }


}



