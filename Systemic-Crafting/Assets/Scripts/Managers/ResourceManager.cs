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
    [SerializeField] private ResourceBase test;

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
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int grid_position = environment.WorldToCell(mouse_pos);

            ResourceBase found_resource = SearchResources(environment.GetTile(grid_position));
            text.text = found_resource.Name;

            CreateNewResource(test, found_resource);
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

    void CreateNewResource(ResourceBase primary_resource, ResourceBase secondary_resource)
    {
        ResourceBase resource = (ResourceBase)ScriptableObject.CreateInstance(typeof(ResourceBase));

        Sprite primary_sprite = primary_resource.Tile.sprite;
        Sprite secondary_sprite = secondary_resource.Tile.sprite;

        // Sprite creation
        Sprite resource_sprite = Sprite.Create(TextureMerge.MergeTextures(primary_sprite.texture, primary_sprite.rect, secondary_sprite.texture, secondary_sprite.rect,0.5f), new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 32.0f);
        Tile new_tile = ScriptableObject.CreateInstance<Tile>();
        new_tile.sprite = resource_sprite;

        resource.Tile = new_tile;
        resource.Name = "Test";




        loadedResources.Add(resource);
        Debug.Log(loadedResources[loadedResources.Count-1].Name);
    }
}
