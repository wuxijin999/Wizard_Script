//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Saturday, May 14, 2016
//--------------------------------------------------------
using System.Collections.Generic;
using System;
using System.Threading;

public class RefDataManager : Singleton<RefDataManager> {

    #region Fields
    public Dictionary<int, RefSkill> skill { get; private set; }
    public Dictionary<int, RefHitData> hitData { get; private set; }
    public Dictionary<int, RefWindowConfig> windowConfig { get; private set; }
    public Dictionary<int, RefEffect> effect { get; private set; }
    #endregion

    public override void Init () {
        base.Init();

        //         long startTick = DateTime.Now.Ticks;
        //         //  effect = ParseRefData<RefEffect>("effect");
        //         skill = ParseRefData<RefSkill>("skill");
        //         long endTicks = DateTime.Now.Ticks;
        // 
        //         WDebug.Log("时长：" + (endTicks - startTick) / 10000);

        Thread trh = new Thread(LoadSkill);
        trh.Start();

        //  hitData = ParseRefData<RefHitData>("hitdata");
        //  windowConfig = ParseRefData<RefWindowConfig>("windowConfig");
    }

    public override void UnInit () {

        base.UnInit();
    }


    private void LoadSkill () {
        long startTick = DateTime.Now.Ticks;

        for (int i = 0; i < 100; i++) {
            skill = ParseRefData<RefSkill>("skill");
        }

        long endTicks = DateTime.Now.Ticks;
        WDebug.Log("时长：" + (endTicks - startTick) / 10000);
    }


    private Dictionary<int, T> ParseRefData<T> (string _fileName) where T : RefDataBase, new() {
        return RefDataProcessor.LoadTable<T>(_fileName);
    }


}



