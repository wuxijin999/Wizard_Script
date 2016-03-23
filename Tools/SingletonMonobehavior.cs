using UnityEngine;
using System.Collections;

public class SingletonMonobehavior<T> : MonoBehaviour where T : Component {

    private static T instance = null;

    public static T Instance {
        get {
            return instance;
        }
    }

    protected virtual void Awake() {

        if (instance == null) {
            instance = this as T;
        }
        else {
            Destroy(instance);
            instance = this as T;
        }

    }
}
