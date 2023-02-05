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

    SpriteRenderer hoverImage;

    void Awake()
    {
        playerController = this;
    }

    private void Start()
    {
        hoverImage = GetComponent<SpriteRenderer>();
        shopMenu.SetActive(false);
        button.onClick.AddListener(ToggleShop);
        hoverImage.enabled = false;
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

        if (Input.GetButtonDown("Set"))
        {
            Instantiate(PurchasedEntity, transform.position, Quaternion.identity).transform.localScale = new Vector3(1, 1, 1);

            PurchasedEntity = null;
        }

        if (PurchasedEntity != null)
        {
            hoverImage.enabled = true;
            hoverImage.sprite = PurchasedEntity.towerSprite;

            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else
        {
            hoverImage.enabled = false;
        }
    }

    void ToggleShop()
    {
        shopMenu.SetActive(!shopMenu.activeSelf);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyEntity enemyController = collision.gameObject.GetComponent<EnemyEntity>();
        if (enemyController != null)
        {
            GameHandler.instance.LoseHealth(enemyController.damage);
            Destroy(enemyController.gameObject);
        }
    }
}
