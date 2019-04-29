using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCGenerator : MonoBehaviour {

    public int totalNPCCount = 2;
    public GameObject npcPrefab;
    public GameObject npcNavPointContainer;

    private int currentNPCCount;
    private Vector3[] npcNavPointsPositions;
    private int[] npcDistribution;
    private int nbFrameLatency;
    Dictionary<GameObject, int> latencyMap;

    void Start() {
        currentNPCCount = 0;
        npcNavPointsPositions = new Vector3[npcNavPointContainer.transform.childCount];
        npcDistribution = new int[npcNavPointContainer.transform.childCount];
        for (int i = 0; i < npcNavPointContainer.transform.childCount; i++) {
            npcNavPointsPositions[i] = npcNavPointContainer.transform.GetChild(i).position;
            npcDistribution[i] = 0;
        }

        nbFrameLatency = 3;
        latencyMap = new Dictionary<GameObject, int>();
    }


    void Update() {

        currentNPCCount = transform.childCount;

        //foreach (Transform child in transform) {
        //    Debug.Log("A " + child.gameObject.name + " = " + child.position);
        //}

        if (currentNPCCount < totalNPCCount) {
            for (int i = 0; i < totalNPCCount - currentNPCCount; i++) {
                GameObject npcClone = Instantiate<GameObject>(npcPrefab);
                int randomIndex = Random.Range(0, npcNavPointsPositions.Length);
                npcClone.transform.position = npcNavPointsPositions[randomIndex];
                npcClone.transform.parent = transform;
                npcClone.name += "From_Point_" + randomIndex + "_" + npcNavPointsPositions[randomIndex].ToString();
                npcClone.GetComponent<NPCNavigation>().targetContainer = npcNavPointContainer;
                npcDistribution[randomIndex]++;

                latencyMap.Add(npcClone, nbFrameLatency);

                //NavMeshHit hit;
                //bool found = NavMesh.SamplePosition(npcClone.transform.position, out hit, 1.0f, NavMesh.AllAreas);
                //Debug.Log("a position was found on the navmesh = " + found + ", at " + hit.position);
            }
            //ShowNPCDistribution();
        }

        //foreach (Transform child in transform) {
        //    Debug.Log("B " + child.gameObject.name + " = " + child.position);
        //}

        Dictionary<GameObject, int> _map = new Dictionary<GameObject, int>(latencyMap);
        foreach (GameObject npc in _map.Keys) {
            latencyMap[npc]--;
            if (latencyMap[npc] == 0) {
                npc.GetComponent<NavMeshAgent>().enabled = true;
                npc.GetComponent<NPCNavigation>().enabled = true;
                latencyMap.Remove(npc);
            }
        }
    }

    private void ShowNPCDistribution() {

        Debug.Log("NPC Distribution:");
        for (int i = 0; i < npcDistribution.Length; i++) {
            Debug.Log("At point " + i + ": " + npcDistribution[i] + " npc have been instantiated so far");
        }
    }
}
