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

    public ResourceBase SearchResources(TileBase search_tile)
    {
        foreach (var resource in loadedResources)
        {
            if (resource.Tile == search_tile)
            {
                return resource;
            }
        }
        return null;
    }

    public ResourceBase SearchResources(string name)
    {
        foreach (var resource in loadedResources)
        {
            if (resource.Name == name)
            {
                return resource;
            }
        }
        return null;
    }

    public Sprite GrabResourceSprite(string name)
    {
        foreach (var resource in loadedResources)
        {
            if (resource.Name == name)
            {
                Sprite primary_sprite = resource.Tile.sprite;
                return primary_sprite;
            }
        }
        return null;
    }

    public bool CheckResourceExists(string name)
    {
        foreach (var resource in loadedResources)
        {
            if (resource.Name == name)
            {
                return true;
            }
        }
        return false;
    }
}
