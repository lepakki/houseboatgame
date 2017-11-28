using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Vector3 playerPosition;
    Camera camera;
    bool zoomedIn;
    bool inTransition;
    float lerpTime;
    [SerializeField]
    float transitionSpeed;

	// Use this for initialization
	void Start () {
        transitionSpeed = 0.4f;
        inTransition = false;
        zoomedIn = false;
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        camera = Camera.main;//FindObjectOfType<Camera>();// GetComponent<Camera>();
        camera.transform.position = new Vector3(playerPosition.x, playerPosition.y +1.5f, -2.5f);
	}

    
    private IEnumerator resizeCamera(float oldSize, float newSize, float time)
    {
            inTransition = true;
            float elapsed = 0;
            while (elapsed <= time)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / time);

                camera.orthographicSize = Mathf.Lerp(oldSize, newSize, t);
                yield return null;
            }
        inTransition = false;
    }

    // Update is called once per frame
    void Update () {
       
        if (Input.GetKey(KeyCode.T))
        {
            if (!inTransition)
            {
                if (!zoomedIn)
                {
                    StartCoroutine(resizeCamera(camera.orthographicSize, 15, transitionSpeed));
                    zoomedIn = !zoomedIn;
                } else
                {
                    StartCoroutine(resizeCamera(camera.orthographicSize, 10, transitionSpeed));
                    zoomedIn = !zoomedIn;
                }
            }
        }
    }
}
