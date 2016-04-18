using UnityEngine;
using System.Collections;

public class HeroActor : OrganicActor {

    HandShankResponser handShankResponser;

    protected override void Awake() {
        base.Awake();
    }

    protected override void Start() {
        base.Start();
        handShankResponser = new HandShankResponser(HandShank.Instance);
        handShankResponser.iMove = this;
        handShankResponser.speed = 5f;

    }

}
