using UnityEngine;
using System.Collections;

public class ThrowableController : MonoBehaviour
{
    //private float speed = 7;
    private Rigidbody2D rb2d;
    private Vector2 direction;
    MouseLook mouseLook;
    private Vector2 aim;
    Stats stats;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        stats = FindObjectOfType<Movement>().GetComponent<Stats>();
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        rb2d.AddForce(dir * stats.power.CurrentVal * 250); //vanha value dir * 800
        StartCoroutine(destroyAfter(1));
    }

    IEnumerator destroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }


}
