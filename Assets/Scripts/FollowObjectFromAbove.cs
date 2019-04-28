using UnityEngine;

public class FollowObjectFromAbove : MonoBehaviour {

    public GameObject objectToFollow;
    public bool strictFollow;

    public float smooth;
    public float minDistance = 40;
    public float maxDistance = 60;
    public float offsetValue;

    public float maxXDistanceValue;
    public float maxZDistanceValue;

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate() {

        if (strictFollow) {
            transform.position = new Vector3(objectToFollow.transform.position.x, transform.position.y, objectToFollow.transform.position.z);
        } else {

            //Adjust the distance to the object to follow in function of its velocity.
            float normVelocityForDistance = 1 - (60 - Mathf.Min(Mathf.Max(velocity.magnitude, 20), 60)) / (60 - 20);
            float distance = (maxDistance - minDistance) * normVelocityForDistance + minDistance;

            //Adjust the offset in function of the velocity
            float normVelocityForOffset = Mathf.Min(velocity.magnitude, 10) / 10f;
            float adjustedOffsetValue = offsetValue * normVelocityForOffset;
            offset = objectToFollow.transform.right * adjustedOffsetValue;

            Vector3 target = new Vector3(objectToFollow.transform.position.x + offset.x, distance, objectToFollow.transform.position.z + offset.z);
            Vector3 transformToTarget = target - transform.position;
            //Vector3 smoothTarget = Vector3.Lerp(transform.position, target, Time.deltaTime);
            Vector3 smoothTarget = Vector3.SmoothDamp(transform.position, target, ref velocity, smooth * Time.deltaTime);

            Vector3 clampedTarget = smoothTarget;
            if (Mathf.Abs(transformToTarget.x) > maxXDistanceValue) {
                if (transformToTarget.x > 0) {
                    clampedTarget.x = target.x - maxXDistanceValue;
                } else {
                    clampedTarget.x = target.x + maxXDistanceValue;
                }
            }
            if (Mathf.Abs(transformToTarget.z) > maxZDistanceValue) {
                if (transformToTarget.z > 0) {
                    clampedTarget.z = target.z - maxZDistanceValue;
                } else {
                    clampedTarget.z = target.z + maxZDistanceValue;
                }
            }

            transform.position = clampedTarget;
        }
    }


}
