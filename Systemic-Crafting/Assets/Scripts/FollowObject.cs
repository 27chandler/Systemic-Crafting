using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform objToFollow;
    [SerializeField] private Vector3 followOffset = new Vector3();

    void Update()
    {
        transform.position = (objToFollow.position + followOffset);
    }
}
