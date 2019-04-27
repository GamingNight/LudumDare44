using UnityEngine;

public class CollisionTrigger : MonoBehaviour {

    public int points = -10;

    private void OnCollisionEnter(Collision collision) {

        GameObject player = GameManager.Instance().GetPlayer();
        if (collision.gameObject == player) {
            player.GetComponent<StreamViewManager>().UpdateStreamPoints(points);
        }
    }
}
