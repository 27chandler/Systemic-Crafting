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

            text.text = environment.GetTile(grid_position).name;

            CreateNewResource();
        }
    }

    void CreateNewResource()
    {
        ResourceBase resource = (ResourceBase)ScriptableObject.CreateInstance(typeof(ResourceBase));
    }
}
