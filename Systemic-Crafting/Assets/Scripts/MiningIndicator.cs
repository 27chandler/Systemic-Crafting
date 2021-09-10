using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class MiningIndicator : MonoBehaviour
{
    [SerializeField] private Tilemap interactionTilemap;
    [SerializeField] private ResourceInteraction resourceInteractor;

    [SerializeField] private Image image;
    [Space]
    [SerializeField] private bool isUIElement;
    [SerializeField] private Color defaultColour;
    [SerializeField] private Color lowColour;
    [SerializeField] private float highThreshold;
    [SerializeField] private Color highColour;
    [SerializeField] private ResourceBase hoverResource;
    [SerializeField] private Vector3Int roundedPos;


    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (isUIElement)
        {
            position = Camera.main.ScreenToWorldPoint(position);
        }

        roundedPos.x = Mathf.FloorToInt(position.x);
        roundedPos.y = Mathf.FloorToInt(position.y);
        roundedPos.z = 0;


        hoverResource = ResourceSearch.GetResourceAtPos(interactionTilemap, roundedPos);

        if (hoverResource != null)
        {
            if (hoverResource.Hardness >= highThreshold)
            {
                image.color = highColour;
            }
            else
            {
                image.color = lowColour;
            }
        }
        else
        {
            image.color = defaultColour;
        }

        ScaleIndicator();
    }

    private void ScaleIndicator()
    {
        float scale = Mathf.Clamp(resourceInteractor.Timer / resourceInteractor.TotalTimer,0.0f,1.0f);

        image.transform.localScale = new Vector3(scale, scale, scale);
    }
}
