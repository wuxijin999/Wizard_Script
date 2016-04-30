using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class WindowMgr : Singleton<WindowMgr> {

        List<WindowViewBase> activedWin = new List<WindowViewBase>();
        List<WindowViewBase> unActivedWin = new List<WindowViewBase>();

        public void Init() {
            UITools.CreatUIRoot();
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Open<T>() where T : WindowViewBase, new() {
            T win = new T();

            if (CheckOpen<T>()) {
                return null;
            }

            for (int i = 0; i < unActivedWin.Count; i++) {
                if (unActivedWin[i] is T) {
                    win = unActivedWin[i] as T;
                    unActivedWin.Remove(win);
                    activedWin.Add(win);
                    win.Open();
                    return win;
                }
            }

            win.Open();
            activedWin.Add(win);

            return win;
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_biz">窗口依赖的逻辑</param>
        /// <returns></returns>
        public T Open<T>(WindowBizBase _biz) where T : WindowViewBase, new() {

            T win = new T();

            if (CheckOpen<T>()) {
                return null;
            }

            for (int i = 0; i < unActivedWin.Count; i++) {
                if (unActivedWin[i] is T) {
                    win = unActivedWin[i] as T;
                    win.Open(_biz);
                    unActivedWin.Remove(win);
                    activedWin.Add(win);
                    break;
                }
            }

            win.Open(_biz);
            activedWin.Add(win);

            return win;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Close<T>() where T : WindowViewBase {

            T win = null;

            for (int i = 0; i < activedWin.Count; i++) {
                if (activedWin[i] is T) {
                    win = activedWin[i] as T;
                    break;
                }
            }

            if (win == null) {
                Debug.Log(string.Format("该窗口已经关闭！"));
                return;
            }

            activedWin.Remove(win);
            unActivedWin.Add(win);
            win.Close();

        }

        /// <summary>
        /// 销毁窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DestroyWin<T>() where T : WindowViewBase {
            T win = null;

            for (int i = 0; i < activedWin.Count; i++) {
                if (activedWin[i] is T) {
                    win = activedWin[i] as T;
                    break;
                }
            }

            if (win == null) {
                for (int i = 0; i < unActivedWin.Count; i++) {
                    if (unActivedWin[i] is T) {
                        win = unActivedWin[i] as T;
                        break;
                    }
                }
            }

            if (win == null) {
                Debug.Log(string.Format("这个窗口并没有创建！"));
                return;
            }

            win.DoDestroy();
        }

        private bool CheckOpen<T>() where T : WindowViewBase {

            for (int i = 0; i < activedWin.Count; i++) {
                if (activedWin[i] is T) {
                    Debug.Log(string.Format("<color=yellow>{0}</color>已经打开！", activedWin[i].GetType().FullName));
                    return true;
                }
            }

            return false;
        }
    }

}
