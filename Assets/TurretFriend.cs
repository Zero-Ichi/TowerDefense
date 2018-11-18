using Assets.Scripts.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFriend : TurretBase
{
    protected override void Awake()
    {
        base.Awake();
        this.TargetTag = Tags.WalkerEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
