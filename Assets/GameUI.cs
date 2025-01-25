using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Image fanImage;
    public FanScript fanScript;

    private void Update()
    {
        //fanImage.transform.position = Input.mousePosition;
        
        if (fanScript.playerAlive == true)
        {
            fanImage.enabled = true;
            fanImage.transform.position = Input.mousePosition;
            Cursor.visible = false;
        }
        else
        {
            fanImage.enabled = false;
            Cursor.visible = true;
        }
        
    }
}
