using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceLoader : MonoBehaviour
{
    [SerializeField] private List<ResourceBase> loadedResources = new List<ResourceBase>();

    public List<ResourceBase> LoadedResources { get { return loadedResources; } }
    // Start is called before the first frame update
    void Start()
    {
        ResourceBase[] resources = Resources.LoadAll<ResourceBase>("BasicResources");
        loadedResources.AddRange(resources);

        foreach (var resource in loadedResources)
        {
            resource.SetIntialIngredient(resource.Name);
        }
    }
}
