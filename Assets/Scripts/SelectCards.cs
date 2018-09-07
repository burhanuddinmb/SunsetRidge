using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCards : MonoBehaviour {

    GameObject selectedCard;
    GameObject selectedArea;
    bool cardMoving;
    bool destroyCardInEnd;
    

    // Use this for initialization
    void Start () {
        selectedCard = null;
        cardMoving = false;
        destroyCardInEnd = false;
    }
	
	// Update is called once per frame
	void Update () {
        //If the left mouse button is clicked.
        if (cardMoving)
        {
            selectedCard.transform.position = Vector3.MoveTowards(selectedCard.transform.position, selectedArea.transform.position, 15.0f * Time.deltaTime);
            if (selectedCard.transform.position == selectedArea.transform.position)
            {
                selectedCard.transform.position.Set(selectedCard.transform.position.x, selectedCard.transform.position.y, selectedCard.transform.position.z - 111111.0f);
                if (destroyCardInEnd)
                {
                    destroyCardInEnd = false;
                    Destroy(selectedCard);
                }
                cardMoving = false;
                selectedCard = null;
                selectedArea = null;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

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
                    selectedArea = hit.collider.gameObject;
                    selectedCard.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    selectedCard.transform.parent = selectedArea.transform;
                    cardMoving = true;
                }
                else if (hit.collider.tag == "PlayingCard")
                {
                    if (selectedCard != null)
                    {
                        selectedArea = hit.collider.gameObject;
                        selectedCard.transform.parent = selectedArea.transform;
                        cardMoving = true;
                    }
                }
                else if (hit.collider.tag == "DiscardSpot")
                {
                    if (selectedCard != null)
                    {
                        selectedArea = hit.collider.gameObject;
                        selectedCard.transform.parent = selectedArea.transform;
                        cardMoving = true;
                        destroyCardInEnd = true;
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