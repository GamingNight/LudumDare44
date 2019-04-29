using UnityEngine;

public class NPCarGenerator : MonoBehaviour {

    public GameObject nPCarPrefab;
    public GameObject nPCarNavPointContainer;

    private Vector3[] nPCarNavPointsPositions;
    private GameObject[] nPCars;

    void Start() {
    	Debug.Log("Nb point"+nPCarNavPointContainer.transform.childCount);
    	int nbCars = nPCarNavPointContainer.transform.childCount;
        nPCarNavPointsPositions = new Vector3[nbCars];
        nPCars = new GameObject[nbCars];
        for (int i = 0; i < nbCars; i++) {
        	Debug.Log("i "+ i);
            nPCarNavPointsPositions[i] = nPCarNavPointContainer.transform.GetChild(i).position;
            nPCars[i] = Instantiate<GameObject>(nPCarPrefab);
            nPCars[i].transform.position = nPCarNavPointsPositions[i];
            nPCars[i].GetComponent<NPCarNavigator>().targetContainer = nPCarNavPointContainer;
            nPCars[i].GetComponent<NPCarNavigator>().posID = i;
        }
    }

    public Vector3[] getNPCarNavPointsPositions() {
    	return nPCarNavPointsPositions;
    }

    public GameObject[] getNPCars() {
    	return nPCars;
    }

/*    void Update() {
        currentNPCCount = transform.childCount;
        if (currentNPCCount < totalNPCCount) {
            for (int i = 0; i < totalNPCCount - currentNPCCount; i++) {
                GameObject npcClone = Instantiate<GameObject>(npcPrefab);
                npcClone.transform.position = npcNavPointsPositions[Random.Range(0, npcNavPointsPositions.Length)];
                npcClone.transform.parent = transform;
                npcClone.GetComponent<NPCNavigation>().targetContainer = npcNavPointContainer;
            }
        }
    }*/
}