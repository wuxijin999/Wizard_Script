using UnityEngine;
using System.Collections;
using System.Text;

public static class StringExtension {

    static StringBuilder strBuilder = new StringBuilder();
    public static string StringBuild(this string str, params object[] objects) {
        strBuilder.Remove(0, strBuilder.Length);
        for (int i = 0; i < objects.Length; i++) {
            strBuilder.Append(objects[i]);
        }

        return strBuilder.ToString();
    }


}
