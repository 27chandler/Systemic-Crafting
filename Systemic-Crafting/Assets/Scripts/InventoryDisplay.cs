using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemQuantity;
    private Inventory inv;
    private float amount;
    private string resourceName;

    public Inventory Inv { get { return inv; } set { inv = value; } }

    public void SetName(string name)
    {
        itemName.text = name;
        resourceName = name;
    }

    public string GetName()
    {
        return resourceName;
    }
    public void SetQuantity(float quantity)
    {
        amount = quantity;
        itemQuantity.text = amount.ToString();
    }

    public void AddQuantity(float quantity)
    {
        amount += quantity;
        itemQuantity.text = amount.ToString();
    }
}

