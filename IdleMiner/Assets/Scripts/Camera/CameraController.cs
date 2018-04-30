using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    #region Configuration

    /// <summary>
    /// Height of the trigger pad at the top and bottom of the screen
    /// </summary>
    public const float TRIGGER_PAD_HEIGHT = 200f;

    #endregion

    private void Update()
    {
        // If mouse button is pressed an UI is not covering the screen
        if (Input.GetMouseButton(0) && !GameController.Instance.UI.IsScreenCovered)
        {
            MoveCamera();
        }
    }

    /// <summary>
    /// Moves the cameras tranform for naviagting the mine shaft
    /// </summary>
    void MoveCamera()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 movement = new Vector3(0, 3f * Time.deltaTime);

        // Move Camera Up
        if (mousePos.y > Screen.height - TRIGGER_PAD_HEIGHT)
        {
            // Do not allow the camera to go any further up
            if (transform.position.y < 0)
            {

                transform.position += movement;
                //transform.Translate(new Vector3(0, moveSpeed));
            }
        }
        // Move Camera Down
        else if (mousePos.y < 0 + TRIGGER_PAD_HEIGHT)
        {
            // Do not allow camera to go beyond the lowest mine
            MineManager manager = GameController.Instance.MineManager;
            float bottomMinePosition = manager.Mines[manager.MineCount - 1].transform.position.y;

            if (transform.position.y > bottomMinePosition)
            {
                transform.position -= movement;
            }
        }



        

       // if (!Input.GetMouseButtonDown(0)) return;

        //Vector3 vector = Camera.main.ScreenToViewportPoint(Input.mousePosition + Vector3.zero);
        //vector = Vector3.Normalize(vector);
        //Debug.Log(vector);


        //Vector3 move = Vector3.zero +  new Vector3(0, vector.y * (dragSpeed * Time.deltaTime));

        //transform.Translate(move, Space.World);

    }
}
