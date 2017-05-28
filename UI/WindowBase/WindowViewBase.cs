using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

namespace UI {
    public class WindowViewBase {

        public enum WindowState {
            Closed,
            Opening,
            Opened,
            Closing,
        }

        public WindowState windowState { get; private set; }
        public int windowId { get; private set; }
        protected GameObject panel;
        protected WindowInfo info;
        Action openCallBack = null;
        Action closeCallBack = null;
        Animator animator = null;
        GameObject mask = null;

        public WindowViewBase () {

        }

        public WindowViewBase Open (int _id) {
            windowId = _id;
            if (panel == null) {
                LoadResource();
                BindController();
                AddListeners();
            }

            windowState = WindowState.Closed;
            OnPreOpen();
            panel.SetActive(true);
            PlayOpenAnim();

            return this;
        }

        public WindowViewBase Close () {
            OnPreClose();
            PlayCloseAnim();

            return this;
        }

        public WindowViewBase Close (bool _playAnimation) {
            OnPreClose();
            if (_playAnimation) {
                PlayCloseAnim();
            }
            else {
                OnCloseComplete();
            }

            return this;
        }

        public void DoDestroy () {
            if (panel != null) {
                GameObject.Destroy(panel);
            }
        }

        public WindowViewBase OnOpenComplete (Action _callBack) {
            openCallBack = _callBack;
            return this;
        }

        public WindowViewBase OnCloseComplete (Action _callBack) {
            closeCallBack = _callBack;
            return this;
        }


        protected virtual void BindController () {
            info = panel.AddMissComponent<WindowInfo>();
            info.IsRaycastValid = false;

            animator = panel.GetComponent<Animator>();
        }

        protected virtual void AddListeners () {

        }

        protected virtual void OnPreOpen () {

            switch (info.Type) {
                case WindowType.Normal:
                    panel.transform.SetParent(WindowManager.Instance.uiRoot.normalCanvas.transform);
                    TransformExtension.MatchWhith(panel.transform as RectTransform, WindowManager.Instance.uiRoot.normalCanvas.transform as RectTransform);
                    break;
                case WindowType.Modal:
                    panel.transform.SetParent(WindowManager.Instance.uiRoot.modalCanvas.transform);
                    TransformExtension.MatchWhith(panel.transform as RectTransform, WindowManager.Instance.uiRoot.modalCanvas.transform as RectTransform);
                    break;
                case WindowType.Tip:
                    panel.transform.SetParent(WindowManager.Instance.uiRoot.tipsCanvas.transform);
                    TransformExtension.MatchWhith(panel.transform as RectTransform, WindowManager.Instance.uiRoot.tipsCanvas.transform as RectTransform);
                    break;
                case WindowType.System:
                    panel.transform.SetParent(WindowManager.Instance.uiRoot.systemCanvas.transform);
                    TransformExtension.MatchWhith(panel.transform as RectTransform, WindowManager.Instance.uiRoot.systemCanvas.transform as RectTransform);
                    break;
            }

            info.WinOpenCompleteEvent += OnOpenComplete;
            info.WinCloseCompleteEvent += OnCloseComplete;

        }
        protected virtual void OnAfterOpen () {
            if (openCallBack != null) {
                openCallBack();
                openCallBack = null;
            }

            info.IsRaycastValid = true;
        }
        protected virtual void OnPreClose () {
            info.IsRaycastValid = false;
        }
        protected virtual void OnAfterClose () {
            if (closeCallBack != null) {
                closeCallBack();
                closeCallBack = null;
            }

            info.WinOpenCompleteEvent -= OnOpenComplete;
            info.WinCloseCompleteEvent -= OnCloseComplete;
            panel.SetActive(false);
        }
        private void PlayOpenAnim () {
            windowState = WindowState.Opening;
            switch (info.AnimType) {
                case WinAnimType.OffSet:
                    switch (info.OffsetStyle) {
                        case WinOffsetAnimStyle.L2R:
                            panel.transform.localPosition = panel.transform.localPosition.SetX(-1920);
                            panel.transform.DOLocalMoveX(0, 0.3f).SetEase(Ease.OutExpo).OnComplete(OnOpenComplete);
                            break;
                        case WinOffsetAnimStyle.R2L:
                            panel.transform.localPosition = panel.transform.localPosition.SetX(1920);
                            panel.transform.DOLocalMoveX(0, 0.3f).SetEase(Ease.OutExpo).OnComplete(OnOpenComplete);
                            break;
                        case WinOffsetAnimStyle.T2B:
                            panel.transform.localPosition = panel.transform.localPosition.SetY(1080);
                            panel.transform.DOLocalMoveY(0, 0.3f).SetEase(Ease.OutExpo).OnComplete(OnOpenComplete);
                            break;
                        case WinOffsetAnimStyle.B2T:
                            panel.transform.localPosition = panel.transform.localPosition.SetY(-1080);
                            panel.transform.DOLocalMoveY(0, 0.3f).SetEase(Ease.OutExpo).OnComplete(OnOpenComplete);
                            break;
                    }
                    break;
                case WinAnimType.Scale:
                    panel.transform.localPosition = Vector3.zero;
                    panel.transform.DOScale(1, 0.3f).SetEase(Ease.OutExpo).OnComplete(OnOpenComplete);
                    break;
                case WinAnimType.None:
                    OnOpenComplete();
                    break;
            }
        }

        private void PlayCloseAnim () {
            windowState = WindowState.Closing;
            switch (info.AnimType) {
                case WinAnimType.OffSet:
                    switch (info.OffsetStyle) {
                        case WinOffsetAnimStyle.L2R:
                            panel.transform.DOLocalMoveX(-1920, 0.3f).SetEase(Ease.InExpo).OnComplete(OnCloseComplete);
                            break;
                        case WinOffsetAnimStyle.R2L:
                            panel.transform.DOLocalMoveX(1920, 0.3f).SetEase(Ease.InExpo).OnComplete(OnCloseComplete);
                            break;
                        case WinOffsetAnimStyle.T2B:
                            panel.transform.DOLocalMoveY(1080, 0.3f).SetEase(Ease.InExpo).OnComplete(OnCloseComplete);
                            break;
                        case WinOffsetAnimStyle.B2T:
                            panel.transform.DOLocalMoveY(-1080, 0.3f).SetEase(Ease.InExpo).OnComplete(OnCloseComplete);
                            break;
                    }
                    break;
                case WinAnimType.Scale:
                    panel.transform.DOScale(0, 0.3f).SetEase(Ease.InExpo).OnComplete(OnCloseComplete);
                    break;
                case WinAnimType.None:
                    OnCloseComplete();
                    break;
            }
        }

        private void OnOpenComplete () {
            OnAfterOpen();
            windowState = WindowState.Opened;
        }

        private void OnCloseComplete () {
            OnAfterClose();
            windowState = WindowState.Closed;
        }

        private void LoadResource () {
            string temp = this.GetType().Name;
            string prefabName = StringUtil.StringBuild("Win_", temp.Substring(0, temp.Length - 3));
            panel = GameObject.Instantiate(AssetLoad.LoadUI(prefabName)) as GameObject;
            panel.name = temp;
        }
    }

}

