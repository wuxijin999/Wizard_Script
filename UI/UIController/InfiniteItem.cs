using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfiniteItem : MonoBehaviour {

    public static int index = 0;

    public Text txtContent;

    public InfiniteScrollRect scrollRect;

    public virtual void DoFirstToLast() {
        index++;
        txtContent.text = index.ToString();
    }

    public virtual void DoLastToFirst() {
        index--;
        txtContent.text = index.ToString();
    }

}
