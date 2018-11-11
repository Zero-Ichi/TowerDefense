using System;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private static Transform[] Points;

    // Awake est appelé quand l'instance de script est chargée
    private void Awake()
    {
        Points = new Transform[transform.childCount];

        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] = transform.GetChild(i);
        }
    }

    public static Transform[] GetPoints()
    {
        Transform[] arrayToReturn = new Transform[Points.Length];
        Array.Copy(Points, arrayToReturn, Points.Length);
        return arrayToReturn;
    }

}