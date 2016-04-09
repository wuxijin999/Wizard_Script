using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class InfiniteItem : InfiniteRect {

    public static int index = 0;
    public static int preIndex = 0;
    public Text txtContent;

    protected virtual void Start() {

    }

    public virtual void Init() {
        index++;
        txtContent.text = index.ToString();
    }

    public virtual void DoFirstToLast() {
        index++;
        preIndex++;
        txtContent.text = index.ToString();
    }

    public virtual void DoLastToFirst() {
        txtContent.text = preIndex.ToString();
        index--;
        preIndex--;
    }

}
