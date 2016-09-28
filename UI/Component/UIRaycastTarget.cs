using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRaycastTarget : MaskableGraphic {

    protected UIRaycastTarget() {
        useLegacyMeshGeneration = false;
    }

    protected override void OnPopulateMesh(VertexHelper vh) {
        vh.Clear();
    }
}
