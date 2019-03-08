using Assets.Scripts.Config;
using UnityEngine;

public class FriendStandard : WalkerBase
{

    protected override void Awake()
    {
        base.Awake();
        this.TargetTag = Tags.WalkerEnemy;
    }


    // Start est appelé juste avant qu'une méthode Update soit appelée pour la première fois
    protected override void Start()
    {
        base.Start();
        this.waypoints = Waypoints.GetPoints();
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
