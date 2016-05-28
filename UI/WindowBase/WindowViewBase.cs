using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace UI {
    public class WindowViewBase {

        protected GameObject panel;
        protected WindowInfo info;
        Animator animator = null;

        public WindowViewBase () {

        }
        public void Open () {
            if (panel == null) {
                LoadResource();
                BindController();
                AddListeners();
            }

            OnPreOpen();
            PlayOpenAnim();
        }

        public void Close () {
            OnPreClose();
            PlayCloseAnim();
        }
        public void DoDestroy () {
            if (panel != null) {
                GameObject.Destroy(panel);
            }
        }
        protected virtual void BindController () {
            info = panel.GetComponent<WindowInfo>();
            info.IsRaycastValid = false;

            animator = panel.GetComponent<Animator>();
        }

        protected virtual void AddListeners () {

        }

        protected virtual void OnPreOpen () {
            if (!panel.activeInHierarchy) {
                panel.SetActive(true);
            }

            switch (info.Type) {
                case WindowType.Normal:
                    panel.transform.SetParent(UITools.NORMALLAYER);
                    UITools.MatchingParent(UITools.NORMALLAYER as RectTransform, panel.transform as RectTransform);
                    break;
                case WindowType.Modal:
                    panel.transform.SetParent(UITools.MODALLAYER);
                    UITools.MatchingParent(UITools.MODALLAYER as RectTransform, panel.transform as RectTransform);
                    break;
                case WindowType.Tip:
                    panel.transform.SetParent(UITools.TIPSLAYER);
                    UITools.MatchingParent(UITools.TIPSLAYER as RectTransform, panel.transform as RectTransform);
                    break;
                case WindowType.System:
                    panel.transform.SetParent(UITools.SYSTEMLAYER);
                    UITools.MatchingParent(UITools.SYSTEMLAYER as RectTransform, panel.transform as RectTransform);
                    break;
            }

            info.WinOpenCompleteEvent += OnOpenComplete;
            info.WinCloseCompleteEvent += OnCloseComplete;

        }
        protected virtual void OnAfterOpen () {
            info.IsRaycastValid = true;
        }
        protected virtual void OnPreClose () {
            info.IsRaycastValid = false;
        }
        protected virtual void OnAfterClose () {
            info.WinOpenCompleteEvent -= OnOpenComplete;
            info.WinCloseCompleteEvent -= OnCloseComplete;
            panel.SetActive(false);

        }
        private void PlayOpenAnim () {
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
        }
        private void OnCloseComplete () {
            OnAfterClose();
        }
        private void LoadResource () {
            string temp = this.GetType().Name;
            string prefabName = StringUtil.StringBuild("Win_", temp.Substring(0, temp.Length - 3));
            panel = GameObject.Instantiate(AssetLoadTools.Load_UI(prefabName)) as GameObject;
            panel.name = temp;
        }
    }

}

