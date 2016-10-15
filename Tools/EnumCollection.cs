using System.Collections;


public enum WindowType {
    Normal,
    Modal,
    Tip,
    System
}

public enum WinAnimType {
    None,
    OffSet,
    Scale,
}

public enum WinOffsetAnimStyle {
    L2R,
    R2L,
    T2B,
    B2T
}

public enum GestureType {
    Left,
    Right,
    Up,
    Down
}

public enum CampType {
    Friendly = 0,
    Hostile = 1,
    Neutral = 2
}

public enum EnemyCategory {
    Normal = 0,
    Elite = 1,
    Boss = 2,

}

public enum SceneType {
    Launch,
    Login,
    Main,
    Fight,
}



public enum AttackMode {
    Melee,
    Project,
}

public enum TargetMode {
    Target,                                  //需要目标
    Notarget,                              //不需要目标
}

public enum Faction {
    None,
    Oppose,
    Neutral,
    Friend,
}

public enum DamageType {
    Normal,                        
    Puncture,                                              //穿刺
    Smash,                                                  //粉碎攻击
    Magic,                                                   //魔法攻击
    Hero,                                                     //英雄攻击
    Holy,                                                     //神圣攻击
}

public enum DefenceType {
    None,                                          
    Spardeck,                                    //轻甲
    Plated,                                        //重甲
    Magic,                                        // 魔法防御
    Hero,                                          //英雄防御
    Holy,                                           //神圣防御
}

