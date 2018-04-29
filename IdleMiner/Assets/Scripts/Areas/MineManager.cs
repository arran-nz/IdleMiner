using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour {

    [SerializeField]
    private GameObject minePrefab;

    [SerializeField]
    public float MineSpacing = 1;

    public decimal StartMineCost = 100;

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
    private decimal mineCost;


    private void Awake()
    {
        // Convert Array into List
        Mine[] startingMines = GetComponentsInChildren<Mine>();
        for (int i = 0; i < startingMines.Length; i++)
        {
            Mines.Add(startingMines[i]);
            Mines[i].SetMineIndex(i + 1);
        }
        mineCost = StartMineCost;

        newMineButton = GetComponentInChildren<SpriteButton>();

        UpdateNewMineButton(Mines[MineCount - 1].transform.position);
    }

    public void AddNewMine()
    {
        if (GameController.Instance.CurrentCash >= mineCost)
        {
            GameController.Instance.RemoveCash(mineCost);

            Vector3 spawnPos = new Vector3(
                Mines[0].transform.position.x,
                Mines[MineCount - 1].transform.position.y - MineSpacing
                );

            GameObject clone = Instantiate(
                minePrefab,
                spawnPos,
                new Quaternion(0, 0, 0, 0),
                this.transform);

            Mine newMine = clone.GetComponent<Mine>();
            Mines.Add(newMine);
            newMine.SetMineIndex(MineCount);

            mineCost *= MineCount;

            UpdateNewMineButton(clone.transform.position);
        }
        else
        {
            GameController.Instance.InsufficientFunds();
        }

    }

    private void UpdateNewMineButton(Vector3 minePos)
    {
        newMineButton.transform.position = minePos - new Vector3(0, MineSpacing);
        newMineButton.SetText(
            "New Shaft\n"
            + StringFormatHelper.GetCurrencyString(mineCost));
    }
}
