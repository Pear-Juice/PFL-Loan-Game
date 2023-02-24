using UnityEngine;

[System.Serializable]
public class Loan
{
    public string name;
    public float amount;
    public float minCreditScore;
    
    public float duration;
    public TimeUnit durationUnit;
    public float interval;
    public TimeUnit intervalUnit;
    public float interestRate;
    public float creditScoreIncrease;

    public float amountToPayOff;
    public float amountPaidOff;

    public Loan(string name, float amount, float duration, TimeUnit durationUnit, float interval, TimeUnit intervalUnit, float interestRate, float creditScore, float creditScoreIncrease)
    {
        this.name = name;
        this.amount = amount;
        this.duration = duration;
        this.durationUnit = durationUnit;
        this.interval = interval;
        this.intervalUnit = intervalUnit;
        this.interestRate = interestRate;
        this.minCreditScore = creditScore;
        this.creditScoreIncrease = creditScoreIncrease;
    }

    public enum TimeUnit
    {
        S,
        M,
    }

    public bool payOff(float amount)
    {
        amountPaidOff += amount;
        
        Debug.Log($"Amount to Pay: {amountToPayOff} Amount Payed Off: {amountPaidOff}");
        
        return (amountPaidOff >= amountToPayOff);
    }
}