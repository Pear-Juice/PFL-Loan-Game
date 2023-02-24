using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    public UIDocument doc;
    public VisualTreeAsset loanAsset;
    
    public VisualElement bank;
    public ListView loanList;

    public Label amountDetail;
    public Label amountDetailUnit;
    public Label durationDetail;
    public Label durationDetailUnit;
    public Label intervalDetail;
    public Label intervalDetailUnit;
    public Label interestRateDetail;
    public Label creditScoreDetail;

    public Label balanceValue;
    public Label creditScoreValue;
    public Label debtHUDValue;
    
    public Loan selectedLoan;

    public Button buyButton;


    private float balance;
    private float creditScore;
    private float debt;

    public float Balance
    {
        get => balance;
        set
        {
            balance = value;
            balanceValue.text = value.ToString();
        }
    }

    public float CreditScore
    {
        get => creditScore;
        set
        {
            creditScore = value;
            creditScoreValue.text = value.ToString();
        }
    }

    public float Debt
    {
        get => debt;
        set
        {
            debt = value;
            debtHUDValue.text = value.ToString();
        }
    }


    private void Awake()
    {
        GameMan.doc = doc;
        GameMan.hud = this;
    }

    private void Start()
    {

        doc.rootVisualElement.Q<Button>("ExitBank").clickable.clicked += CloseBankHud;
        bank = doc.rootVisualElement.Q<VisualElement>("Bank");
        loanList = doc.rootVisualElement.Q<ListView>("LoanList");

        amountDetail = doc.rootVisualElement.Q<Label>("AmountValue");
        amountDetailUnit = doc.rootVisualElement.Q<Label>("AmountUnit");
        durationDetail = doc.rootVisualElement.Q<Label>("DurationValue");
        durationDetailUnit = doc.rootVisualElement.Q<Label>("DurationUnit");
        intervalDetail = doc.rootVisualElement.Q<Label>("IntervalValue");
        intervalDetailUnit = doc.rootVisualElement.Q<Label>("IntervalUnit");
        interestRateDetail = doc.rootVisualElement.Q<Label>("InterestRateValue");
        creditScoreDetail = doc.rootVisualElement.Q<Label>("CreditScoreValue");

        buyButton = doc.rootVisualElement.Q<Button>("Buy");
        buyButton.clickable.clicked += () =>
        {
            GameMan.bank.buyLoan(selectedLoan);
        };

        balanceValue = doc.rootVisualElement.Q<Label>("BalanceHUDValue");
        creditScoreValue = doc.rootVisualElement.Q<Label>("CreditScoreHUDValue");
        debtHUDValue = doc.rootVisualElement.Q<Label>("DebtHUDValue");
    }

    public void OpenBankHUD()
    {
        bank.style.visibility = Visibility.Visible;
        bank.style.display = DisplayStyle.Flex;
        
        InputMan.blockMove = true;
        InputMan.blockJump = true;
        InputMan.blockInteract = true;
    }

    public void CloseBankHud()
    {
        bank.style.visibility = Visibility.Hidden;
        bank.style.display = DisplayStyle.None;
        
        InputMan.blockMove = false;
        InputMan.blockJump = false;
        InputMan.blockInteract = false;
        print("Exit");
    }

    public void initLoanList(List<Loan> items)
    {
        loanList.itemsSource = items;
        Action<object> loanListOnonSelectionChange = delegate(object obj)
        {
            print($"num items {items.Count} selected item {loanList.selectedIndex}");
            selectLoan(items[loanList.selectedIndex]);
        };
        
        loanList.onSelectionChange -= loanListOnonSelectionChange;
        
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            e.Q<Label>("Name").text = items[i].name;
            e.Q<Label>("Amount").text = items[i].amount.ToString();
        };

        Func<VisualElement> makeItem = () => loanAsset.Instantiate();
        
        loanList.bindItem = bindItem;
        loanList.makeItem = makeItem;
        loanList.fixedItemHeight = 60;

        loanList.onSelectionChange += loanListOnonSelectionChange;
    }

    public void selectLoan(Loan loan)
    {
        amountDetail.text = loan.amount.ToString();
        durationDetail.text = loan.duration.ToString();
        durationDetailUnit.text = loan.durationUnit.ToString();
        intervalDetail.text = loan.interval.ToString();
        intervalDetailUnit.text = loan.intervalUnit.ToString();
        interestRateDetail.text = (loan.interestRate * 100).ToString();
        creditScoreDetail.text = loan.minCreditScore.ToString();

        if (loan.minCreditScore >= GameMan.player.CreditScore)
        {
            buyButton.SetEnabled(false);
        }
        else
        {
            buyButton.SetEnabled(true);
        }

        selectedLoan = loan;
    }
}
