using UnityEngine;
using System.Collections;
using UI;

public class GameLanucher : MonoBehaviour {

    private void Start() {

        GameObject gameBase = new GameObject("GameBase");
        gameBase.AddMissComponent<ScenesManager>();
        MonoBehaviour.DontDestroyOnLoad(gameBase);

        //创建游戏基础物件
        WindowMgr.Instance.Init();
        ScenesManager.Instance.LoadLevel("InfiniteScrollRect", SceneType.Main, null, null);

    }


    void CreatBasicGameObject() {


    }


}
