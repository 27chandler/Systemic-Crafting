using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private Transform inventoryTab;
    [Space]
    [SerializeField] private GameObject inventoryDisplayPrefab;
    [SerializeField] private List<InventorySlot> resources = new List<InventorySlot>();
    [Space]
    [SerializeField] private CraftingCounter craftingSlotA;
    [SerializeField] private CraftingCounter craftingSlotB;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (resources.Count < resourceManager.LoadedResources.Count)
        {
            ReloadInventory();
        }
    }

    private void ReloadInventory()
    {
        //foreach (var resource in resources)
        //{
        //    Destroy(resource.display.gameObject);
        //}

        //resources.Clear();

        foreach (var resource in resourceManager.LoadedResources)
        {
            if (!CheckSlotExists(resource.Name))
            {
                GameObject new_slot_display = GameObject.Instantiate(inventoryDisplayPrefab);
                new_slot_display.transform.parent = inventoryTab;
                InventoryDisplay display = new_slot_display.GetComponentInChildren<InventoryDisplay>();

                InventorySlot new_slot = new InventorySlot();
                new_slot.name = resource.Name;
                new_slot.quantity = 0.0f;
                new_slot.display = display;

                new_slot.display.SetName(new_slot.name);
                new_slot.display.SetQuantity(new_slot.quantity);

                // UI Buttons
                UIButton button = new_slot_display.GetComponentInChildren<UIButton>();

                SetCraftingSlot[] crafting_setters = new_slot_display.GetComponentsInChildren<SetCraftingSlot>();

                crafting_setters[0].ResourceName = new_slot.name;
                crafting_setters[0].CraftingSlot = craftingSlotA;
                crafting_setters[1].ResourceName = new_slot.name;
                crafting_setters[1].CraftingSlot = craftingSlotB;

                resources.Add(new_slot);
            }
        }
    }

    public void AddQuantity(string name, float amount)
    {
        int counter = 0;
        foreach (var resource in resources)
        {
            if (resource.name == name)
            {
                resources[counter].quantity += amount;

                resources[counter].display.SetQuantity(resources[counter].quantity);
            }
            counter++;
        }
    }

    public bool CheckSlotExists(string name)
    {
        foreach (var resource in resources)
        {
            if (resource.name == name)
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class InventorySlot
{
    public string name;
    public float quantity;
    public InventoryDisplay display;
}
