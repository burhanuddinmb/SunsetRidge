using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour {

    [SerializeField] GameObject card;
    [SerializeField] int numberOfCards;
    GameObject clone;
    Vector3 deckPosition;
    Vector3 deckOffset;
    float timeSkip;
    bool movingCard;

    // Use this for initialization
    void Start () {
        deckPosition = GameObject.Find("Deck Spot").transform.position;
        deckPosition.z -= 0.01f;
        Debug.Log(deckPosition);
        deckOffset = new Vector3(deckPosition.x - 1.0f, deckPosition.y - 1.0f, 4.0f);
        timeSkip = 0.0f;
        movingCard = false;

    }
	
	// Update is called once per frame
	void Update () {
        timeSkip += Time.deltaTime;
        if (numberOfCards > 0)
        {
            if (movingCard)
            {
                MoveCard();
            }
            else if (timeSkip >= 0.1f)
            {
                timeSkip = 0.0f;
                GenerateCard();
            }
        }
	}

    void GenerateCard()
    {
        clone = Instantiate(card, deckOffset, Quaternion.identity);
        movingCard = true;
    }

    void MoveCard()
    {
        clone.transform.position = Vector3.MoveTowards(clone.transform.position, deckPosition, 20.0f * Time.deltaTime);
        if (clone.transform.position == deckPosition)
        {
            movingCard = false;
            deckPosition.z -= 0.01f;
            numberOfCards -= 1;
        }
    }
}
