using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Image fanImage;

    private void Update()
    {
        fanImage.transform.position = Input.mousePosition;
    }
}
