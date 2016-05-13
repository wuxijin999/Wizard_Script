//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Monday, May 02, 2016
//--------------------------------------------------------
using UnityEngine;
using UnityEditor.ProjectWindowCallback;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System;

public class MVCCodeGenerator {

    public const string relativeFilePath = @"Assets/Wizard_Script/ScriptTemplate/model.txt";

    [MenuItem("Assets/Create/MVCCodeGenerator", false, 3)]
    public static void CreatMVCCodeScript() {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<DoCreateMVCScript>(),
        GetSelectedPathOrFallback() + "/NewView.cs",
        null,
       relativeFilePath);

    }

    public static string GetSelectedPathOrFallback() {
        string path = "Assets";
        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets)) {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path)) {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }

}

class DoCreateMVCScript : EndNameEditAction {

    public override void Action(int instanceId, string pathName, string resourceFile) {
        UnityEngine.Object o = CreateScriptAssetFromTemplate(pathName, resourceFile);
        ProjectWindowUtil.ShowCreatedAsset(o);
    }

    internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile) {
        string fullPath = Path.GetFullPath(pathName);
        StreamReader sr = new StreamReader(resourceFile);
        StringBuilder textStr = new StringBuilder();

        List<string> allLine = new List<string>();
        while (!sr.EndOfStream) {
            string nextLine = sr.ReadLine();
            if (!string.IsNullOrEmpty(nextLine)) {
                allLine.Add(nextLine);
            }
        }

        sr.Close();

        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);

        textStr.Append(UsingNameSpaceGenerator());
        textStr.Append("\n");
        textStr.Append(ClassNameGenetor(fileNameWithoutExtension));

        for (int i = 0; i < allLine.Count; i++) {
            textStr.AppendLine(DelegateGenerator(allLine[i]));
        }

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

        textStr.Append("\n}");

        bool encoderShouldEmitUTF8Identifier = true;
        bool throwOnInvalidBytes = false;
        UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
        bool append = false;
        StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
        streamWriter.Write(textStr);
        streamWriter.Close();
        AssetDatabase.ImportAsset(pathName);
        return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
    }


    private static string UsingNameSpaceGenerator() {
        StringBuilder sb = new StringBuilder();
        sb.Append("using System;\n");
        sb.Append("using System.Collections;\n");
        sb.Append("using UnityEngine;\n");
        return sb.ToString();
    }

    private static string ClassNameGenetor(string _className) {
        StringBuilder sb = new StringBuilder();
        sb.Append("public class ");
        sb.Append(_className);
        sb.Append(" {\n");

        return sb.ToString();

    }

    private static string FiledGenerator(string _fieldName) {
        StringBuilder sb = new StringBuilder();

        sb.Append(string.Format("private "));

        return sb.ToString();
    }

    private static string DelegateGenerator(string _methodName) {
        StringBuilder sb = new StringBuilder();
        sb.Append("Action<bool>  ");
        sb.Append(_methodName);
        sb.Append("CallBack=null;");

        return sb.ToString();
    }
    private static string MethodGenerator(string _methodName) {
        StringBuilder sb = new StringBuilder();
        sb.Append("public void ");
        sb.Append(_methodName);
        sb.Append("(Action<bool> _callBack){\n");
        sb.Append(_methodName);
        sb.Append("CallBack=_callBack;\n");
        sb.Append(" \n}");

        return sb.ToString();
    }

    private static string CallBackGenerator(string _methodName) {
        StringBuilder sb = new StringBuilder();

        sb.Append(" private void On");
        sb.Append(_methodName);
        sb.Append("CallBack(bool _isOk) {\nif (_isOk) {\n}\nelse {\n}\nif (");
        sb.Append(_methodName);
        sb.Append("CallBack != null) {\n");
        sb.Append(_methodName);
        sb.Append("CallBack(_isOk);\n}\n}");

        return sb.ToString();
    }

}


