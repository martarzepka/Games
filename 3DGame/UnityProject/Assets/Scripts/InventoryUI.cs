using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI diamondText;
    public TextMeshProUGUI winText;
    // Start is called before the first frame update
    void Start()
    {
        winText.gameObject.SetActive(false);
    }

    public void UpdateDiamondText(PlayerInventory playerInventory)
    {
        diamondText.text = playerInventory.numberOfDiamond.ToString() + "/5";
        if (playerInventory.numberOfDiamond == 5)
        {
            winText.gameObject.SetActive(true);
        }
    }
}
