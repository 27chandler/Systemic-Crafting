using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceInteraction : MonoBehaviour
{
    [SerializeField] private Tilemap environment;
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private Inventory inv;
    [SerializeField] private CraftingCounter primarySlot;

    [SerializeField] private float miningSpeed;
    private float totalTimer = 1.0f;
    [SerializeField] private float timer;

    private ResourceBase minedResource = null;
    private Vector3Int resourcePosition;

    public float TotalTimer { get { return totalTimer; } }
    public float Timer { get { return timer; } }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MineResource();
        }
        else if (Input.GetMouseButton(1))
        {
            PlaceResource();
        }
    }

    private void PlaceResource()
    {
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        resourcePosition = environment.WorldToCell(mouse_pos);

        if ((environment.GetTile(resourcePosition) == null)
            && (inv.FindQuantity(primarySlot.ResourceName) >= 1.0f))
        {
            TileBase placed_tile = ResourceSearch.SearchResources(primarySlot.ResourceName).Tile;

            inv.AddQuantity(primarySlot.ResourceName, -1.0f);
            environment.SetTile(resourcePosition, placed_tile);
            resourcePosition = new Vector3Int();
        }
    }

    private void MineResource()
    {
        if (minedResource == null)
        {
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            resourcePosition = environment.WorldToCell(mouse_pos);

            minedResource = ResourceSearch.SearchResources(environment.GetTile(resourcePosition));

            if (minedResource != null)
            {
                totalTimer = minedResource.Hardness * miningSpeed;
                timer = totalTimer;
            }
        }
        else
        {
            timer -= Time.deltaTime;

            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int rounded_mouse_pos = new Vector3Int();
            rounded_mouse_pos.x = Mathf.FloorToInt(mouse_pos.x);
            rounded_mouse_pos.y = Mathf.FloorToInt(mouse_pos.y);
            rounded_mouse_pos.z = 0;
            if (rounded_mouse_pos != resourcePosition)
            {
                minedResource = null;
            }

            if (timer <= 0.0f)
            {
                timer = 0.0f;
                inv.AddQuantity(minedResource.Name, 1.0f);
                environment.SetTile(resourcePosition, null);
                minedResource = null;
            }
        }
    }
}
