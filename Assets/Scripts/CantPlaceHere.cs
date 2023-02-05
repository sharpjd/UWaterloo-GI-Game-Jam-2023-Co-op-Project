using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantPlaceHere : MonoBehaviour
{
    [SerializeField]
    bool currentlyOverlapping = false;

    [SerializeField]
    bool allowOverlap = false;
    public bool AllowOverlap { get => allowOverlap; set => allowOverlap = value; }
    public bool CurrentlyOverlapping { get => currentlyOverlapping; }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        CantPlaceHere cantPlaceHere = collision.gameObject.GetComponent<CantPlaceHere>();

        if(cantPlaceHere != null)
        {
            currentlyOverlapping= true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentlyOverlapping = false;
    }

}
