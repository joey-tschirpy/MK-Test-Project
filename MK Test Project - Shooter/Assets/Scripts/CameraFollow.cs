using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = player.transform.position + offset;
	}
}
