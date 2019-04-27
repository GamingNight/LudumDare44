using UnityEngine;

public class CarController : MonoBehaviour {
    public float strength = 1000;
    public float rotateSpeed = 250;
    Rigidbody rgbd;

    private void Start() {

        rgbd = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool accelerate = Input.GetButton("Accelerate");

        if (accelerate) {
            rgbd.AddRelativeForce(Time.deltaTime * strength, h * Time.deltaTime * strength, 0);
        }

        if (Mathf.Abs(rgbd.velocity.x) > 0.01) {
            transform.Rotate(0, 0, -h * Time.deltaTime * rotateSpeed, Space.Self);
        }
    }
}
