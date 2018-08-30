using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour {

    [SerializeField] GameObject card;
    [SerializeField] int numberOfCards;
    int cardsOnDeck;
    List <GameObject> deckCards;
    Vector3 deckPosition;
    Vector3 deckOffset;
    float timeSkip;
    bool movingCard;
    int cardsInHand;
    GameObject handCards;

    // Use this for initialization
    void Start () {
        handCards = GameObject.Find("Holding Cards");
        deckCards = new List<GameObject>();
        cardsInHand = 0;
        deckPosition = GameObject.Find("Deck Spot").transform.position;
        Debug.Log(deckPosition);
        deckOffset = new Vector3(deckPosition.x - 1.0f, deckPosition.y - 1.0f, 4.0f);
        timeSkip = 0.0f;
        movingCard = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (numberOfCards > 0)
        {
            timeSkip += Time.deltaTime;
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
        else if (cardsInHand < 4)//Place the top four cards in hand
        {
            timeSkip += Time.deltaTime;
            deckCards[cardsOnDeck - 1].transform.position = Vector3.MoveTowards(deckCards[cardsOnDeck - 1].transform.position, handCards.transform.GetChild(cardsInHand).position, 15.0f * Time.deltaTime);
            if (deckCards[cardsOnDeck - 1].transform.position == handCards.transform.GetChild(cardsInHand).position)
            {
                cardsInHand += 1;
                cardsOnDeck -= 1;
                movingCard = false;
                deckPosition.z -= 0.01f;
            }
        }
	}

    void GenerateCard()
    {
        deckCards.Add(Instantiate(card, deckOffset, Quaternion.identity));
        movingCard = true;
    }

    void MoveCard()
    {
        deckCards[cardsOnDeck].transform.position = Vector3.MoveTowards(deckCards[cardsOnDeck].transform.position, deckPosition, 25.0f * Time.deltaTime);
        if (deckCards[cardsOnDeck].transform.position == deckPosition)
        {
            cardsOnDeck += 1;
            movingCard = false;
            deckPosition.z -= 0.01f;
            numberOfCards -= 1;
        }
    }
}
