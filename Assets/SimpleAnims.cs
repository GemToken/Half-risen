using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnims : MonoBehaviour
{
    public bool Rotate = false;

    public float RotationSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y + RotationSpeed,
            transform.rotation.eulerAngles.z);
    }
}
