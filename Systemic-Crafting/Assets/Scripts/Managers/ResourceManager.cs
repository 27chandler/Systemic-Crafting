using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Tilemap environment;
    [SerializeField] private List<ResourceBase> loadedResources = new List<ResourceBase>();
    [SerializeField] private ResourceBase primaryCrafting;
    [SerializeField] private ResourceBase secondaryCrafting;

    public static ResourceManager current;

    public List<ResourceBase> LoadedResources { get { return loadedResources; }}

    // Start is called before the first frame update
    void Start()
    {
        current = this;

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

    public ResourceBase GetResourceAtPos(Vector3Int position)
    {
        return SearchResources(environment.GetTile(position));
    }

    public ResourceBase CreateNewResource(ResourceBase primary_resource, ResourceBase secondary_resource)
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
        Sprite resource_sprite = Sprite.Create(TextureMerge.MergeTextures(primary_sprite.texture, primary_sprite.rect, secondary_sprite.texture, secondary_sprite.rect,0.5f), new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 32.0f);
        Tile new_tile = ScriptableObject.CreateInstance<Tile>();
        new_tile.sprite = resource_sprite;

        resource.Tile = new_tile;
        resource.Name = primary_resource.Name + secondary_resource.Name;
        resource.Hardness = CalculateHardness(primary_resource.Hardness, secondary_resource.Hardness);
        resource.Flammability = CalculateFlammability(primary_resource.Flammability, secondary_resource.Flammability);
        resource.Durability = CalculateFlammability(primary_resource.Durability, secondary_resource.Durability);
        resource.Conductivity = CalculateFlammability(primary_resource.Conductivity, secondary_resource.Conductivity);

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

        loadedResources.Add(resource);

        return resource;
    }

    private bool CheckExists(float check_code, out ResourceBase output)
    {
        foreach (var resource in loadedResources)
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

    // TODO: Calculation functions should go in their own class
    float CalculateHardness(float primary, float secondary)
    {
        return primary + secondary;
    }

    float CalculateFlammability(float primary, float secondary)
    {
        return (primary + secondary) / 2.0f;
    }

    float CalculateDurability(float primary, float secondary)
    {
        return primary + secondary + 1;
    }

    float CalculateConductivity(float primary, float secondary)
    {
        return (primary + secondary) / 2.0f;
    }
}
