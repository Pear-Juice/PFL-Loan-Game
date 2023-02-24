using System;
using System.Collections;
using System.Collections.Generic;
using Movement_Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public JumpController jumpController;
    public Rigidbody2D rb;
    public ParticleSystem ps;
    public SpriteRenderer sr;
    public Animator animator;

    public float balance;
    public float creditScore;
    public float debt;

    public float Balance
    {
        get => balance;
        set
        {
            balance = value;
            GameMan.hud.Balance = value;
        }
    }

    public float CreditScore
    {
        get => creditScore;
        set
        {
            creditScore = value;
            GameMan.hud.CreditScore = value;
        }
    }

    public float Debt
    {
        get => debt;
        set
        {
            debt = value;
            GameMan.hud.Debt = value;
        }
    }

    private void Awake()
    {
        GameMan.player = this;
    }

    private void Start()
    {

        jumpController.OnJumpAction += () =>
        {
            animator.SetTrigger("Jump");
        };

        Balance = balance;
        CreditScore = creditScore;
    }

    private void Update()
    {
        
        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        if (rb.velocity.x < 0)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Spike"))
        {
            die();
        }
    }

    private void die()
    {
        ps.Play();
        sr.enabled = false;
        rb.simulated = false;

        StartCoroutine(respawn());
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("SampleScene");
    }
}
