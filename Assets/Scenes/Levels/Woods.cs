using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woods : LevelContent {


    private void Start()
    {
        initCounters();
        HidePopup();
        SpawnAtNewHole(1);
    }

    private void Update()
    {
        DetectBreathTrigger();
        if (player.isAtEndpoint) GoToNextHole();
        if (player.isInDeathzone) SpawnAtNewHole(currentHole);
    }

    public void GoToNextHole()
    {
        EndpointReached();
        SpawnAtNewHole(currentHole);
    }

    private void SpawnAtNewHole(int holeNumber)
    {
        player.isInDeathzone = false;
        switch (holeNumber)
        {
            case 1:
                SetNewPosition(0, 1f, -6.75f);
                SetPar(2);
                StartCoroutine(ShowPopup("Hole 1", 3f));
                break;
            case 2:
                SetNewPosition(0, 1f, -6.75f);
                SetPar(2);
                StartCoroutine(ShowPopup("Hole 2", 3f));
                break;
            case 3:
                SetNewPosition(0, 1f, -6.75f);
                SetPar(2);
                StartCoroutine(ShowPopup("Hole 2", 3f));
                break;
            default: Debug.Log("This shouldn't happen"); StartCoroutine(BackToMainMenu()); break;
        }

    }


}
