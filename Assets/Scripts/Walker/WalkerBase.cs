using Assets.Scripts.Enums;
using System;
using UnityEngine;

public abstract class WalkerBase : MonoBehaviour
{

    [SerializeField]
    public float speed = 10f;
    [SerializeField]
    public WalkerType type;

    [SerializeField]
    protected float PV = 100f;

    protected int waypointIndex = 0;
    protected Transform[] waypoints;

    public Transform Target { get; set; }

    // Start est appelé juste avant qu'une méthode Update soit appelée pour la première fois
    protected virtual void Start()
    {
        waypoints = Waypoints.Points;
        if (type == WalkerType.Friend)
        {
            Array.Reverse(waypoints);
        }
        Target = waypoints[waypointIndex];
    }

    // Update est appelé pour chaque trame, si le MonoBehaviour est activé
    protected virtual void Update()
    {
        Vector3 dir = Target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, Target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= waypoints.Length - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            waypointIndex++;
            Target = waypoints[waypointIndex];
        }
    }
}
