using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    // Singleton Instance
    public static GameController Instance = null;

    [SerializeField]
    public MineManager MineManager;

    [SerializeField]
    public UpgradePanel UpgradePanel;

    [SerializeField]
    public Elevator Elevator;

    [SerializeField]
    private Text cashDisplay;

    private const decimal STARTCASH = 20m;

    public decimal CurrentCash { get; private set; }

    public void Start()
    {
        AddCash(STARTCASH);
    }

    private void Awake()
    {
        SingletonSetup();
    }

    public void AddCash(decimal value)
    {
        CurrentCash += value;
        cashDisplay.text = StringFormatHelper.GetCurrencyString(CurrentCash);
    }

    public void RemoveCash(decimal value)
    {
        if ((CurrentCash - value) >= 0)
        {
            CurrentCash -= value;
            cashDisplay.text = StringFormatHelper.GetCurrencyString(CurrentCash);
        }
        else
        {
            Debug.LogError("Game Controller: Attempting to remove more cash than available!");
        }
    }

    public void InsufficientFunds()
    {
        Debug.Log("No Money, Shame");
    }

    private void SingletonSetup()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
