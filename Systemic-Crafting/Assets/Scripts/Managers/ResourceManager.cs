using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private List<ResourceBase> loadedResources = new List<ResourceBase>();
    // Start is called before the first frame update
    void Start()
    {
        ResourceBase[] resources = Resources.LoadAll<ResourceBase>("BasicResources");
        loadedResources.AddRange(resources);

        foreach (var resource in loadedResources)
        {
            Debug.Log(resource);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
