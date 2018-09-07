using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCards : MonoBehaviour {

    GameObject selectedCard;

	// Use this for initialization
	void Start () {
        selectedCard = null;
	}
	
	// Update is called once per frame
	void Update () {
        //If the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null)
            {
                //Cards to displace
                if (hit.collider.tag == "Card")
                {
                    selectedCard = hit.collider.gameObject;
                    Debug.Log("Selected");
                }
                //Card positions
                else if(hit.collider.tag == "HoldingCard")
                {

                }
                else if (hit.collider.tag == "PlayingCard")
                {

                }
                else if (hit.collider.tag == "DiscardSpot")
                {
                    if (selectedCard != null)
                    {
                        Debug.Log("Trashed");
                        Destroy(selectedCard);
                        selectedCard = null;
                    }
                    else 
                    {
                        Debug.Log("EmptyHand");
                    }
                }
            }
            else
            {
                selectedCard = null;
            }
        }
    }
}