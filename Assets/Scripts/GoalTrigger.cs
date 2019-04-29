using UnityEngine;

public class GoalTrigger : MonoBehaviour {

    public int nbPoints;

    public void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Ball") {
            GameObject player = GameManager.Instance().GetPlayer();
            player.GetComponent<StreamViewManager>().UpdateStreamPoints(nbPoints);
        }
    }
}
