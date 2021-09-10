using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager current;
    private ResourceLoader loader;

    public ResourceLoader Loader { get => loader; set => loader = value; }

    // Start is called before the first frame update
    void Start()
    {
        current = this;

        loader = GetComponent<ResourceLoader>();
        ResourceSearch.LoadedResources = loader.LoadedResources;
        ResourceCreator.Loader = loader;
    }
}
