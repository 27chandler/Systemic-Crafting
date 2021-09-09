using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceSearch : MonoBehaviour
{
    public static ResourceBase SearchResources(List<ResourceBase> resources, TileBase search_tile)
    {
        foreach (var resource in resources)
        {
            if (resource.Tile == search_tile)
            {
                return resource;
            }
        }
        return null;
    }

    public static ResourceBase SearchResources(List<ResourceBase> resources, string name)
    {
        foreach (var resource in resources)
        {
            if (resource.Name == name)
            {
                return resource;
            }
        }
        return null;
    }

    public static Sprite GrabResourceSprite(List<ResourceBase> resources, string name)
    {
        foreach (var resource in resources)
        {
            if (resource.Name == name)
            {
                Sprite primary_sprite = resource.Tile.sprite;
                return primary_sprite;
            }
        }
        return null;
    }

    public static bool CheckResourceExists(List<ResourceBase> resources, string name)
    {
        foreach (var resource in resources)
        {
            if (resource.Name == name)
            {
                return true;
            }
        }
        return false;
    }
}
