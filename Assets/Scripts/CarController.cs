using UnityEngine;

public class CarController : MonoBehaviour
{
    public float carAcceleration = 1000;
    public int pointsDrift = 5;
    public float brakingInit = 50;
    public float steering = 0.2f;
    public float timer = 0f;
    public float speedLocal = 0f;
    public float speedDrift = 0f;
    float timerDrift = 0;
    public AudioFade driftSound;
    public AudioSource hornSound;

    float braking;
    Rigidbody rgbd;
    private ParticleSystem[] pcSystems;
    private bool fearStatus = false;
    private float fearTimer = 0f;
    
    public bool getFearStatus()
    {
        return fearStatus;
    }

    public void setFearStatus(bool status)
    {
        fearStatus = status;
        if (status)
            fearTimer = GameManager.Instance().getPlayTime();
    }
    
    private float initialPitch;


    private void Start()
    {
        if(GameManager.FINAL_PLAYER_POSITION.x != -1 && GameManager.FINAL_PLAYER_POSITION.y != -1 && GameManager.FINAL_PLAYER_POSITION.z != -1){
            transform.position = GameManager.FINAL_PLAYER_POSITION;
            transform.eulerAngles = GameManager.FINAL_PLAYER_EULER_ANGLES;
        }

        rgbd = GetComponent<Rigidbody>();
        pcSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem pcSystem in pcSystems)
        {
            //pcSystem.Pause(true);
        }

        braking = brakingInit;
        initialPitch = driftSound.source.pitch;
    }

    private void Update() {

        if (Input.GetButtonDown("Horn")) {
            hornSound.Play();
        }
        if ((fearTimer == 0) || (GameManager.Instance().getPlayTime() - fearTimer > 10)) {
            fearTimer = 0;
            fearStatus = false;
        }
    }

    void FixedUpdate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Brake/Reverse");
        if (Input.GetButton("Alt-Brake"))
        {
            v = -1;
        }
        bool accelerate = (Input.GetButton("Accelerate") || Input.GetButton("Alt-Accelerate"));
        speedLocal = transform.InverseTransformDirection(rgbd.velocity).x;
        speedDrift = transform.InverseTransformDirection(rgbd.velocity).y;
        if (speedLocal <= 1)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        if (v < 0)
        {
            braking = braking + Time.deltaTime * brakingInit / braking * 40;
            if (timer > 0.3)
            {
                rgbd.AddRelativeForce(-Time.deltaTime * carAcceleration / 2, 0, 0);
            }
            else
            {
                rgbd.AddRelativeForce(-Time.deltaTime * carAcceleration * braking / brakingInit * speedLocal / 40, 0, 0);
            }
        }
        else
        {
            braking = brakingInit;
        }
        if (accelerate)
        {
            rgbd.AddRelativeForce(Time.deltaTime * carAcceleration, 0, 0);
        }

        //        rgbd.velocity = transform.TransformDirection(new Vector3(10, 0, 0));
        //       Debug.Log(rgbd.velocity);
        rgbd.AddRelativeForce(0, -Time.deltaTime * braking * speedDrift, 0);
        transform.Rotate(0, 0, -h * Time.deltaTime * speedLocal * steering * braking);
        foreach (ParticleSystem pcSystem in pcSystems)
        {
            bool drift_condition = (Mathf.Abs(speedDrift) > 20 || (timer==0 && v<0));

            if (drift_condition == true)
            {
                timerDrift = timerDrift + Time.deltaTime;
                if (timerDrift>0.4)
                {
                    GameManager.Instance().GetPlayer().GetComponent<StreamViewManager>().UpdateStreamPoints(pointsDrift);
                    Debug.Log("tipDrift");
                    timerDrift = 0;
                }
            }
            if (drift_condition && !pcSystem.isPlaying)
            {
                pcSystem.Play(true);
                driftSound.source.pitch = initialPitch + Random.Range(-0.2f, 0.2f);
                driftSound.PlayWithFadeIn();
                //GameManager.Instance().GetPlayer().GetComponent<StreamViewManager>().UpdateStreamPoints(pointsDrift);
                
            } else if ((Mathf.Abs(speedDrift) < 10 || timer > 0.1) && pcSystem.isPlaying && v >= 0) {
                driftSound.StopWithFadeOut();
                pcSystem.Pause(true);
                //GameManager.Instance().GetPlayer().GetComponent<StreamViewManager>().UpdateStreamPoints(pointsDrift);
            }
            //Debug.Log(pcSystem.isPlaying+"    "+speedDrift);
        }
    }
}
