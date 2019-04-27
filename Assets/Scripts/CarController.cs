using UnityEngine;

public class CarController : MonoBehaviour {
    public float carAcceleration = 1000;
    public float brakingInit = 50;
    public float steering = 0.2f;
    public float timer = 0f;
    float braking;
    Rigidbody rgbd;

    private void Start() {

        rgbd = GetComponent<Rigidbody>();
        braking = brakingInit;
    }

    void FixedUpdate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool accelerate = Input.GetButton("Accelerate");
        float speedLocal = transform.InverseTransformDirection(rgbd.velocity).x;
        float speedDrift = transform.InverseTransformDirection(rgbd.velocity).y;
        if (speedLocal <= 1){
            timer = timer + Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        if (v < 0)
        {
            braking = braking + Time.deltaTime*brakingInit/braking*40;
            if (timer > 0.5)
            {
                rgbd.AddRelativeForce(-Time.deltaTime * carAcceleration / 2, 0, 0);
            }
        }
        else
        {
            braking = brakingInit;

        }
        Debug.Log(braking);
        if (accelerate) {
            rgbd.AddRelativeForce(Time.deltaTime * carAcceleration, 0, 0);
        }

//        rgbd.velocity = transform.TransformDirection(new Vector3(10, 0, 0));
 //       Debug.Log(rgbd.velocity);
        rgbd.AddRelativeForce(0, -Time.deltaTime * braking *  speedDrift, 0);
        transform.Rotate(0, 0, -h*Time.deltaTime * speedLocal*steering * braking);

    }
}
