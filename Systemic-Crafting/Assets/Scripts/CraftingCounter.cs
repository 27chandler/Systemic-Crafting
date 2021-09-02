using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text display;
    [SerializeField] private Image sprite;
    [SerializeField] private TMP_Text resourceNameDisplay;
    [SerializeField] private int minAmount;
    [SerializeField] private int maxAmount;
    [SerializeField] private string resourceName;
    private int amount = 0;

    public int Amount { get { return amount; } }
    public int Min { set { minAmount = value; } }
    public int Max { set { maxAmount = value; } }
    public string ResourceName {
        get { return resourceName; }
        set 
        { 
            resourceNameDisplay.text = resourceName = value;
            sprite.sprite = ResourceManager.current.GrabResourceSprite(resourceName);
        } }

    public void IncreaseAmount(int value)
    {
        maxAmount = (int)Inventory.current.FindQuantity(resourceName);

        amount = Mathf.Clamp(amount + value, minAmount, maxAmount);
        display.text = amount.ToString();
    }

    public void DecreaseAmount(int value)
    {
        amount = Mathf.Clamp(amount - value, minAmount, maxAmount);
        display.text = amount.ToString();
    }
}
