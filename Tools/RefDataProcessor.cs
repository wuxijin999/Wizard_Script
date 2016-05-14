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

        string[] fieldName = _lines[1].Split('\t');
        List<string> propertyName = new List<string>(fieldName);
        for (int i = 0; i < titlefieldList.Length; i++) {
            indexSet[i] = propertyName.IndexOf(titlefieldList[i].FieldType.ToString());
        }

        for (int m = 2; m < _lines.Length; m++) {
            fieldName = _lines[m].Split('\t');
            T t1 = new T();
            FieldInfo[] fieldList = t1.GetType().GetFields();

            for (int n = 0; n < fieldName.Length; n++) {
                FieldInfo field = fieldList[indexSet[n]];
                string content = fieldName[n];
                if (field.FieldType == typeof(int)) {
                    field.SetValue(t1, int.Parse(content));
                }
                else if (field.FieldType == typeof(string)) {
                    field.SetValue(t1, content);
                }
                else if (field.FieldType == typeof(Enum)) {
                    field.SetValue(t1, Enum.Parse(field.FieldType, content));
                }
                else if (field.FieldType == typeof(bool)) {
                    field.SetValue(t1, bool.Parse(content));
                }
                else if (field.FieldType == typeof(float)) {
                    field.SetValue(t1, float.Parse(content));
                }
                else if (field.FieldType == typeof(int[])) {
                    string[] str = content.Split(';');
                    field.SetValue(t1, ParseIntArray(str));
                }
                else if (field.FieldType == typeof(bool)) {
                    string[] str = content.Split(';');
                    field.SetValue(t1, ParseBoolArray(str));
                }

                _dict.Add((int)fieldList[0].GetValue(t1), t1);
            }
        }

    }


    public static int[] ParseIntArray(string[] _content) {
        int[] array = new int[_content.Length];

        for (int i = 0; i < _content.Length; i++) {
            array[i] = int.Parse(_content[i]);
        }

        return array;
    }

    public static float[] ParseFloatArray(string[] _content) {
        float[] array = new float[_content.Length];

        for (int i = 0; i < _content.Length; i++) {
            array[i] = float.Parse(_content[i]);
        }

        return array;
    }

    public static bool[] ParseBoolArray(string[] _content) {
        bool[] array = new bool[_content.Length];

        for (int i = 0; i < _content.Length; i++) {
            array[i] = bool.Parse(_content[i]);
        }

        return array;
    }


}



