using UnityEngine;

public class GrazeTrigger : MonoBehaviour {

    public int points = 30;
    public bool active = true;

    private void OnTriggerEnter(Collider other) {

        GameObject player = GameManager.Instance().GetPlayer();
        if (other.gameObject == player && active) {
            player.GetComponent<StreamViewManager>().UpdateStreamPoints(points);
        }
    }
}
