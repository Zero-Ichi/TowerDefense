using Assets.Scripts.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : TurretBase {

    protected override void Awake()
    {
        base.Awake();
        this.TargetTag = Tags.WalkerFriend;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
