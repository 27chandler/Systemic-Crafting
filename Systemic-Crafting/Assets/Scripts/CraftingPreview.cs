using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingPreview : MonoBehaviour
{
    [SerializeField] private CraftingCounter primaryCraftingSlot;
    [SerializeField] private CraftingCounter secondaryCraftingSlot;
    [Space]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text durabilityText;
    [SerializeField] private TMP_Text hardnessText;
    [SerializeField] private TMP_Text flammabilityText;
    [SerializeField] private TMP_Text conductivityText;
    [SerializeField] private Image sprite;

    private string primaryResource;
    private string secondaryResource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string primary_name = primaryCraftingSlot.ResourceName;
        string secondary_name = secondaryCraftingSlot.ResourceName;

        if ((primary_name != primaryResource)
            || (secondary_name != secondaryResource))
        {
            Refresh(primary_name, secondary_name);

            primaryResource = primary_name;
            secondaryResource = secondary_name;
        }
    }


    public void Refresh(string primary, string secondary)
    {
        ResourceBase primary_resource = ResourceSearch.SearchResources(primary);
        ResourceBase secondary_resource = ResourceSearch.SearchResources(secondary);

        if ((primary_resource == null) || (secondary_resource == null))
        {
            Debug.Log("Failed to find resources");
            return;
        }

        sprite.sprite = ResourceManager.current.MergeSprites(primary_resource.Tile.sprite, secondary_resource.Tile.sprite);

        nameText.text = ResourceManager.current.CalculateName(primary_resource.Name, secondary_resource.Name);
        durabilityText.text = ResourceManager.current.CalculateDurability(primary_resource.Durability,secondary_resource.Durability).ToString();
        hardnessText.text = ResourceManager.current.CalculateHardness(primary_resource.Hardness,secondary_resource.Hardness).ToString();
        flammabilityText.text = ResourceManager.current.CalculateFlammability(primary_resource.Flammability,secondary_resource.Flammability).ToString();
        conductivityText.text = ResourceManager.current.CalculateConductivity(primary_resource.Conductivity,secondary_resource.Conductivity).ToString();
    }
}
