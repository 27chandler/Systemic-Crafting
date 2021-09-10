using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class ResourceSearch
{
    private static List<ResourceBase> loadedResources;

    public static List<ResourceBase> LoadedResources { get => loadedResources; set => loadedResources = value; }

    public static ResourceBase SearchResources(TileBase search_tile)
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

    public static ResourceBase SearchResources(string name)
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

    public static Sprite GrabResourceSprite(string name)
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

    public static bool CheckResourceExists(string name)
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

    public static ResourceBase GetResourceAtPos(Tilemap tilemap, Vector3Int position)
    {
        return ResourceSearch.SearchResources(tilemap.GetTile(position));
    }
}
