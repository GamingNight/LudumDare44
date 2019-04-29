using UnityEngine;

public class NPCGenerator : MonoBehaviour {

    public int totalNPCCount = 2;
    public GameObject npcPrefab;
    public GameObject npcNavPointContainer;

    private int currentNPCCount;
    private Vector3[] npcNavPointsPositions;
    private int[] npcDistribution;

    void Start() {
        currentNPCCount = 0;
        npcNavPointsPositions = new Vector3[npcNavPointContainer.transform.childCount];
        npcDistribution = new int[npcNavPointContainer.transform.childCount];
        for (int i = 0; i < npcNavPointContainer.transform.childCount; i++) {
            npcNavPointsPositions[i] = npcNavPointContainer.transform.GetChild(i).position;
            npcDistribution[i] = 0;
        }
    }


    void Update() {
        currentNPCCount = transform.childCount;
        if (currentNPCCount < totalNPCCount) {
            for (int i = 0; i < totalNPCCount - currentNPCCount; i++) {
                GameObject npcClone = Instantiate<GameObject>(npcPrefab);
                int randomIndex = Random.Range(0, npcNavPointsPositions.Length);
                npcClone.transform.position = npcNavPointsPositions[randomIndex];
                npcClone.transform.parent = transform;
                npcClone.name += "From_Point_" + randomIndex + "_" + npcNavPointsPositions[randomIndex].ToString();
                npcClone.GetComponent<NPCNavigation>().targetContainer = npcNavPointContainer;
                npcDistribution[randomIndex]++;
            }
            //ShowNPCDistribution();
        }
    }

    private void ShowNPCDistribution() {

        Debug.Log("NPC Distribution:");
        for (int i = 0; i < npcDistribution.Length; i++) {
            Debug.Log("At point " + i + ": " + npcDistribution[i] + " npc have been instantiated so far");
        }
    }
}
