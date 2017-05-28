using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System;

public class JsonFormatting:MonoBehaviour {

    string inputString = @"'aaa':111,'bbb':222,[{'ccc':333,'ddd':444}]";

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)) {
            string inputString = "{\"aaa\":111,\"bbb\":222,[{\"ccc\":333,\"ddd\":444,},]}";
            var output = Formatting(inputString);
            File.WriteAllText(@"C:\Users\Administrator\Desktop\" + "Json.txt",output,Encoding.UTF8);
        }
    }

    public string Formatting(string _input) {
        var sb = new StringBuilder();

        var level = 0;
        var set = _input.ToCharArray();
        for(int i = 0;i < set.Length;i++) {
            var c = set[i];
            switch(c) {
                case '[':
                    level++;
                    sb.Append(RepeatingChar('\t',level));
                    sb.Append(c);
                    sb.Append('\n');
                    break;
                case '{':
                    level++;
                    sb.Append(RepeatingChar('\t',level));
                    sb.Append(c);
                    sb.Append('\n');
                    sb.Append(RepeatingChar('\t',level));
                    break;
                case ',':
                    sb.Append(c);
                    sb.Append('\n');
                    sb.Append(RepeatingChar('\t',level));
                    break;
                case ']':
                    level--;
                    sb.Append(RepeatingChar('\t',level));
                    sb.Append(c);
                    sb.Append('\n');
                    break;
                case '}':
                    level--;
                    sb.Append(RepeatingChar('\t',level));
                    sb.Append(c);
                    sb.Append('\n');
                    break;
                default:
                    sb.Append(c);
                    break;
            }
        }

        return sb.ToString();
    }

    private string RepeatingChar(char _c,int _count) {
        var sb = new StringBuilder();
        for(int i = 0;i < _count;i++) {
            sb.Append(_c);
        }

        return sb.ToString();
    }


}
