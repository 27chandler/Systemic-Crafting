using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
[CreateAssetMenu(fileName = "Resource", menuName = "Resource/Create new resource")]
public class ResourceBase : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Tile tile;

    [Space]

    // Resource properties
    [SerializeField] private float hardness;
    [SerializeField] private float flammability;
    [SerializeField] private float durability;
    [SerializeField] private float conductivity;

    //
    public string Name { get { return name; }
        set
        { name = value;
            base.name = value;
        } }
    public Tile Tile { get { return tile; } set { tile = value; } }
    public float Hardness { get { return hardness; } set { hardness = value; } }
    public float Flammability { get { return flammability; } set { flammability = value; } }
    public float Durability { get { return durability; } set { durability = value; } }
    public float Conductivity { get { return conductivity; } set { conductivity = value; } }
}
