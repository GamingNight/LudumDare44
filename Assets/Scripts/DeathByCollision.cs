using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DeathByCollision : MonoBehaviour {

    public Sprite deathSprite;
    public SpriteRenderer spriteRenderer;

    private void OnTriggerEnter(Collider other) {

        GameObject player = GameManager.Instance().GetPlayer();
        if (other.gameObject == player) {
            GetComponent<NavMeshAgent>().isStopped = true;
            spriteRenderer.sprite = deathSprite;
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
