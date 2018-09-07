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
    GameObject deck;

    // Use this for initialization
    void Start () {
        handCards = GameObject.Find("Holding Cards");
        deckCards = new List<GameObject>();
        cardsInHand = 0;
        deck = GameObject.Find("Deck Spot");
        deckPosition = deck.transform.position;
        Debug.Log(deckPosition);
        deckOffset = new Vector3(deckPosition.x - 1.0f, deckPosition.y - 1.0f, -4.0f);
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
                deckCards[cardsOnDeck - 1].transform.parent = handCards.transform.GetChild(cardsInHand).transform;
                deckCards[cardsOnDeck - 1].transform.position.Set(deckCards[cardsOnDeck - 1].transform.position.x, deckCards[cardsOnDeck - 1].transform.position.y, deckCards[cardsOnDeck - 1].transform.position.z - 111111.0f);
                deckCards[cardsOnDeck - 1].transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                cardsInHand += 1;
                cardsOnDeck -= 1;
                movingCard = false;
                deckPosition.z -= 0.01f;
            }
        }
	}

    void GenerateCard()
    {
        deckCards.Add(Instantiate(card, deckOffset, Quaternion.Euler(0.0f, 180.0f, 0.0f), deck.transform));
        movingCard = true;
    }

    void MoveCard()
    {
        deckCards[cardsOnDeck].transform.position = Vector3.MoveTowards(deckCards[cardsOnDeck].transform.position, deckPosition, 25.0f * Time.deltaTime);
        if (deckCards[cardsOnDeck].transform.position == deckPosition)
        {
            deckCards[cardsOnDeck].transform.position.Set(deckCards[cardsOnDeck].transform.position.x, deckCards[cardsOnDeck].transform.position.y, deckCards[cardsOnDeck].transform.position.z - 0.01f);
            cardsOnDeck += 1;
            movingCard = false;
            deckPosition.z -= 0.01f;
            numberOfCards -= 1;
        }
    }
}
