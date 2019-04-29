using UnityEngine;

public class NPCGenerator : MonoBehaviour {

    public int totalNPCCount = 2;
    public GameObject npcPrefab;
    public GameObject npcNavPointContainer;

    private int currentNPCCount;
    private Vector3[] npcNavPointsPositions;

    void Start() {
        currentNPCCount = 0;
        npcNavPointsPositions = new Vector3[npcNavPointContainer.transform.childCount];
        for (int i = 0; i < npcNavPointContainer.transform.childCount; i++) {
            npcNavPointsPositions[i] = npcNavPointContainer.transform.GetChild(i).position;
        }
    }

    void Update() {
        currentNPCCount = transform.childCount;
        if (currentNPCCount < totalNPCCount) {
            for (int i = 0; i < totalNPCCount - currentNPCCount; i++) {
                GameObject npcClone = Instantiate<GameObject>(npcPrefab);
                npcClone.transform.position = npcNavPointsPositions[Random.Range(0, npcNavPointsPositions.Length)];
                npcClone.transform.parent = transform;
                npcClone.GetComponent<NPCNavigation>().targetContainer = npcNavPointContainer;
            }
        }
    }
}
