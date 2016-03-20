using UnityEngine;
using System.Collections;

public class WindowViewBase {

    private WindowInfo info;
    public GameObject winGo;
    Animator animator = null;
    protected WindowBizBase winBiz = null;

    public WindowViewBase() {

    }
    public void Open(WindowBizBase _biz = null) {
        if (winGo == null) {
            LoadResource();
            InitGUI();
        }
        winBiz = _biz;
        OnPreOpen();
        PlayOpenAnim();
    }

    public void Close() {
        winBiz = null;
        OnPreClose();
        PlayCloseAnim();
    }

    public void DoDestroy() {
        winBiz = null;
        if (winGo != null) {
            GameObject.Destroy(winGo);
        }
    }

    protected virtual void InitGUI() {
        info = winGo.GetComponent<WindowInfo>();
        animator = winGo.GetComponent<Animator>();
    }

    protected virtual void OnPreOpen() {

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

    protected virtual void PlayOpenAnim() {
        if (animator != null) {
            animator.Play("Open");
        }
    }

    protected virtual void OnAfterOpen() {

    }

    protected virtual void OnPreClose() {

    }

    protected virtual void PlayCloseAnim() {
        if (animator != null) {
            animator.Play("Close");
        }
    }


    protected virtual void OnAfterClose() {
        info.WinOpenCompleteEvent -= OnOpenComplete;
        info.WinCloseCompleteEvent -= OnCloseComplete;
        winGo.SetActive(false);

    }

    private void OnOpenComplete() {
        OnAfterOpen();
    }

    private void OnCloseComplete() {
        OnAfterClose();
    }



    private void LoadResource() {

        string temp = this.GetType().FullName;
        string prefabName = "Win_" + temp.Substring(0, temp.Length - 3);
        winGo = GameObject.Instantiate(AssetLoadTools.Load_UI(prefabName)) as GameObject;
        winGo.name = temp;

    }
}
