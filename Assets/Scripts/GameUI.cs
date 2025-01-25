using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Image fanImage;
    [SerializeField] Image airStreaksImage;
    public FanScript fanScript;
    [SerializeField] Sprite fanOFF;
    [SerializeField] Sprite fanON1;
    [SerializeField] Sprite fanON2;
    [SerializeField] Sprite airStreaks1;
    [SerializeField] Sprite airStreaks2;

    private bool isSwitchingSprites = false;

    private void Update()
    {
        if (fanScript.playerAlive)
        {
            fanImage.enabled = true;
            fanImage.transform.position = Input.mousePosition;
            Cursor.visible = false;

            if (Input.GetMouseButton(0))
            {
                if (!isSwitchingSprites)
                {
                    StartCoroutine(SwitchSprites());
                }
            }
            else
            {
                StopAllCoroutines();
                isSwitchingSprites = false;
                fanImage.sprite = fanOFF;
                airStreaksImage.enabled = false;
            }

            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 direction = screenCenter - fanImage.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            fanImage.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        else
        {
            fanImage.enabled = false;
            airStreaksImage.enabled = false;
            Cursor.visible = true;
        }
    }

    private IEnumerator SwitchSprites()
    {
        isSwitchingSprites = true;
        airStreaksImage.enabled = true;

        while (true)
        {
            fanImage.sprite = fanON1;
            airStreaksImage.sprite = airStreaks1;
            yield return new WaitForSeconds(0.1f);
            fanImage.sprite = fanON2;
            airStreaksImage.sprite = airStreaks2;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
