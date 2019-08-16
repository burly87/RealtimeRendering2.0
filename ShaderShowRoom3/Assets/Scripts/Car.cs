using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Forward"))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetButton("Backward"))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        if (Input.GetButton("Left"))
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetButton("Right"))
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }
}
