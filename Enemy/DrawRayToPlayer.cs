using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRayToPlayer : MonoBehaviour {

    private GameObject playerCharacter;
    public Vector3 playerLocation { get; private set; }
    public Vector3 durp { get; private set; }
    public Vector2 diff;
    // Use this for initialization
    void Start () {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }

	// Update is called once per frame
	void Update () {
        DrawRay();
	}

    void DrawRay()
    {
        playerLocation = playerCharacter.transform.position;
        Vector3 point1 = new Vector3(playerLocation.x, playerLocation.y, 0);
        Vector3 point2 = new Vector3(transform.position.x, transform.position.y, 0);
        diff = point1 - point2;
        //RaycastHit2D ray = Physics2D.Raycast(point2, diff);
        Debug.DrawRay(point2, diff, Color.cyan);
        //durp = point2 - diff;
        //return knockBack = (point2 - diff);
    }
}
