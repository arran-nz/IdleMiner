using UnityEngine;

/// <summary>
/// Contains cash and used to reach sub managers
/// </summary>
public class GameController : MonoBehaviour {

    // Singleton Instance
    public static GameController Instance = null;

    [SerializeField]
    public MineManager MineManager;

    [SerializeField]
    public UserInterfaceManager UI;

    [SerializeField]
    public Elevator Elevator;

    public decimal CurrentCash { get; private set; }

    #region Configuration

    private const decimal START_CASH = 500m;

    #endregion

    public void Start()
    {
        AddCash(START_CASH);
    }

    private void Awake()
    {
        SingletonSetup();
    }

    /// <summary>
    /// Add cash to spendable amount
    /// </summary>
    /// <param name="value"></param>
    public void AddCash(decimal value)
    {
        CurrentCash += value;
        UI.CashDisplay.text = StringFormatter.GetCurrencyString(CurrentCash);
    }

    /// <summary>
    /// Spend cash stored in the Game Controller
    /// </summary>
    /// <param name="cost">Cost of purchase</param>
    /// <param name="purchaseSuccessful">This is called if the purchase is succesful</param>
    public void SpendCash(decimal cost, System.Action purchaseSuccessful)
    {
        if ((CurrentCash - cost) >= 0)
        {
            CurrentCash -= cost;
            UI.CashDisplay.text = StringFormatter.GetCurrencyString(CurrentCash);
            purchaseSuccessful.Invoke();
        }
        else
        {
            InsufficientFunds();
        }
    }

    /// <summary>
    /// Display Insufficient Funds Message
    /// </summary>
    private void InsufficientFunds()
    {
        UI.Notification.PopNotification("Insuffient Funds", NotificationMessage.NotifcationStyle.Alert);
    }

    /// <summary>
    /// Ensures only one instance of GameController.cs
    /// </summary>
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
