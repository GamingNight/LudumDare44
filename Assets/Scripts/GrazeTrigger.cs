using UnityEngine;

public class GrazeTrigger : MonoBehaviour {

    public int points = 30;
    public bool active = true;

    private void OnTriggerEnter(Collider other) {

        GameObject player = GameManager.Instance().GetPlayer();

        if (other.gameObject == player && active) {
        	CarController car_controller = player.GetComponent<CarController>();
        	float speedDrift = Mathf.Abs(car_controller.speedDrift);
        	float speedLocal = Mathf.Abs(car_controller.speedLocal);
        	float limit = 10;
            if ((speedDrift > limit) || (speedLocal > limit))
            	player.GetComponent<StreamViewManager>().UpdateStreamPoints(points);
        }
    }
}
