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


    Vector2 mouseFollowOffset;

    public GameObject RangeIndicatorToInstantiate;

    GameObject rangeIndicator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shop"))
        {
            ToggleShop();
        }

        if (Input.GetButtonDown("Set") && PurchasedEntity != null)
        {
            CantPlaceHere cantPlaceHere = cantPlaceHereHitbox.GetComponent<CantPlaceHere>();

            if (!cantPlaceHere.CurrentlyOverlapping)
            {
                Instantiate(PurchasedEntity, (Vector2)transform.position, Quaternion.identity).transform.localScale = new Vector3(1, 1, 1);
                Destroy(cantPlaceHereHitbox);
                Destroy(rangeIndicator);
                cantPlaceHereHitbox = null;
                PurchasedEntity = null;
            }
        }

        if (PurchasedEntity != null)
        {

            //this only instantiates and renders the can't place here effect
            if (cantPlaceHereHitbox == null)
            {
                cantPlaceHereHitbox = Instantiate(PurchasedEntity.GetComponentInChildren<CantPlaceHere>().gameObject);
                mouseFollowOffset = cantPlaceHereHitbox.transform.position;

                rangeIndicator = Instantiate(RangeIndicatorToInstantiate);
                rangeIndicator.AddComponent<FollowMouse>();
                float range = PurchasedEntity.GetComponent<TowerEntity>().Range;
                rangeIndicator.transform.localScale = new Vector3(2*range, 2*range);
                rangeIndicator.SetActive(true);
            }
            CantPlaceHere cantPlaceHere = cantPlaceHereHitbox.GetComponentInChildren<CantPlaceHere>();
            cantPlaceHere.MouseFollowOffset = mouseFollowOffset;
            cantPlaceHere.FollowMouse = true;

            if (cantPlaceHere.CurrentlyOverlapping)
            {
                hoverImage.color = Color.red;
            }
            else
            {
                hoverImage.color = Color.white;
            }

            hoverImage.enabled = true;
            hoverImage.sprite = PurchasedEntity.towerSprite;

            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
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
