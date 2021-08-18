using System.Collections;
using UnityEngine;


public class GoalPoint : MonoBehaviour
{

    public bool visited = false;

    public GameObject islandPlacedOn;

    public Material visitedMaterial;
    public GameObject flag;
    private Renderer flagRenderer;

    public AudioSource audioSource;

    private void Start()
    {
        if(flag != null)
        {
            flagRenderer = flag.GetComponent<Renderer>();
        }
    }

    public void VisitGoalPoint()
    {
        if(!visited)
        {
            visited = true;
            if(audioSource != null)
            {
                audioSource.Play();
            }
            ChangeFlagToVisited();
        }
    }

    private void ChangeFlagToVisited()
    {
        if(flag != null && flagRenderer && visitedMaterial != null)
        {
            flagRenderer.material = visitedMaterial;
        }
    }

}
