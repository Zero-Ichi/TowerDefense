using Assets.Scripts.Config;
using UnityEngine;

public class FriendStandard : WalkerBase {

    protected override void OpposeWalkerInRange()
    {
        //Find all GameObject with this tag
        GameObject[] opposeWalkers = GameObject.FindGameObjectsWithTag(Tags.Enemy.ToString());

        //initialisation shortestDistance to inifinit value
        float shortestDistance = float.PositiveInfinity;

        GameObject nerestOpposeWalker = null;

        foreach (GameObject item in opposeWalkers)
        {
            //Get distance between 2 vector3
            float DistanceToOppeseWalker = Vector3.Distance(transform.position, item.transform.position);

            if (DistanceToOppeseWalker < shortestDistance)
            {
                shortestDistance = DistanceToOppeseWalker;
                nerestOpposeWalker = item;
            }
        }
        // If one gameobject find and if is in range
        if (nerestOpposeWalker != null && shortestDistance <= this.range)
        {
            this.OpposeWalkerTarget = nerestOpposeWalker;
            this.IsWalking = false;
        }
        else
        {
            this.IsWalking = true;
        }
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
