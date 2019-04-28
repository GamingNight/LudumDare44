using UnityEngine;

public class FollowObject : MonoBehaviour {

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

            transform.position = Vector3.Lerp(transform.position, target, unsmooth * Time.deltaTime);
        }
    }


}
