using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;
using System.Text.RegularExpressions;

public class UITemplateScript {

    [MenuItem("Assets/Create/C# Custom Script/NewMonobehavior Script", false, 0)]
    public static void CreatNewMonoScript() {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<MyDoCreateScriptAsset>(),
        GetSelectedPathOrFallback() + "/NewMonobehaviorScript.cs",
        null,
       "Assets/Wizard_Script/ScriptTemplate/NewMonobehaviorScriptTemplate.txt");
    }

    [MenuItem("Assets/Create/C# Custom Script/UI View Script", false, 1)]
    public static void CreatUIViewScript() {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<MyDoCreateScriptAsset>(),
        GetSelectedPathOrFallback() + "/NewUIViewScript.cs",
        null,
       "Assets/Wizard_Script/ScriptTemplate/UIViewTemplate.txt");
    }

    [MenuItem("Assets/Create/C# Custom Script/UI Controller Script", false, 2)]
    public static void CreatUIControllerScript() {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<MyDoCreateScriptAsset>(),
        GetSelectedPathOrFallback() + "/NewUIControllerScript.cs",
        null,
       "Assets/Wizard_Script/ScriptTemplate/UIControllerTemplate.txt");
    }

    [MenuItem("Assets/Create/C# Custom Script/UI Model Script", false, 3)]
    public static void CreatUIModelScript() {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<MyDoCreateScriptAsset>(),
        GetSelectedPathOrFallback() + "/NewUIModelScript.cs",
        null,
       "Assets/Wizard_Script/ScriptTemplate/UIModelTemplate.txt");
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


class MyDoCreateScriptAsset : EndNameEditAction {

    public override void Action(int instanceId, string pathName, string resourceFile) {
        UnityEngine.Object o = CreateScriptAssetFromTemplate(pathName, resourceFile);
        ProjectWindowUtil.ShowCreatedAsset(o);
    }

    internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile) {
        string fullPath = Path.GetFullPath(pathName);
        StreamReader streamReader = new StreamReader(resourceFile);
        string text = streamReader.ReadToEnd();
        streamReader.Close();
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);
        text = Regex.Replace(text, "#SCRIPTNAME#", fileNameWithoutExtension);
        text = Regex.Replace(text, "#DateTime#", System.DateTime.Now.ToLongDateString());

        bool encoderShouldEmitUTF8Identifier = true;
        bool throwOnInvalidBytes = false;
        UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
        bool append = false;
        StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
        streamWriter.Write(text);
        streamWriter.Close();
        AssetDatabase.ImportAsset(pathName);
        return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
    }

}