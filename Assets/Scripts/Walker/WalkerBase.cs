using UnityEngine;

public abstract class WalkerBase : MonoBehaviour
{
    
    [SerializeField]
    public float speed = 10f;
    [SerializeField]
    public float range = 5f;

    [SerializeField]
    protected float PV = 100f;


    protected int waypointIndex = 0;


    protected bool IsWalking { get; set; }

    protected GameObject OpposeWalkerTarget { get; set; }

    protected Transform[] waypoints;

    public Transform WaypointTarget { get; set; }

    // Awake est appelé quand l'instance de script est chargée
    private void Awake()
    {
        IsWalking = true;
    }

    // Start est appelé juste avant qu'une méthode Update soit appelée pour la première fois
    protected virtual void Start()
    {
        InvokeRepeating("OpposeWalkerInRange", 0, 0.075f);
    }

    /// <summary>
    /// For find oppose Walker
    /// </summary>
    protected abstract void OpposeWalkerInRange();

    // Update est appelé pour chaque trame, si le MonoBehaviour est activé
    protected virtual void Update()
    {
        Vector3 dir = WaypointTarget.position - transform.position;
        if (IsWalking)
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }

        if (Vector3.Distance(transform.position, WaypointTarget.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
    }

    /// <summary>
    /// Get the next waypoint & destroy gameobject on the last waypoint
    /// </summary>
    private void GetNextWaypoint()
    {
        if (waypointIndex >= waypoints.Length - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            waypointIndex++;
            WaypointTarget = waypoints[waypointIndex];
        }
    }

}
