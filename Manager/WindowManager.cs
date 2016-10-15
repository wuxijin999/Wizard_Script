using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class WindowManager : Singleton<WindowManager> {

        public UIRoot uiRoot {
            get; set;
        }

        Dictionary<int, WindowViewBase> windowDict = new Dictionary<int, WindowViewBase>();

        public override void Init () {

        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Open<T> (int _id) where T : WindowViewBase, new() {

            WindowViewBase win = null;
            if (TryGetWindow(_id, out win)) {
                if (win.windowState == WindowViewBase.WindowState.Closed) {
                    CloseOtherNormalWindow(_id);
                    win.Open(_id);
                }
                else {
                    WDebug.Log(string.Format("Id为:{0}的窗口已经打开！", _id));
                }
            }
            else {
                WDebug.Log(string.Format("Id为:{0}的窗口无法获得！", _id));
            }

            return (T)win;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public WindowViewBase Close (int _id) {

            WindowViewBase win = null;
            if (TryGetWindow(_id, out win)) {
                if (win.windowState == WindowViewBase.WindowState.Opened) {
                    win.Close();
                }
                else {
                    WDebug.Log(string.Format("Id为:{0}的窗口已经关闭！", _id));
                }
            }
            else {
                WDebug.Log(string.Format("Id为:{0}的窗口无法获得！", _id));
            }

            return win;
        }

        /// <summary>
        /// 销毁窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DestroyWin (int _id) {
            WindowViewBase win = null;
            if (TryGetWindow(_id, out win)) {
                windowDict.Remove(_id);
                win.DoDestroy();
            }
            else {
                WDebug.Log(string.Format("Id为:{0}的窗口无法获得！", _id));
            }
        }

        private bool CheckOpen (int _id) {
            WindowViewBase win = null;
            if (TryGetWindow(_id, out win)) {
                return win.windowState == WindowViewBase.WindowState.Opened;
            }
            else {
                return false;
            }
        }

        private void CloseOtherNormalWindow (int _id) {
            foreach (int key in windowDict.Keys) {
                if (key != _id) {
                    Close(key);
                }
            }
        }

        private bool TryGetWindow<T> (int _id, out T _win) where T : WindowViewBase, new() {

            bool get = false;
            WindowViewBase windowBase = null;
            if (!windowDict.TryGetValue(_id, out windowBase)) {
                RefWindowConfig config = null;
                if (RefWindowConfig.TryGet(_id, out config)) {
                    _win = new T();
                    windowDict[_id] = _win;
                    get = true;
                }
                else {
                    _win = null;
                    get = false;
                }
            }
            else {
                _win = (T)windowBase;
                get = true;
            }

            return get;
        }


    }


}
