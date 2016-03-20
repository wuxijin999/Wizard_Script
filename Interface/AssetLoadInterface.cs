using UnityEngine;
using System.Collections;


public interface IAssetLoad {

    void Load_Animation(int _id);
    void Load_AnimationCtrl(int _id);
    void Load_Card(int _id);
    void Load_Effect(int _id);
    void Load_Level(int _id);
    void Load_Sound(int _id);
    void Load_Window(int _id);
    void Load_Sprite(int _id);
    void Load_UIController(int _id);

}