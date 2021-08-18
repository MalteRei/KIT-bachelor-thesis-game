using System.Collections;
using UnityEngine;


    public class IslandProximityController : MonoBehaviour
    {
        public Island island;
        public TrackOnWhichIslandController trackOnWhichIslandController;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                trackOnWhichIslandController.currentIslandCharacterOn = island;
            }
        }

      /*  private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Character"))
            {
            trackOnWhichIslandController.currentIslandCharacterOn = null;
            }
        }*/
    }
