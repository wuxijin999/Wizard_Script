//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Monday, May 02, 2016
//--------------------------------------------------------
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System;

public class MVCCodeGenerator : MonoBehaviour {

    public const string relativeFilePath = @"Assets/Wizard_Script/ScriptTemplate/";

    public string fileName;

    public void CodeBuilder() {

        string path = relativeFilePath + fileName + ".txt";
        string fullPath = Path.GetFullPath(path);
        StreamReader sr = new StreamReader(fullPath);
        StringBuilder textStr = new StringBuilder();

        List<string> allLine = new List<string>();
        while (!sr.EndOfStream) {
            string nextLine = sr.ReadLine();
            if (!string.IsNullOrEmpty(nextLine)) {
                allLine.Add(nextLine);
            }
        }

        sr.Close();

        textStr.Append("#region Action\n");
        for (int i = 0; i < allLine.Count; i++) {
            textStr.AppendLine(MethodGenerator(allLine[i]));
            textStr.Append("\n");
        }
        textStr.Append("#endregion\n");

        textStr.Append("\n");
        textStr.Append("#region CallBack \n");
        for (int i = 0; i < allLine.Count; i++) {
            textStr.AppendLine(CallBackGenerator(allLine[i]));
            textStr.Append("\n");
        }
        textStr.Append("#endregion");

        bool encoderShouldEmitUTF8Identifier = true;
        bool throwOnInvalidBytes = false;
        UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
        bool append = false;
        StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
        streamWriter.Write(textStr);
        streamWriter.Close();
        AssetDatabase.ImportAsset(path);

    }


    private string FiledGenerator(string _fieldName) {
        StringBuilder sb = new StringBuilder();

        sb.Append(string.Format("private "));

        return sb.ToString();
    }

    private string MethodGenerator(string _methodName) {
        StringBuilder sb = new StringBuilder();
        sb.Append("public void ");
        sb.Append(_methodName);
        sb.Append("(Action<bool> _callBack){\n\n}");

        return sb.ToString();
    }

    private string CallBackGenerator(string _methodName) {
        StringBuilder sb = new StringBuilder();

        sb.Append("private void ");
        sb.Append(_methodName);
        sb.Append("CallBack(bool _isOk){\nif(_isOk){\n}else\n{\n}\n}");

        return sb.ToString();
    }


    public void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            CodeBuilder();
        }
    }

}



