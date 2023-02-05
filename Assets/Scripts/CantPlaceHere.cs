using UnityEngine;

public class CantPlaceHere : MonoBehaviour
{
    [SerializeField]
    bool currentlyOverlapping = false;

    [SerializeField]
    bool followMouse = false;
    public bool CurrentlyOverlapping { get => currentlyOverlapping; }
    public bool FollowMouse { get => followMouse; set => followMouse = value; }

    public Vector2 MouseFollowOffset;

    // Update is called once per frame
    void Update()
    {
        if (followMouse)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3)MouseFollowOffset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OVERLAPPING");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag != "CantPlaceHere") return;

        CantPlaceHere cantPlaceHere = collision.gameObject.GetComponent<CantPlaceHere>();

        if (cantPlaceHere != null)
        {
            currentlyOverlapping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "CantPlaceHere") return;
        currentlyOverlapping = false;
    }

}
