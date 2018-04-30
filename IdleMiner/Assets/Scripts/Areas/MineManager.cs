using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages collecting and spawning of mines
/// </summary>
public class MineManager : MonoBehaviour {

    [SerializeField]
    private GameObject minePrefab;
    [HideInInspector]
    public List<Mine> Mines = new List<Mine>();

    public int MineCount
    {
        get
        {
            return Mines.Count;
        }
    }

    private SpriteButton newMineButton;
    private decimal newMineCost;

    #region Configuration

    /// <summary>
    /// World Spacing
    /// </summary>
    public const float MINE_SPACING = 1;
    /// <summary>
    /// Cost of the first Mine
    /// </summary>
    public const decimal MINE_START_COST = 1000;
    /// <summary>
    /// Power of which the mines increase in price the deeper you go.
    /// </summary>
    public const int UPGRADE_COST_POWER = 10;

    #endregion


    private void Awake()
    {
        // Convert Array into List
        Mine[] startingMines = GetComponentsInChildren<Mine>();
        for (int i = 0; i < startingMines.Length; i++)
        {
            Mines.Add(startingMines[i]);
            Mines[i].SetMineIndex(i + 1);
        }
        newMineCost = MINE_START_COST;

        newMineButton = GetComponentInChildren<SpriteButton>();
        UpdateNewMineButton(Mines[MineCount - 1].transform.position);
    }

    /// <summary>
    /// Adds a new mine if there is sufficient balance in cash
    /// </summary>
    public void AddNewMine()
    {
        GameController.Instance.SpendCash(newMineCost,
            () =>
            {
                Vector3 spawnPos = new Vector3(
                Mines[0].transform.position.x,
                Mines[MineCount - 1].transform.position.y - MINE_SPACING
                );

                GameObject clone = Instantiate(
                    minePrefab,
                    spawnPos,
                    new Quaternion(0, 0, 0, 0),
                    this.transform);

                Mine newMine = clone.GetComponent<Mine>();
                Mines.Add(newMine);
                newMine.SetMineIndex(MineCount);

                newMineCost = (decimal)Mathf.Pow(UPGRADE_COST_POWER, MineCount + 2);

                UpdateNewMineButton(clone.transform.position);
            }
            );           

    }

    /// <summary>
    /// Moves the "New Mine" button and updates the text
    /// </summary>
    /// <param name="minePos"></param>
    private void UpdateNewMineButton(Vector3 minePos)
    {
        newMineButton.transform.position = minePos - new Vector3(0, MINE_SPACING);
        newMineButton.SetText(
            "New Shaft\n"
            + StringFormatter.GetCurrencyString(newMineCost));
    }
}
