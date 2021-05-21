using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    //depending on source image size <<i.e Photoshop  Edit-->ImageSize>> you need to adjuts the center of the mouse.
    //public float centerX;
    //public float centerY;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public bool autoCenterHotSpot = false;
    public Vector2 hotSpotCustom = Vector2.zero;
    private Vector2 hotSpotAuto;

    void Start()
    {



        Vector2 hotSpot;
        if (autoCenterHotSpot)
        {
            hotSpotAuto = new Vector2(cursorTexture.width * 0.5f, cursorTexture.height * 0.5f);
            hotSpot = hotSpotAuto;

        }
        else { hotSpot = hotSpotCustom; }

        //Cursor.SetCursor (cursorTexture, new Vector2( centerX,centerY) ,CursorMode.ForceSoftware);
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);

    }
}
