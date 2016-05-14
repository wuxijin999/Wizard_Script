//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Saturday, May 14, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System;
using System.Reflection;

public class RefDataManager : Singleton<RefDataManager> {

    #region Fields
    public Dictionary<int, RefSkill> skill;
    #endregion


    public override void Init () {
        base.Init();

        skill = ParseRefData<RefSkill>("skill");
    }

    public override void UnInit () {


        base.UnInit();
    }


    private Dictionary<int, T> ParseRefData<T> (string _fileName) where T : RefDataBase, new() {
        return RefDataProcessor.LoadTable<T>(_fileName);
    }


}



