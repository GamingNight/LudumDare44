using UnityEngine;

public class FollowObject : MonoBehaviour
{

    public GameObject objectToFollow;

    //void Update() {

    //    transform.position = new Vector3(objectToFollow.position.x, transform.position.y, objectToFollow.position.z);
    //}

    public float smooth;
    public float distance;
    public float offsetValue;

    private Vector3 position;
    private Vector3 offset;

    void Start()
    {

        position = transform.position;
        offset = new Vector3(offsetValue, 0f, 0f);

    }

    void FixedUpdate()
    {

        position = Vector3.Lerp(position, objectToFollow.transform.position, (smooth * Time.fixedDeltaTime));
        position = new Vector3(position.x, distance, position.z);

        transform.position = position + offset;

    }


}
