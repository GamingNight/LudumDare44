using UnityEngine;

public class GrazeTrigger : MonoBehaviour {

    public int points = 30;
    public bool active = true;
    public float grazeSpeedLimit = 15;

    private void OnTriggerEnter(Collider other) {

        GameObject player = GameManager.Instance().GetPlayer();

        if (other.gameObject == player && active) {
            CarController car_controller = player.GetComponent<CarController>();
            float speedDrift = Mathf.Abs(car_controller.speedDrift);
            float speedLocal = Mathf.Abs(car_controller.speedLocal);
            if ((speedDrift > grazeSpeedLimit) || (speedLocal > grazeSpeedLimit)) {
                player.GetComponent<StreamViewManager>().UpdateStreamPoints(points);
            }
        }
    }
}
