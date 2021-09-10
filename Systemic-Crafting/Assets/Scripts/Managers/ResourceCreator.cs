using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class ResourceCreator
{
    private static ResourceLoader loader;

    public static ResourceLoader Loader { get => loader; set => loader = value; }

    public static ResourceBase CreateNewResource(ResourceBase primary_resource, ResourceBase secondary_resource)
    {
        ResourceBase result;
        // Checks if the new resource already exists within the list of resources available
        //if (CheckExists(primary_resource,secondary_resource, out result))
        //{
        //    return result;
        //}

        ResourceBase resource = (ResourceBase)ScriptableObject.CreateInstance(typeof(ResourceBase));

        Sprite primary_sprite = primary_resource.Tile.sprite;
        Sprite secondary_sprite = secondary_resource.Tile.sprite;

        // Sprite creation
        Sprite resource_sprite = TextureMerge.MergeSprites(primary_sprite, secondary_sprite);
        Tile new_tile = ScriptableObject.CreateInstance<Tile>();
        new_tile.sprite = resource_sprite;

        resource.Tile = new_tile;
        resource.Name = CalculateName(primary_resource.Name, secondary_resource.Name);
        resource.Hardness = CalculateHardness(primary_resource.Hardness, secondary_resource.Hardness);
        resource.Flammability = CalculateFlammability(primary_resource.Flammability, secondary_resource.Flammability);
        resource.Durability = CalculateDurability(primary_resource.Durability, secondary_resource.Durability);
        resource.Conductivity = CalculateConductivity(primary_resource.Conductivity, secondary_resource.Conductivity);

        resource.SetIngredients(primary_resource.GetIngredients());
        resource.MergeIngredients(secondary_resource.GetIngredients());

        if (CheckExists(resource.CheckCode, out result))
        {
            return result;
        }
        else
        {
            Debug.Log("New Check Code: " + resource.CheckCode);
        }

        loader.LoadedResources.Add(resource);

        return resource;
    }

    private static bool CheckExists(float check_code, out ResourceBase output)
    {
        foreach (var resource in loader.LoadedResources)
        {
            if (check_code == resource.CheckCode)
            {
                output = resource;
                return true;
            }
        }
        output = null;
        return false;
    }

    public static string CalculateName(string primary, string secondary)
    {
        return primary + secondary;
    }

    // TODO: Calculation functions should go in their own class
    public static float CalculateHardness(float primary, float secondary)
    {
        return primary + secondary;
    }

    public static float CalculateFlammability(float primary, float secondary)
    {
        return (primary + secondary) / 2.0f;
    }

    public static float CalculateDurability(float primary, float secondary)
    {
        return primary + secondary + 1;
    }

    public static float CalculateConductivity(float primary, float secondary)
    {
        return (primary + secondary) / 2.0f;
    }
}
