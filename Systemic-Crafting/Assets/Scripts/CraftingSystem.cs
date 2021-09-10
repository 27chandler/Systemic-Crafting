using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    [SerializeField] private CraftingCounter craftingSlotA;
    [SerializeField] private CraftingCounter craftingSlotB;

    public void CraftResource()
    {
        if ((Inventory.current.FindQuantity(craftingSlotA.ResourceName) < 1.0f)
        || (Inventory.current.FindQuantity(craftingSlotB.ResourceName) < 1.0f))
        {
            return;
        }

        ResourceBase primary_resource = ResourceSearch.SearchResources(craftingSlotA.ResourceName);
        ResourceBase secondary_resource = ResourceSearch.SearchResources(craftingSlotB.ResourceName);

        ResourceBase result_resource = ResourceCreator.CreateNewResource(primary_resource, secondary_resource);

        inventory.AddQuantity(craftingSlotA.ResourceName, -1.0f,true);
        inventory.AddQuantity(craftingSlotB.ResourceName, -1.0f,true);

        inventory.AddQuantity(result_resource.Name,1.0f,true);
    }
}
