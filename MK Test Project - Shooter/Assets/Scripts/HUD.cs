using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    [SerializeField] PlayerData playerData;
    [SerializeField] Image healthBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.fillAmount = Mathf.Clamp(playerData.Health() / playerData.HealthMax(), 0f, 1f);
	}
}
