using UnityEngine;
using System.Collections;

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
            if (animator != null) {
                animator.Play("Open");
            }
        }
        private void PlayCloseAnim () {
            if (animator != null) {
                animator.Play("Close");
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

