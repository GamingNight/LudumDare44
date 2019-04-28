using UnityEngine;

public class CollisionTrigger : MonoBehaviour {

    public int points = -10;
    private AudioSource collisionSound;
    private float initialPitch;
    private float initialVolume;

    private void Start() {

        collisionSound = GetComponent<AudioSource>();
        initialPitch = collisionSound.pitch;
        initialVolume = collisionSound.volume;
    }

    private void OnCollisionEnter(Collision collision) {

        GameObject player = GameManager.Instance().GetPlayer();
        if (collision.gameObject == player) {
            player.GetComponent<StreamViewManager>().UpdateStreamPoints(points);
            collisionSound.pitch = initialPitch + Random.Range(-0.3f, 0.3f);
            collisionSound.volume = initialVolume + Random.Range(-0.2f, 0.2f);
            collisionSound.Play();
        }
    }
}
