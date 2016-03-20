using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

    protected int instanceId;
    public int InstanceId {
        get {
            return instanceId;
        }
    }

    protected int level;
    public int Level {
        get {
            return level;
        }
    }

    protected virtual void Awake() {
        instanceId = BattleManager.Instance.AllocateActorInstanceId();
    }

    protected virtual void OnEnable() {
    }

    protected virtual void Start() {
    }

    protected virtual void FixedUpdate() {

    }
    protected virtual void Update() {
    }

    protected virtual void LateUpdate() {

    }

    protected virtual void OnDisable() {

    }

    protected virtual void OnDestroy() {

    }

}
