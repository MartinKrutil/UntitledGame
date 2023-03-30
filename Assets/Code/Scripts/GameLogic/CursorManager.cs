using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    [SerializeField] private Texture2D cursorTexture;

    private Vector2 cursorHotspot;

    private void Awake()
    {
        if(instance != null && instance != this)
            Destroy(this);

        else
            instance = this;

        cursorHotspot = new Vector2 (cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
}
