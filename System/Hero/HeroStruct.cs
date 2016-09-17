//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Friday, September 16, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;


public struct HeroData {
    public int id;
    public bool isActive;
    public HeroBasicData basicData;
    public HeroEquipData equipData;
    public HeroMainProperty mainProperty;
}

public struct HeroBasicData {
    public int level;
    public int starLevel;
    public int rankLevel;
    public int breakLevel;
}


public struct HeroEquipData {
    public int weaponId;
    public int helmetId;
    public int breastId;
    public int legId;
    public int bootId;
    public int gloveId;
}

public struct HeroMainProperty {
    public int strength;
    public int intelligence;

}








