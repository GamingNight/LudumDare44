using UnityEngine;

public class GrazeTrigger : MonoBehaviour {

    public int points = 30;
    public bool active = true;
    public float grazeSpeedLimit = 15;
    public bool updateViewersAtStay = false;

    private float timeSinceLastStayUpdate;

    private void Start() {
        timeSinceLastStayUpdate = Time.time;
    }

    private void OnTriggerEnter(Collider other) {

        GameObject player = GameManager.Instance().GetPlayer();

        if (other.gameObject == player && active) {
            CarController carController = player.GetComponent<CarController>();
            float speedDrift = Mathf.Abs(carController.speedDrift);
            float speedLocal = Mathf.Abs(carController.speedLocal);
            if ((speedDrift > grazeSpeedLimit) || (speedLocal > grazeSpeedLimit)) {
                player.GetComponent<StreamViewManager>().UpdateStreamPoints(points);
                // carController.setFearStatus(true); Fear on graze is deactivated
            }
        }
    }

    private void OnTriggerStay(Collider other) {

        GameObject player = GameManager.Instance().GetPlayer();
        if (other.gameObject == player && active) {

            if (!updateViewersAtStay || (Time.time - timeSinceLastStayUpdate) < 0.2) {
                return;
            }

            CarController car_controller = player.GetComponent<CarController>();
            float speedDrift = Mathf.Abs(car_controller.speedDrift);
            float speedLocal = Mathf.Abs(car_controller.speedLocal);
            if ((speedDrift > grazeSpeedLimit) || (speedLocal > grazeSpeedLimit)) {
                player.GetComponent<StreamViewManager>().UpdateStreamPoints(points);
                timeSinceLastStayUpdate = Time.time;
            }
        }
    }
}
