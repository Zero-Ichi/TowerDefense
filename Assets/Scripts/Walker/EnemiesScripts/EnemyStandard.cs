using Assets.Scripts.Config;
using System;
using UnityEngine;

public class EnemyStandard : WalkerBase
{
    private void Awake()
    {
        this.TargetTag = Tags.Friend;
    }
    // Start est appelé juste avant qu'une méthode Update soit appelée pour la première fois
    protected override void Start()
    {
        base.Start();

        this.waypoints = Waypoints.GetPoints();
        Array.Reverse(waypoints);
        this.WaypointTarget = waypoints[waypointIndex];

    }
    // Update est appelé pour chaque trame, si le MonoBehaviour est activé
    protected override void Update()
    {
        base.Update();
    }

    // Implémenter OnDrawGizmos si vous voulez dessiner des gizmos qui peuvent également être choisis et sont toujours dessinés
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
