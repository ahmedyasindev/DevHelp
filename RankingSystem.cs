using System.Collections.Generic;
using MalbersAnimations.Controller;
using UnityEngine;
using UnityEngine.UI;

public class RankingSystem : MonoBehaviour
{
    public List<Transform> racers;
    public Transform[] waypoints;
    public float margin = 5f;

    public int[] racerRanks;
    public int[] racerWaypoints;

   // public AnimalAnimationController animalAnimationController;
   public bool GameStarted;
    public bool showGizmoz = true;

    public Transform[] playersNamePlate;
    public Text[] playersNamePlateText;
   public void SetStarter()
    {
        racerRanks = new int[racers.Count];
        racerWaypoints = new int[racers.Count];
        
        for (int i = 0; i < racers.Count; i++)
        {
            playersNamePlate[i].gameObject.SetActive(true);
        }
     

    }
    
    void Update()
    {
        if (GameStarted)
        {
            for (int i = 0; i < racers.Count; i++)
            {
                Transform racer = racers[i];
                int waypointIndex = racerWaypoints[i];

                foreach (Transform tr in racers)
                    if (tr == null)
                        return;

                if (Vector3.Distance(racer.position, waypoints[waypointIndex].position) < margin)
                {
                    racerWaypoints[i]++;
                   
                }

            }

            List<int> sortedRacers = new List<int>();
            for (int i = 0; i < racers.Count; i++)
            {
                sortedRacers.Add(i);
            }

            sortedRacers.Sort((a, b) =>
            {
                int waypointDiff = racerWaypoints[b] - racerWaypoints[a];
                if (waypointDiff != 0)
                    return waypointDiff;

                float distA = Vector3.Distance(racers[a].position, waypoints[racerWaypoints[a]].position);
                float distB = Vector3.Distance(racers[b].position, waypoints[racerWaypoints[b]].position);
                return distA.CompareTo(distB);
            });

            for (int rank = 0; rank < sortedRacers.Count; rank++)
            {
                int racerIndex = sortedRacers[rank];
                racerRanks[racerIndex] = rank + 1;
                playersNamePlate[racerIndex].SetSiblingIndex(rank);
                
            }
        }
    }

    void OnDrawGizmos()
    {
        if (showGizmoz)
        {
            if(waypoints.Length > 0)
            {
                Gizmos.color = Color.yellow;
                foreach (Transform waypoint in waypoints)
                {
                    Gizmos.DrawWireSphere(waypoint.position, margin);
                }
            }
        }
    }
}
