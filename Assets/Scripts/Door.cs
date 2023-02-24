using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float price;
    public float interactDistance;
    public BoxCollider2D collider;
    public SpriteRenderer spriteRenderer;
    public Sprite doorOpen;
    public Sprite doorClosed;

    public TextMeshProUGUI priceText;

    public bool doorState;
    public bool previouslyOpened;
    
    void Start()
    {
        priceText.text = price + "C";
        InputMan.InteractAction += () =>
        {
            if (Vector3.Distance(transform.position, GameMan.player.transform.position) < interactDistance)
            {
                if (!doorState)
                {
                    if (GameMan.player.Balance >= price || previouslyOpened)
                    {
                        priceText.text = 0 + "C";
                        previouslyOpened = true;
                        openDoor(!previouslyOpened);
                    }
                }
                else
                {
                    closeDoor();
                }
            }
        };
    }

    void openDoor(bool charge)
    {
        doorState = true;
        spriteRenderer.sprite = doorOpen;
        collider.enabled = false;

        if (charge)
            GameMan.player.balance -= price;
    }

    void closeDoor()
    {
        doorState = false;
        spriteRenderer.sprite = doorClosed;
        collider.enabled = true;
    }
}
