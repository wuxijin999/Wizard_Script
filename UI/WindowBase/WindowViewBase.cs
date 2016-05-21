using UnityEngine;
using System.Collections;

namespace UI {
    public class WindowViewBase {

        private WindowInfo info;
        private GameObject winGo;
        Animator animator = null;

        public WindowViewBase () {

        }
        public void Open () {
            if (winGo == null) {
                LoadResource();
                BindController();
            }
            OnPreOpen();
            PlayOpenAnim();
        }
        public void Close () {
            OnPreClose();
            PlayCloseAnim();
        }
        public void DoDestroy () {
            if (winGo != null) {
                GameObject.Destroy(winGo);
            }
        }
        protected virtual void BindController () {
            info = winGo.GetComponent<WindowInfo>();
            animator = winGo.GetComponent<Animator>();
        }
        protected virtual void OnPreOpen () {

            if (!winGo.activeInHierarchy) {
                winGo.SetActive(true);
            }

            switch (info.Type) {
                case WindowType.Normal:
                    winGo.transform.SetParent(UITools.NORMALLAYER);
                    UITools.MatchingParent(UITools.NORMALLAYER as RectTransform, winGo.transform as RectTransform);
                    break;
                case WindowType.Modal:
                    winGo.transform.SetParent(UITools.MODALLAYER);
                    UITools.MatchingParent(UITools.MODALLAYER as RectTransform, winGo.transform as RectTransform);
                    break;
                case WindowType.Tip:
                    winGo.transform.SetParent(UITools.TIPSLAYER);
                    UITools.MatchingParent(UITools.TIPSLAYER as RectTransform, winGo.transform as RectTransform);
                    break;
                case WindowType.System:
                    winGo.transform.SetParent(UITools.SYSTEMLAYER);
                    UITools.MatchingParent(UITools.SYSTEMLAYER as RectTransform, winGo.transform as RectTransform);
                    break;
            }

            info.WinOpenCompleteEvent += OnOpenComplete;
            info.WinCloseCompleteEvent += OnCloseComplete;

        }
        protected virtual void OnAfterOpen () {

        }
        protected virtual void OnPreClose () {

        }
        protected virtual void OnAfterClose () {
            info.WinOpenCompleteEvent -= OnOpenComplete;
            info.WinCloseCompleteEvent -= OnCloseComplete;
            winGo.SetActive(false);

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
            winGo = GameObject.Instantiate(AssetLoadTools.Load_UI(prefabName)) as GameObject;
            winGo.name = temp;
        }
    }

}

