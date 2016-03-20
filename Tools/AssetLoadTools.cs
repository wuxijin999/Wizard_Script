#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class AssetLoadTools {

    private static string Animation_Path = "Assets/ExternalResources/Animation/";
    private static string Card_Path = "Assets/ExternalResources/Card/";
    private static string Effect_Path = "Assets/ExternalResources/Effect/";
    private static string Sound_Path = "Assets/ExternalResources/Sound/";
    private static string UI_Path = "Assets/ExternalResources/UI/";
    private static string Sprite_Path = "Assets/ExternalResources/UI/Sprite/";

    /// <summary>
    /// 加载动画
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static AnimationClip Load_Animation(string _name) {
        AnimationClip clip = null;

        string path = Animation_Path + _name + ".anim";
        clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path) as AnimationClip;
        return clip;
    }

    /// <summary>
    /// 加载卡片
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static GameObject Load_Card(string _name) {
        GameObject card = null;

        string path = Card_Path + _name + ".prefab";
        card = AssetDatabase.LoadAssetAtPath<GameObject>(path) as GameObject;
        return card;
    }

    /// <summary>
    /// 加载特效
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static GameObject Load_Effect(string _name) {
        GameObject effect = null;

        string path = Effect_Path + _name + ".prefab";
        effect = AssetDatabase.LoadAssetAtPath<GameObject>(path) as GameObject;
        return effect;
    }

    /// <summary>
    /// 加载声音
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static AudioClip Load_Sound(string _name) {
        AudioClip audioClip = null;

        string path = Sound_Path + _name + ".prefab";
        audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(path) as AudioClip;
        return audioClip;
    }

    /// <summary>
    /// 加载窗口
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static GameObject Load_UI(string _name) {
        GameObject winPrefab = null;

        string path = UI_Path + _name + ".prefab";
        winPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path) as GameObject;
        return winPrefab;
    }

    /// <summary>
    /// 加载图片精灵
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static Sprite Load_Sprite(string _name) {
        Sprite sprite = null;

        string path = Sprite_Path + _name + ".sprite";
        sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path) as Sprite;
        return sprite;
    }

}
