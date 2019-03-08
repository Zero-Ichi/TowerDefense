using Assets.Scripts.Config;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    [SerializeField]
    protected float range = 15f;
    [SerializeField]
    protected float fireRate = 1f;
    [SerializeField]
    protected float damages = 15f;
    [SerializeField]
    protected float PV = 15f;
    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    protected Transform firePoint;
    [SerializeField]
    protected Transform pivotPoint;

    protected Tags TargetTag { get; set; }

    protected float TotalDamages { get; set; }
    protected float TurnSpeed = 10f;
    protected float FireCountdown = 0;


    protected GameObject Target { get; set; }

    protected Transform[] waypoints;

    protected virtual void Awake()
    {
    }

    // Use this for initialization
    protected virtual void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Target != null)
        {
            Vector3 dir = Target.transform.position - transform.position;

            // Look to target
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(pivotPoint.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;            //Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
            pivotPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    private void UpdateTarget()
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
            this.Target = nerestOpposeWalker;
            if (this.FireCountdown <= 0f)
            {
                this.Shoot();

                this.FireCountdown = 1 / fireRate;
            }
        }
        else
        {
            Target = null;
        }

    }
    protected void Shoot()
    {
        GameObject bulletgo = Instantiate<GameObject>(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletController bullet = bulletgo.GetComponent<BulletController>();
        if (bullet != null)
        {
            bullet.Target = Target;
            bullet.Damages = damages;
        }
    }

}
