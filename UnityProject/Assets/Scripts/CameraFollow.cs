using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // init some vars editable in unity
    public GameObject objectToFollow;
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        // calculate interpolation
        float interpolation = speed * Time.deltaTime;

        // calculate change in position
        Vector3 position = this.transform.position;
        position.x = Mathf.Lerp(
            this.transform.position.x,
            Mathf.Clamp(objectToFollow.transform.position.x, -21, 21),
            interpolation
        );

        // move the camera
        this.transform.position = position;
    }
}
