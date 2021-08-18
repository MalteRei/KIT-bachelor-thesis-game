using System.Collections;
using UnityEngine;

    public class IslandInnerController : MonoBehaviour
    {

    public TrackOnWhichIslandController trackOnWhichIslandController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
            trackOnWhichIslandController.isOnEdgeOfIsland = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Character"))
            {
            trackOnWhichIslandController.isOnEdgeOfIsland = true;
            }
        }
    }
