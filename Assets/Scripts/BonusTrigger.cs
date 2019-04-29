using UnityEngine;

public class BonusTrigger : MonoBehaviour {

    public int nbPoints;
    public bool destroyAfterScoring;

    public void OnTriggerEnter(Collider other) {

        GameObject player = GameManager.Instance().GetPlayer();
        if (other.gameObject == player) {
            player.GetComponent<StreamViewManager>().UpdateStreamPoints(nbPoints);
            if (destroyAfterScoring) {
                Destroy(gameObject);
            }
        }
    }
}
