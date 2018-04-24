using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    public int Height = 1;
    public int Width = 1;
    public Color DebugColor = Color.yellow;
    public TextMesh MineValueText;

    [SerializeField]
    private Transform BucketTransform;
    [SerializeField]
    private Transform MineTransform;


    public Vector2 BucketPosition
    {
        get
        {
            return BucketTransform.position;
        }
    }
    public Vector2 MinePosition
    {
        get
        {
            return MineTransform.position;
        }
    }

    public float BucketAmount;
    public bool ManangerPresent;

    // Use this for initialization
    void Start () {
        ManangerPresent = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddToBucket(float amountToAdd)
    {
        BucketAmount += amountToAdd;
        UpdateText(BucketAmount);
    }

    private void UpdateText(float newAmount)
    {
        MineValueText.text = newAmount + "k";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = DebugColor;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 1));
    }

}
