using Assets.Scripts.Config;
using System;
using UnityEngine;

public abstract class WalkerBase : MonoBehaviour
{

    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    protected Transform firePoint;

    [SerializeField]
    public float speed = 10f;
    [SerializeField]
    public float range = 5f;
    [SerializeField]
    protected float fireRate = 1f;
    [SerializeField]
    protected float damages = 15f;
    [SerializeField]
    protected float PV = 15f;


    protected float TotalDamages { get; set; }
    protected float TurnSpeed = 10f;
    protected float FireCountdown = 0;
    protected int waypointIndex = 0;


    protected Tags TargetTag { get; set; }

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
        InvokeRepeating("UpdateOpposeWalkerInRange", 0, 0.075f);
    }

    /// <summary>
    /// For find oppose Walker
    /// </summary>
    protected void UpdateOpposeWalkerInRange()
    {
        //Find all GameObject with this tag
        GameObject[] opposeWalkers = GameObject.FindGameObjectsWithTag(TargetTag.ToString());

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
            if (this.FireCountdown <= 0f)
            {
                this.Shoot();

                this.FireCountdown = 1 / fireRate;
            }

            this.IsWalking = false;
        }
        else
        {
            this.IsWalking = true;
        }
    }

    // Update est appelé pour chaque trame, si le MonoBehaviour est activé
    protected virtual void Update()
    {
        Vector3 dir = WaypointTarget.position - transform.position;

        // Look to target
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (IsWalking)
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }

        if (Vector3.Distance(transform.position, WaypointTarget.position) <= 0.3f)
        {
            GetNextWaypoint();
        }


        FireCountdown -= Time.deltaTime;

    }

    protected void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletController bullet = bulletGO.GetComponent<BulletController>();
        if (bullet != null)
        {
            bullet.Target = OpposeWalkerTarget;
            bullet.Damages = damages;
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

    /// <summary>
    /// Add damage
    /// </summary>
    /// <param name="damages"></param>
    public void AddDamges(float damages)
    {
        TotalDamages += damages;
        if (TotalDamages >= PV)
        {
            Destroy(gameObject);
        }
    }

}
