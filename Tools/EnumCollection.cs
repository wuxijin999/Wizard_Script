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
    Alpha,
    Scale,
    AlphaScale,
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
    Friendly=0,
    Hostile=1,
    Neutral=2
}

public enum EnemyCategory {
    Normal=0,
    Elite=1,
    Boss=2,

}

public enum SceneType {
    Launch,
    Login,
    Main,
    Fight,
}