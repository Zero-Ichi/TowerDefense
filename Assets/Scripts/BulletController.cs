using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    protected float speed = 70f;  

    public GameObject Target { get; set; }
    public float Damages { get; set; }

    private void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = Target.transform.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distancePerFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distancePerFrame, Space.World);
    }

    private void HitTarget()
    {
        WalkerBase walker = Target.GetComponent<WalkerBase>();
        if (walker != null)
        {
            walker.AddDamges(Damages);
        }
    }
}
