using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemQuantity;
    private float amount;

    public void SetName(string name)
    {
        itemName.text = name;
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

