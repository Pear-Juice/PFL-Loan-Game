using System;
using System.Collections;
using System.Collections.Generic;
using ElRaccoone.Tweens;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float value = 1;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            transform.TweenLocalScale(new Vector3(0, 0, 0), .1f).SetOnComplete(() =>
            {
                if (!GameMan.bank) GameMan.player.Balance += value;
                
                if (GameMan.bank && GameMan.bank.currentLoans.Count <= 0)
                    GameMan.player.Balance += value;
                if (GameMan.bank)
                    GameMan.bank.collectCoin(this);
                Destroy(gameObject);
            });
        }
    }
}
