using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    TowerEntity ThingToBuy;

    void Start()
    {
        button.onClick.AddListener(onButtonClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    void onButtonClick()
    {
        // Remove currency
        PlayerController.playerController.PurchasedEntity = ThingToBuy;
    }
}
