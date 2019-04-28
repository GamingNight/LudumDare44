using UnityEngine;

public class CarController : MonoBehaviour {
    public float carAcceleration = 1000;
    public float brakingInit = 50;
    public float steering = 0.2f;
    public float timer = 0f;
    public float speedLocal = 0f;
    public float speedDrift = 0f;
    float braking;
    Rigidbody rgbd;
    private ParticleSystem[] pcSystems;
    public AudioFade driftSound;
    public AudioSource hornSound;

    private void Start() {

        rgbd = GetComponent<Rigidbody>();
        pcSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem pcSystem in pcSystems) {
            //pcSystem.Pause(true);
        }

        braking = brakingInit;
    }

    private void Update() {

        if (Input.GetButtonDown("Horn"))
            hornSound.Play();
    }

    void FixedUpdate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool accelerate = Input.GetButton("Accelerate");
        speedLocal = transform.InverseTransformDirection(rgbd.velocity).x;
        speedDrift = transform.InverseTransformDirection(rgbd.velocity).y;
        if (speedLocal <= 1) {
            timer = timer + Time.deltaTime;
        } else {
            timer = 0;
        }
        if (v < 0) {
            braking = braking + Time.deltaTime * brakingInit / braking * 40;
            if (timer > 0.1) {
                rgbd.AddRelativeForce(-Time.deltaTime * carAcceleration / 2, 0, 0);
            } else {
                rgbd.AddRelativeForce(-Time.deltaTime * carAcceleration * braking / brakingInit * speedLocal / 40, 0, 0);
            }
        } else {
            braking = brakingInit;
        }
        if (accelerate) {
            rgbd.AddRelativeForce(Time.deltaTime * carAcceleration, 0, 0);
        }

        //        rgbd.velocity = transform.TransformDirection(new Vector3(10, 0, 0));
        //       Debug.Log(rgbd.velocity);
        rgbd.AddRelativeForce(0, -Time.deltaTime * braking * speedDrift, 0);
        transform.Rotate(0, 0, -h * Time.deltaTime * speedLocal * steering * braking);
        foreach (ParticleSystem pcSystem in pcSystems) {
            if ((Mathf.Abs(speedDrift) > 20 || (timer == 0 && v < 0)) && !pcSystem.isPlaying) {
                pcSystem.Play(true);
                driftSound.PlayWithFadeIn();
            } else if ((Mathf.Abs(speedDrift) < 10 || timer > 0.1) && pcSystem.isPlaying && v >= 0) {
                pcSystem.Pause(true);
                driftSound.StopWithFadeOut();
                //Debug.Log(pcSystem.isPlaying+"    "+speedDrift);
            }
        }
    }
}
