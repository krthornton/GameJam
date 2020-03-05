using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // init some vars editable in unity
    public GameObject objectToFollow;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // calculate interpolation
        float interpolation = speed * Time.deltaTime;

        // calculate change in position
        Vector3 position = this.transform.position;
        position.x = Mathf.Lerp(
            this.transform.position.x,
            objectToFollow.transform.position.x,
            interpolation
        );

        // move the camera
        this.transform.position = position;
    }
}
