using UnityEngine;

public class CarController : MonoBehaviour {
    public float carAcceleration = 1000;
    public float brakingInit = 10;
    public float steering = 10;
    Rigidbody rgbd;

    private void Start() {

        rgbd = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float braking = brakingInit;
        bool accelerate = Input.GetButton("Accelerate");
        float speedLocal = transform.InverseTransformDirection(rgbd.velocity).x;
        float speedDrift = transform.InverseTransformDirection(rgbd.velocity).y;
        if (v < 0)
        {
            braking = braking * 1.2f;
            rgbd.AddRelativeForce(-Time.deltaTime * carAcceleration/2*braking/brakingInit, 0, 0);
        }
        else
        {
            braking = brakingInit;

        }

        if (accelerate) {
            rgbd.AddRelativeForce(Time.deltaTime * carAcceleration, 0, 0);
        }

//        rgbd.velocity = transform.TransformDirection(new Vector3(10, 0, 0));
 //       Debug.Log(rgbd.velocity);
        rgbd.AddRelativeForce(0, -Time.deltaTime * braking *  speedDrift, 0);
        transform.Rotate(0, 0, -h*Time.deltaTime * speedLocal*steering * braking);

    }
}
