using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text display;
    [SerializeField] private int minAmount;
    [SerializeField] private int maxAmount;
    private int amount = 0;

    public int Amount { get { return amount; } }

    public void IncreaseAmount(int value)
    {
        amount = Mathf.Clamp(amount + value, minAmount, maxAmount);
        display.text = amount.ToString();

    }

    public void DecreaseAmount(int value)
    {
        amount = Mathf.Clamp(amount - value, minAmount, maxAmount);
        display.text = amount.ToString();
    }
}
