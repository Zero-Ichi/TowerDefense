using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    [SerializeField]
    public float speed = 10f;
    [SerializeField]
    protected float PV = 100f;

    protected int waypointIndex = 0;

    public Transform Target { get; set; }

    // Start est appelé juste avant qu'une méthode Update soit appelée pour la première fois
    protected virtual void Start()
    {
        Target = Waypoints.Points[waypointIndex];
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
        waypointIndex++;
        Target = Waypoints.Points[waypointIndex];
    }
}
