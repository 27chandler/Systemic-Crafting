using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Resource/Create new resource")]
public class ResourceBase : ScriptableObject
{
    [SerializeField] string name;

    // Property values
    [SerializeField] float hardness;
    [SerializeField] float flammability;
    [SerializeField] float durability;
    [SerializeField] float conductivity;
}
