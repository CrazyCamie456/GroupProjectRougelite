using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCamera : MonoBehaviour
{
	Transform _camera;

    void Start()
    {
		_camera = Camera.main.transform;
    }

    void Update()
    {
		transform.position = new Vector3(_camera.position.x, _camera.position.y, 0.0f);
    }
}
