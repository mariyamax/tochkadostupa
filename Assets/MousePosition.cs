using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
   public float speed = 10;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X") * Time.deltaTime * speed;
            if (x>10)
            {
                x = 10;
            }
            float y = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;
            if (y > 10) {
                y = 10;
            }
            Vector3 mouse = new Vector3(x, 0, y);
            transform.Translate(mouse * speed);
        }
    }
}
