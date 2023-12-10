using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseLook : MonoBehaviour
{

    [SerializeField] private Vector2 sensitivity;

    private Vector2 rotation; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Vector2 GetInput() {

        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
         );
        return input;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 wantedVelocity = GetInput() * sensitivity;

        rotation += wantedVelocity * Time.deltaTime;
        

        if (rotation.y < -90)
        {
            rotation.y = -90;
        }
        else if (rotation.y > 90)
        {
            rotation.y = 90;
        }

        Vector3 lookVector = new Vector3(rotation.y, rotation.x, 0);
        transform.localEulerAngles = lookVector;
    }
}
