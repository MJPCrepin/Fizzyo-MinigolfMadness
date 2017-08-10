using System.Collections;
using UnityEngine;

public class Woods : LevelContent {

    // Tutorial level specific content

    private void Start()
    {
        initCounters();
        HidePopup();
        StartCoroutine(SpawnAtNewHole(1));
    }

    private void Update()
    {
        DetectBreathTrigger();
        if (player.isAtEndpoint) GoToNextHole();
        if (player.isSkippingHole) { player.isSkippingHole = false; StartCoroutine(SpawnAtNewHole(currentHole)); }
        if (player.isInDeathzone) StartCoroutine(SpawnAtNewHole(currentHole));
    }

    public void GoToNextHole()
    {
        EndpointReached();
        StartCoroutine(SpawnAtNewHole(currentHole));
    }

    private IEnumerator SpawnAtNewHole(int holeNumber)
    { // Pause before respawning
        player.isInDeathzone = false;

        float currCountdownValue = 1f;
        while (currCountdownValue > 0) { yield return new WaitForSeconds(1.0f); currCountdownValue--; }

        switch (holeNumber)
        {
            case 1:
                SetNewPosition(-0.7f, 1f, -7f);
                SetPar(2);
                break;
            case 2:
                SetNewPosition(-30.7f, 2.4f, -14f);
                SetPar(2);
                break;
            case 3:
                SetNewPosition(3f, 7.5f, -47f);
                SetPar(3);
                break;
            case 4:
                SetNewPosition(32.8f, 20f, -45f);
                SetPar(1);
                break;
            case 5:
                SetNewPosition(-29.8f, 3.75f, -47f);
                SetPar(4);
                break;
            case 6:
                SetNewPosition(14f, 10f, -83f);
                SetPar(1);
                break;
            case 7:
                SetNewPosition(80.14f, 8.9f, -62.8f);
                SetPar(3);
                break;
            default: Debug.Log("This shouldn't happen"); StartCoroutine(BackToMainMenu()); break;
        }
    }

}
