using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerController;

    public TowerEntity PurchasedEntity;

    [SerializeField]
    GameObject shopMenu;
    Button button;
    Image hoverImage;

    void Awake()
    {
        playerController = this;
    }

    private void Start()
    {
        shopMenu.SetActive(false);
        button.onClick.AddListener(ToggleShop);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shop"))
        {
            ToggleShop();
        }

        if (PurchasedEntity != null)
        {
            hoverImage.gameObject.SetActive(true);
            hoverImage.sprite = PurchasedEntity.towerSprite;
        } else
        {
            hoverImage.gameObject.SetActive(false);
        }
    }

    void ToggleShop()
    {
        shopMenu.SetActive(!shopMenu.activeSelf);
    }
}
