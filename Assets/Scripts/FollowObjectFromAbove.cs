using UnityEngine;

public class FollowObjectFromAbove : MonoBehaviour {

    public GameObject objectToFollow;
    public bool strictFollow;

    public float unsmooth;
    public float distance;
    public float offsetValue;

    private Vector3 offset;

    void Update() {

        if (strictFollow) {
            transform.position = new Vector3(objectToFollow.transform.position.x, transform.position.y, objectToFollow.transform.position.z);
        } else {
            offset = objectToFollow.transform.right * offsetValue;
            Vector3 target = new Vector3(objectToFollow.transform.position.x + offset.x, distance, objectToFollow.transform.position.z + offset.z);
            Vector3 smoothTarget = Vector3.Lerp(transform.position, target, unsmooth * Time.deltaTime);

            float distanceToSmoothTarget = (smoothTarget - transform.position).magnitude;

            Vector3 finalTarget = smoothTarget;
            if (distanceToSmoothTarget > 0.5) {
                Vector3 distanceToTargetVec = target - transform.position;
            } else if (distanceToSmoothTarget < 0.2) {
                Debug.Log("Very close");
            }

            transform.position = finalTarget;

        }
    }


}
