using UnityEngine;

public class FollowObjectFromAbove : MonoBehaviour {

    public GameObject objectToFollow;
    public bool strictFollow;

    public float smooth;
    public float distance;
    public float offsetValue;

    public float maxXDistanceValue;
    public float maxZDistanceValue;

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void Update() {

        if (strictFollow) {
            transform.position = new Vector3(objectToFollow.transform.position.x, transform.position.y, objectToFollow.transform.position.z);
        } else {
            offset = objectToFollow.transform.right * offsetValue;
            Vector3 target = new Vector3(objectToFollow.transform.position.x + offset.x, distance, objectToFollow.transform.position.z + offset.z);
            Vector3 transformToTarget = target - transform.position;
            //Vector3 smoothTarget = Vector3.Lerp(transform.position, target, smooth * Time.deltaTime);
            Vector3 smoothTarget = Vector3.SmoothDamp(transform.position, target, ref velocity, smooth * Time.deltaTime);
            if (Mathf.Abs(transformToTarget.x) > maxXDistanceValue) {
                if (transformToTarget.x > 0) {
                    smoothTarget.x = target.x - maxXDistanceValue;
                } else {
                    smoothTarget.x = target.x + maxXDistanceValue;
                }
            }
            if (Mathf.Abs(transformToTarget.z) > maxZDistanceValue) {
                if (transformToTarget.z > 0) {
                    smoothTarget.z = target.z - maxZDistanceValue;
                } else {
                    smoothTarget.z = target.z + maxZDistanceValue;
                }
            }

            transform.position = smoothTarget;
            //transform.LookAt(objectToFollow.transform);
        }
    }


}
