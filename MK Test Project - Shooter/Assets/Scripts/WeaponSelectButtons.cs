using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectButtons : MonoBehaviour {
    [SerializeField] List<Button> buttons;

    public void EnableButtons()
    {
        foreach(Button button in buttons)
        {
            button.interactable = true;
        }
    }
}
