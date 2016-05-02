//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Monday, May 02, 2016
//--------------------------------------------------------
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System;

public class MVCCodeBuilder : MonoBehaviour {

    public const string relativeFilePath = @"Assets/Wizard_Script/ScriptTemplate/";

    public string fileName;

    public void CodeBuilder() {

        string path = relativeFilePath + fileName + ".txt";
        string fullPath = Path.GetFullPath(path);
        StreamReader sr = new StreamReader(fullPath);
        StringBuilder textStr = new StringBuilder();

        while (!sr.EndOfStream) {
            string nextLine = sr.ReadLine();
            if (!string.IsNullOrEmpty(nextLine)) {
                StringBuilder newStr = new StringBuilder();
                newStr.Append("public void ");
                newStr.Append(nextLine);
                newStr.Append("(Action<bool> _callBack){\n}");
                newStr.AppendLine();
                newStr.Append("private void ");
                newStr.Append(nextLine);
                newStr.Append("CallBack");
                newStr.Append("(bool _isOk){");
                newStr.AppendLine();
                newStr.Append("if(_isOk){");
                newStr.AppendLine("");
                newStr.Append(" }");
                newStr.AppendLine("else{");
                newStr.AppendLine(" }");
                newStr.AppendLine(" }");

                nextLine = Regex.Replace(nextLine, nextLine, newStr.ToString());
                textStr.AppendLine(nextLine);
            }
        }

        sr.Close();

        bool encoderShouldEmitUTF8Identifier = true;
        bool throwOnInvalidBytes = false;
        UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
        bool append = false;
        StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
        streamWriter.Write(textStr);
        streamWriter.Close();
        AssetDatabase.ImportAsset(path);

    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            CodeBuilder();
        }
    }

}



