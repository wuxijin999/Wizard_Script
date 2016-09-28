//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Saturday, May 14, 2016
//--------------------------------------------------------
using System.Collections.Generic;

public class RefDataManager : Singleton<RefDataManager> {

    #region Fields
    public Dictionary<int, RefSkill> skill {
        get; private set;
    }

    public Dictionary<int, RefHitData> hitData {
        get; private set;
    }

    public Dictionary<int, RefWindowConfig> windowConfig {
        get; private set;
    }
    #endregion


    public override void Init () {
        base.Init();

        skill = ParseRefData<RefSkill>("skill");
        hitData = ParseRefData<RefHitData>("hitdata");
        windowConfig = ParseRefData<RefWindowConfig>("windowConfig");
    }

    public override void UnInit () {

        base.UnInit();
    }


    private Dictionary<int, T> ParseRefData<T> (string _fileName) where T : RefDataBase, new() {
        return RefDataProcessor.LoadTable<T>(_fileName);
    }


}



