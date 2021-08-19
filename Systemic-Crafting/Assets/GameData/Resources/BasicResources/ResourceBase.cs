using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
[CreateAssetMenu(fileName = "Resource", menuName = "Resource/Create new resource")]
public class ResourceBase : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Tile tile;

    [Space]

    // Resource properties
    [SerializeField] private float hardness;
    [SerializeField] private float flammability;
    [SerializeField] private float durability;
    [SerializeField] private float conductivity;

    private Dictionary<string, float> composition = new Dictionary<string, float>();
    private float compositionCheckCode;

    //
    public string Name { get { return name; }
        set
        { name = value;
            base.name = value;
        } }
    public Tile Tile { get { return tile; } set { tile = value; } }
    public float Hardness { get { return hardness; } set { hardness = value; } }
    public float Flammability { get { return flammability; } set { flammability = value; } }
    public float Durability { get { return durability; } set { durability = value; } }
    public float Conductivity { get { return conductivity; } set { conductivity = value; } }
    public float CheckCode { get { return compositionCheckCode; } }

    public void SetIntialIngredient(string name)
    {
        composition.Clear();
        composition.Add(name, 1.0f);
        GenerateCheckCode();
    }

    public void SetIngredients(Dictionary<string,float> ingredients)
    {
        composition.Clear();
        foreach (var ingredient in ingredients)
        {
            composition.Add(ingredient.Key, ingredient.Value);
        }
        GenerateCheckCode();
    }

    public Dictionary<string,float> GetIngredients()
    {
        return composition;
    }

    public void MergeIngredients(Dictionary<string, float> ingredients)
    {
        Dictionary<string, float> working_composition = new Dictionary<string, float>();

        foreach (var ingredient in composition)
        {
            working_composition.Add(ingredient.Key, ingredient.Value * 0.5f);
        }

        foreach (var ingredient in ingredients)
        {
            if (working_composition.ContainsKey(ingredient.Key))
            {
                working_composition[ingredient.Key] += (ingredient.Value * 0.5f);
            }
            else
            {
                working_composition.Add(ingredient.Key,ingredient.Value * 0.5f);
            }
        }

        composition.Clear();

        foreach (var ingredient in working_composition)
        {
            composition.Add(ingredient.Key, ingredient.Value);
        }
        GenerateCheckCode();
    }

    private void GenerateCheckCode()
    {
        compositionCheckCode = 1.0f;

        foreach (var ingredient in composition)
        {
            compositionCheckCode *= (ingredient.Value + 100.0f) * GenerateStringHash(ingredient.Key);
        }
    }

    // For debug purposes, prints composition into console
    public void DisplayComposition()
    {
        foreach (var ingredient in composition)
        {
            Debug.Log(ingredient.Key + ":" + ingredient.Value);
        }
    }

    public float GenerateStringHash(string text)
    {
        float hash = 0.0f;
        for (int i = 0; i < text.Length; i++)
        {
            hash += ((float)text[i] * (i + 1.1f));
        }
        return hash;
    }
}
