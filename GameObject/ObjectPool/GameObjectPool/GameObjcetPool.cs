using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjcetPool {

    GameObject prefab = null;
    List<int> instanceIdList = null;
    List<GameObject> unActivedObject;
    List<GameObject> activedObject;

    private int capacity;
    public int Capacity {
        get {
            return capacity;
        }
    }

    public GameObjcetPool(int _capacity, GameObject _prefab) {
        capacity = _capacity;
        if (capacity < 1) {
            Debug.LogWarning("Capacity should not less than one!");
        }
        prefab = _prefab;
        if (prefab == null) {
            Debug.LogWarning("Prefab is null!");
        }
        instanceIdList = new List<int>();
        unActivedObject = new List<GameObject>();
        activedObject = new List<GameObject>();
    }

    /// <summary>
    /// 获取一个gameobject
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameObject() {
        GameObject go;
        if (unActivedObject.Count < 1) {
            go = MonoBehaviour.Instantiate(prefab) as GameObject;
            instanceIdList.Add(go.GetInstanceID());
        }
        else {
            go = unActivedObject[0];
            unActivedObject.RemoveAt(0);
        }

        activedObject.Add(go);
        return go;
    }

    /// <summary>
    /// 回收一个gameobject
    /// </summary>
    /// <param name="_go"></param>
    public void RecycleGameObject(GameObject _go) {
        int instanceId = _go.GetInstanceID();
        if (!instanceIdList.Contains(instanceId)) {
            Debug.Log("The go does not belong to this pool!");
            return;
        }

        activedObject.Remove(_go);
        if (unActivedObject.Count >= capacity) {
            GameObject.Destroy(_go);
            instanceIdList.Remove(instanceId);
        }
        else {
            unActivedObject.Add(_go);
        }
    }

    /// <summary>
    /// 回收所有
    /// </summary>
    public void RecycleAll() {
        while (activedObject.Count > 0) {
            RecycleGameObject(activedObject[0]);
        }
    }

    /// <summary>
    /// 开关所有
    /// </summary>
    /// <param name="_value"></param>
    public void ToggleAll(bool _value) {
        for (int i = 0; i < activedObject.Count; i++) {
            activedObject[i].gameObject.SetActive(_value);
        }
    }


}
