//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Sunday, May 15, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

public class DebugFilter {

    [MenuItem("Tools/Debug/Clear", false, 5)]
    public static void DeleteAllDebug () {
        string fullPath = Path.GetFullPath("Assets/Wizard_Script/Test/modeltest.txt");
        StreamReader streamReader = new StreamReader("Assets/Wizard_Script/Test/modeltest.txt");
        string text = streamReader.ReadToEnd();
        streamReader.Close();

        string pattern = @"Debug.log(.*);";
        text = Regex.Replace(text, pattern, "");

        bool encoderShouldEmitUTF8Identifier = true;
        bool throwOnInvalidBytes = false;
        UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
        bool append = false;
        StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
        streamWriter.Write(text);
        streamWriter.Close();
        AssetDatabase.ImportAsset("Assets/Wizard_Script/Test/modeltest.txt");
    }

}



