using UnityEditor;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class WindowTestEditorWindow : EditorWindow {

    static WindowTestEditorWindow window;
    public static void OpenWindowTest(WindowTestEditorWindow _window) {
        CloseTest();

        window = _window;

        _window.titleContent = new GUIContent("UI Test");
        _window.position = new Rect(Screen.width * 0.5f - 400, Screen.height * 0.5f - 300, 1200, 600);

        _window.testTool.BindController();
        _window.Show();
    }

    public static void CloseTest() {
        if (window != null) {
            window.Close();
        }
    }


    public  UITest testTool;

    public void OnGUI() {

        GUILayout.Space(40);
        if (testTool.auto) {
            GUILayout.BeginHorizontal();
            GUILayout.Space(400);
            EditorGUILayout.LabelField(string.Format("累计时间  {0} 秒", (int)testTool.autoCumulativeTime), GUILayout.ExpandWidth(true));
            GUILayout.Space(400);
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Space(50);
        testTool.interVal = EditorGUILayout.FloatField("测试间隔", testTool.interVal, GUILayout.ExpandWidth(true));
        GUILayout.Space(50);
        testTool.TouchCount = EditorGUILayout.IntField("多点模拟", testTool.TouchCount, GUILayout.ExpandWidth(true));
        GUILayout.Space(50);
        if (!testTool.auto) {
            if (GUILayout.Button("开始", GUILayout.Width(50))) {
                testTool.StartAutoTest();
            }
        }
        else {
            if (GUILayout.Button("结束", GUILayout.Width(50))) {
                testTool.StopAutoTest();
            }

        }
        GUILayout.Space(50);
        GUILayout.EndHorizontal();
        GUILayout.Space(40);

        foreach (int key in testTool.hashComponent.Keys) {

            Component component = testTool.hashComponent[key];
            if (component == null) {
                continue;
            }
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();

            GUILayout.Space(50);

            GUILayout.Label(component.gameObject.name, GUILayout.Width(150));
            IPointerClickHandler click = null;
            IPointerDownHandler down = null;
            IPointerUpHandler up = null;
            ISubmitHandler submit = null;

            GUILayout.Space(10);
            if (testTool.hashClick.TryGetValue(key, out click)) {
                if (GUILayout.Button("Click")) {
                    click.OnPointerClick(new PointerEventData(EventSystem.current));
                    testTool.clickTestTimes[key]++;
                }

                EditorGUILayout.LabelField(testTool.clickTestTimes[key].ToString(), GUILayout.Width(30));
            }

            GUILayout.Space(10);
            if (testTool.hashDown.TryGetValue(key, out down)) {
                if (GUILayout.Button("Down")) {
                    down.OnPointerDown(new PointerEventData(EventSystem.current));
                    testTool.downTestTimes[key]++;
                }

                EditorGUILayout.LabelField(testTool.downTestTimes[key].ToString(), GUILayout.Width(30));
            }

            GUILayout.Space(10);
            if (testTool.hashUp.TryGetValue(key, out up)) {
                if (GUILayout.Button("Up")) {
                    up.OnPointerUp(new PointerEventData(EventSystem.current));
                    testTool.upTestTimes[key]++;
                }

                EditorGUILayout.LabelField(testTool.upTestTimes[key].ToString(), GUILayout.Width(30));
            }

            GUILayout.Space(10);
            if (testTool.hashSubmit.TryGetValue(key, out submit)) {
                if (GUILayout.Button("Submit")) {
                    submit.OnSubmit(new PointerEventData(EventSystem.current));
                    testTool.submitTestTimes[key]++;
                }

                EditorGUILayout.LabelField(testTool.submitTestTimes[key].ToString(), GUILayout.Width(30));
            }

            GUILayout.EndHorizontal();
        }

        Repaint();
    }
}
