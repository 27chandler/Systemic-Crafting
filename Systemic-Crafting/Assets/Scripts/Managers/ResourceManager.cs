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

    // Start is called before the first frame update
    void Start()
    {
        ResourceBase[] resources = Resources.LoadAll<ResourceBase>("BasicResources");
        loadedResources.AddRange(resources);

        foreach (var resource in loadedResources)
        {
            resource.SetIntialIngredient(resource.Name);
            Debug.Log(resource);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check cell type
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int grid_position = environment.WorldToCell(mouse_pos);

            primaryCrafting = SearchResources(environment.GetTile(grid_position));
            text.text = primaryCrafting.Name;

            primaryCrafting.DisplayComposition();
        }

        // Debug Craft
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int grid_position = environment.WorldToCell(mouse_pos);

            secondaryCrafting = SearchResources(environment.GetTile(grid_position));
            text.text = secondaryCrafting.Name;
        }
        if (Input.GetMouseButtonDown(2))
        {
            ResourceBase new_resource = CreateNewResource(primaryCrafting, secondaryCrafting);

            // Place new resource at mouse position
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int grid_position = environment.WorldToCell(mouse_pos);
            environment.SetTile(grid_position, new_resource.Tile);
        }
    }

    private ResourceBase SearchResources(TileBase search_tile)
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

    ResourceBase CreateNewResource(ResourceBase primary_resource, ResourceBase secondary_resource)
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


        resource.PrimaryIngredient = primary_resource;
        resource.SecondaryIngredient = secondary_resource;

        resource.SetIngredients(primary_resource.GetIngredients());
        resource.MergeIngredients(secondary_resource.GetIngredients());

        if (CheckExists(resource.CheckCode, out result))
        {
            Debug.Log("Check code already exists: " + resource.CheckCode);
            return result;
        }
        else
        {
            Debug.Log("New Check Code: " + resource.CheckCode);
        }

        loadedResources.Add(resource);

        Debug.Log("------------------------------");
        Debug.Log("Name: " + resource.Name);
        Debug.Log("Hardness: " + resource.Hardness);
        Debug.Log("------------------------------");

        return resource;
    }

    private bool CheckExists(ResourceBase primary_resource, ResourceBase secondary_resource, out ResourceBase output)
    {
        foreach (var resource in loadedResources)
        {
            if (((primary_resource == resource.PrimaryIngredient)
                && (secondary_resource == resource.SecondaryIngredient))
                ||
                ((secondary_resource == resource.PrimaryIngredient)
                && (primary_resource == resource.SecondaryIngredient)))
            {
                output = resource;
                return true;
            }
        }
        output = null;
        return false;
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
