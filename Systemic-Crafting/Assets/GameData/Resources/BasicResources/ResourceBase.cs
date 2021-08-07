using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Resource/Create new resource")]
public class ResourceBase : ScriptableObject
{
    [SerializeField] private string name;

    // Resource properties
    [SerializeField] private float hardness;
    [SerializeField] private float flammability;
    [SerializeField] private float durability;
    [SerializeField] private float conductivity;

    //
    public string Name { get { return name; } }
    public float Hardness { get { return hardness; } }
    public float Flammability { get { return flammability; } }
    public float Durability { get { return durability; } }
    public float Conductivity { get { return conductivity; } }
}
