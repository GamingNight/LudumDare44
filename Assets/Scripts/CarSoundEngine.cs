using UnityEngine;

public class CarSoundEngine : MonoBehaviour {
    public Rigidbody rgbd;
    public float minPitch = 0.6f;
    public float maxPitch = 1.2f;
    private AudioFade engine;

    private void Start() {
        engine = GetComponent<AudioFade>();
    }

    void Update() {
        float normVelocity = Mathf.Min(1, rgbd.velocity.magnitude / 40f);
        engine.source.pitch = minPitch + normVelocity * (maxPitch - minPitch);
        if (!engine.isPlaying && !engine.isFadingIn) {
            engine.PlayWithFadeIn();
        }
    }
}
