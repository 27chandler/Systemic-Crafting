using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCraftingSlot : MonoBehaviour
{
    [SerializeField] private CraftingCounter craftingSlot;
    [SerializeField] private string resourceName;

    public CraftingCounter CraftingSlot { set { craftingSlot = value; } }
    public string ResourceName { get { return resourceName; } set { resourceName = value; } }

    public void SetResource()
    {
        craftingSlot.ResourceName = resourceName;
    }
}
