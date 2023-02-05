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

    GameObject cantPlaceHereHitbox;

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


    Color originalSpriteColor;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shop"))
        {
            ToggleShop();
        }

        if (Input.GetButtonDown("Set") && PurchasedEntity != null)
        {
            if (!cantPlaceHereHitbox.GetComponent<CantPlaceHere>().CurrentlyOverlapping)
            {
                Instantiate(PurchasedEntity, transform.position, Quaternion.identity).transform.localScale = new Vector3(1, 1, 1);
                Destroy(cantPlaceHereHitbox);
                cantPlaceHereHitbox = null;
                PurchasedEntity = null;
            }
        }

        if (PurchasedEntity != null)
        {

            //this only instantiates and renders the can't place here effect
            if (cantPlaceHereHitbox == null) {
                cantPlaceHereHitbox = Instantiate(PurchasedEntity.GetComponentInChildren<CantPlaceHere>().gameObject);
            }
            CantPlaceHere cantPlaceHere = cantPlaceHereHitbox.GetComponentInChildren<CantPlaceHere>();
            cantPlaceHere.FollowMouse = true;

            if (cantPlaceHere.AllowOverlap == false
                && cantPlaceHere.CurrentlyOverlapping
                )
            {
                PurchasedEntity.SpriteRenderer.color = Color.red;
            }
            else
            {
                PurchasedEntity.SpriteRenderer.color = Color.white;
            }

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
