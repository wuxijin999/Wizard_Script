#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class AssetLoad {

    private static string Animation_Path = "Assets/Animation/";
    private static string Card_Path = "Assets/Card/";
    private static string Effect_Path = "Assets/Effect/";
    private static string Sound_Path = "Assets/Sound/";
    private static string UI_Path = "Assets/UI/";
    private static string Sprite_Path = "Assets/UI/Sprite/";

    public static AnimationClip LoadAnimation(string _name) {
        AnimationClip clip = null;

        var path = StringUtil.StringBuild(Animation_Path,_name,".anim");
        clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path) as AnimationClip;
        if(clip == null) {
            Debug.Log(string.Format("加载动画资源失败，资源路径: {0}",path));
        }
        return clip;
    }

    public static GameObject LoadCard(string _name) {
        GameObject card = null;
        var path = StringUtil.StringBuild(Card_Path,_name,".prefab");
        card = AssetDatabase.LoadAssetAtPath<GameObject>(path) as GameObject;
        return card;
    }

    public static GameObject LoadEffect(string _name) {
        GameObject effect = null;
        var path = StringUtil.StringBuild(Effect_Path,_name,".prefab");
        effect = AssetDatabase.LoadAssetAtPath<GameObject>(path) as GameObject;
        return effect;
    }

    public static AudioClip LoadAudioClip(string _name) {
        AudioClip audioClip = null;
        var path = StringUtil.StringBuild(Sound_Path,_name,".prefab");
        audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(path) as AudioClip;
        return audioClip;
    }

    public static GameObject LoadUI(string _name) {
        GameObject winPrefab = null;
        var path = StringUtil.StringBuild(UI_Path,_name,".prefab");
        winPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path) as GameObject;
        return winPrefab;
    }

    public static Sprite LoadSprite(string _name) {
        Sprite sprite = null;
        var path = StringUtil.StringBuild(Sprite_Path,_name,".prefab");
        sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path) as Sprite;
        return sprite;
    }

}
