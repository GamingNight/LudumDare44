using UnityEngine;

public class GrazeTrigger : MonoBehaviour {

    public int points = 30;

    private void OnTriggerEnter(Collider other) {

        GameObject player = GameManager.Instance().GetPlayer();
        if (other.gameObject == player) {
            player.GetComponent<StreamViewManager>().UpdateStreamPoints(points);
        }
    }
}
