using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public float activationThreshold;

    public List<Loan> startupLoans = new List<Loan>();
    
    public List<Loan> currentLoans = new List<Loan>();

    private void Awake()
    {
        print("Load bank");
    }

    private void Start()
    {
        InputMan.InteractAction += OnInteract;
    }

    public void OnInteract()
    {
        if (Vector2.Distance(GameMan.player.transform.position, transform.position) < activationThreshold)
            enterBank();
    }

    private void enterBank()
    {
        GameMan.bank = this;
        GameMan.hud.OpenBankHUD();
        print($"Interact with bank {startupLoans.Count}");
        
        GameMan.hud.initLoanList(startupLoans);
        GameMan.loans = startupLoans;
    }

    public void buyLoan(Loan loan)
    {
        if (GameMan.player.CreditScore < loan.minCreditScore)
        {
            return;
        }
        
        GameMan.hud.CloseBankHud();
        GameMan.player.Balance += loan.amount;
        GameMan.loans.Remove(loan);
        currentLoans.Add(loan);

        GameMan.player.CreditScore -= loan.creditScoreIncrease;

        Clock clock = new Clock(loan.interval);
        
        float time = 0;

        float amountOwed = loan.amount * (1 + loan.interestRate);
        float numberOfIterations = loan.duration / loan.interval;
        float amountOwnedPerInterval = amountOwed / numberOfIterations;
        
        clock.pass += () =>
        {
            time += loan.interval;
            GameMan.player.Balance -= amountOwnedPerInterval;

            if (time >= loan.duration)
                clock.stop();
        };

        clock.start();

        loan.amountToPayOff = amountOwed;
        GameMan.player.Debt += amountOwed;
        print(amountOwed);
    }

    public void collectCoin(Coin coin)
    {
        if (currentLoans.Count > 0)
        {
            Loan loanToPay = currentLoans[0];
            GameMan.player.CreditScore += (loanToPay.creditScoreIncrease * 2) / (loanToPay.amountToPayOff / coin.value);
            GameMan.player.Debt -= coin.value;
            
            if (loanToPay.payOff(coin.value))
            {
                currentLoans.Remove(loanToPay);
                print("Payed off loan");
            }
        }
    }
}
