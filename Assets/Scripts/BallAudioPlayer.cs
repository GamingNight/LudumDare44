using UnityEngine;

public class BallAudioPlayer : MonoBehaviour {
    public AudioClip[] clips;
    private AudioSource source;
    private float initPitch;

    private void Start() {

        source = GetComponent<AudioSource>();
        initPitch = source.pitch;
    }

    private void Update() {
        float distToCar = (GameManager.Instance().GetPlayer().transform.position - transform.position).magnitude;
        distToCar = Mathf.Min(distToCar, 100);
        source.volume = 0.7f * (1 - (distToCar / 100f));
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Road") {
            return;
        }

        if (source.isPlaying) {
            source.Stop();
        }
        source.clip = clips[Random.Range(0, clips.Length)];
        source.pitch = initPitch + Random.Range(-0.1f, 0.1f);
        source.Play();
    }
}
