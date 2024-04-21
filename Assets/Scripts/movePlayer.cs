using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{

    public float speed = 5.0f;
    public GameObject character;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKey(KeyCode.W)){
			transform.position += Vector3.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A)){
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S)){
			transform.position += Vector3.back * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D)){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
	}
}
