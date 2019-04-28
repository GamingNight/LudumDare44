using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DeathByCollision : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public float points = -50;
    public bool active = true;

    private GrazeTrigger grazeTrigger;
    private AudioSource collisionSound;

    private void Start() {
        grazeTrigger = GetComponentInChildren<GrazeTrigger>();
        collisionSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {

        GameObject player = GameManager.Instance().GetPlayer();
        if (other.gameObject == player && active) {
            player.GetComponent<StreamViewManager>().UpdateStreamPoints(points);
            animator.SetTrigger("die");
            collisionSound.Play();
            player.GetComponent<CarController>().setFearStatus(true);
            spriteRenderer.sortingOrder = 0;
            spriteRenderer.color = new Color(255, 255, 255, 255);
            grazeTrigger.active = false;
            active = false;
            GetComponent<NPCNavigation>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(InitiateDestroyCoroutine());
        }
    }

    private IEnumerator InitiateDestroyCoroutine() {

        float totalTime = 4f;
        float currentTime = 0f;
        float step = 0.01f;
        while (currentTime < totalTime) {
            float alpha = Mathf.Lerp(1, 0, currentTime / totalTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            yield return new WaitForSeconds(step);
            currentTime += step;
        }
        Destroy(gameObject);
    }
}
