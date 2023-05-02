using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest: MonoBehaviour
{
    public float jump = 10f;
    public Rigidbody rb;
    public bool onGround = true;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && onGround) {
            rb.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
            onGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Ground") {
            onGround = true;
        }
    }
}
