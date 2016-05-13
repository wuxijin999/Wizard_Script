//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Saturday, May 14, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System;

public class RefDataBase {

    public RefDataBase() {

    }

}

public class RefDataProcessor {


    public static void LoadTable<T>(Dictionary<int, T> _dict, string[] _lines) where T : RefDataBase, new() {

        T t = new T();
        FieldInfo[] titlefieldList = t.GetType().GetFields();
        int[] indexSet = new int[titlefieldList.Length];

        List<string> propertyName = new List<string>(_lines[1].Split('\t'));
        for (int i = 0; i < titlefieldList.Length; i++) {
            indexSet[i] = propertyName.IndexOf(titlefieldList[i].FieldType.ToString());
        }

        for (int m = 2; m < _lines.Length; m++) {
            for (int n = 0; n < _lines[m].Length; n++) {
                T t1 = new T();
                FieldInfo[] fieldList = t1.GetType().GetFields();
                fieldList[indexSet[n]].SetValue(t1, _lines[m][n]);
            }
        }

    }


    



}



