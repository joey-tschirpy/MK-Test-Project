using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    [SerializeField] GameObject killableObject;
    [SerializeField] Image healthBar;

    private Status status;

	// Use this for initialization
	void Start () {
        status = killableObject.GetComponent<Status>();
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.fillAmount = Mathf.Clamp(status.Health() / status.Data().HealthMax(), 0f, 1f);
	}
}
