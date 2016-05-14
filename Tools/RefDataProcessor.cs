﻿//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Saturday, May 14, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.IO;

public class RefDataBase {

    public RefDataBase () {

    }

}

public class RefDataProcessor {

    static List<string> contentLine = new List<string>();
    public static Dictionary<int, T> LoadTable<T> (string _fileName) where T : RefDataBase, new() {
        Dictionary<int, T> dict = new Dictionary<int, T>();

        StreamReader sr = new StreamReader(GetFullPath(_fileName));
        contentLine.Clear();
        while (!sr.EndOfStream) {
            contentLine.Add(sr.ReadLine());
        }
        sr.Close();

        T t = new T();
        FieldInfo[] titlefieldList = t.GetType().GetFields();
        int[] indexSet = new int[titlefieldList.Length];

        string[] fieldName = contentLine[1].Split('\t');
        List<string> propertyName = new List<string>(fieldName);
        for (int i = 0; i < titlefieldList.Length; i++) {
            indexSet[i] = propertyName.IndexOf(titlefieldList[i].Name);
        }

        for (int m = 2; m < contentLine.Count; m++) {
            fieldName = contentLine[m].Split('\t');
            T t1 = new T();
            FieldInfo[] fieldList = t1.GetType().GetFields();

            for (int n = 0; n < fieldList.Length; n++) {
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
                else if (field.FieldType == typeof(bool[])) {
                    string[] str = content.Split(';');
                    field.SetValue(t1, ParseBoolArray(str));
                }
                else if (field.FieldType == typeof(float[])) {
                    string[] str = content.Split(';');
                    field.SetValue(t1, ParseFloatArray(str));
                }
                else if (field.FieldType == typeof(Enum[])) {
                    Type type = field.FieldType.GetType();
                    string[] str = content.Split(';');
                    field.SetValue(t1, ParseEnumArray(type, str));
                }

            }

            int key = (int)fieldList[0].GetValue(t1);
            dict.Add(key, t1);
        }

        return dict;
    }


    public static int[] ParseIntArray (string[] _content) {
        int[] array = new int[_content.Length];

        for (int i = 0; i < _content.Length; i++) {
            array[i] = int.Parse(_content[i]);
        }

        return array;
    }

    public static float[] ParseFloatArray (string[] _content) {
        float[] array = new float[_content.Length];

        for (int i = 0; i < _content.Length; i++) {
            array[i] = float.Parse(_content[i]);
        }

        return array;
    }

    public static bool[] ParseBoolArray (string[] _content) {
        bool[] array = new bool[_content.Length];

        for (int i = 0; i < _content.Length; i++) {
            array[i] = bool.Parse(_content[i]);
        }

        return array;
    }

    public static Enum[] ParseEnumArray (Type euemType, string[] _content) {
        Enum[] array = new Enum[_content.Length];

        for (int i = 0; i < _content.Length; i++) {
            array[i] = Enum.Parse(euemType, _content[i]) as Enum;
        }

        return array;
    }


    private static string GetFullPath (string _fileName) {
        string fullPath = string.Empty;
        string relativePath = StringUtil.StringBuild("Assets/ExternalResources/RefData/", _fileName, ".txt");

        return relativePath;
    }
}



