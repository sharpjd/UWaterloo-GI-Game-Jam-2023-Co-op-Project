using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerController;

    [HideInInspector]
    public TowerEntity PurchasedEntity;

    [SerializeField]
    GameObject shopMenu;
    [SerializeField]
    Button button;
    [SerializeField]
    Image hoverImage;

    void Awake()
    {
        playerController = this;
    }

    private void Start()
    {
        shopMenu.SetActive(false);
        button.onClick.AddListener(ToggleShop);
        hoverImage.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
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

            hoverImage.gameObject.transform.position = Input.mousePosition;
        } else
        {
            hoverImage.gameObject.SetActive(false);
        }
    }

    void ToggleShop()
    {
        shopMenu.SetActive(!shopMenu.activeSelf);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            GameHandler.instance.LoseHealth(enemyController.damage);
            Destroy(enemyController.gameObject);
        }
    }
}
