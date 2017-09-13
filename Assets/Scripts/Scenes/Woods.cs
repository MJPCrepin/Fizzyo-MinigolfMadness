using System.Collections;
using UnityEngine;

public class Woods : LevelContent {

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
                SetPar(4);
                break;
            case 3:
                SetNewPosition(3f, 7.5f, -47f);
                SetPar(9);
                break;
            case 4:
                SetNewPosition(32.8f, 20f, -45f);
                SetPar(4);
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
            case 8:
                SetNewPosition(-82.43f, 17.53f, -45.63f);
                SetPar(10);
                break;
            case 9:
                SetNewPosition(-57f, 0f, -127f);
                SetPar(12); 
                break;
            case 10:
                SetNewPosition(-69.23f, 9.33f, -46.714f);
                SetPar(8); 
                break; 
            case 11:
                SetNewPosition(36.5f, 8f, -40.5f);
                SetPar(8);
                break; 
            case 12:
                SetNewPosition(47.7f, 0.5f, -34f);
                SetPar(5);
                break; 
            case 13:
                SetNewPosition(-30f, 10.3f, -50f);
                SetPar(10);
                break;
            case 14:
                SetNewPosition(11f, 8f, -138f);
                SetPar(8);
                break;
            case 15:
                SetNewPosition(74f, 0f, 12f);
                SetPar(5);
                break;
            case 16:
                SetNewPosition(79f, 9f, -125f);
                SetPar(7);
                break;
            default: Debug.Log("This shouldn't happen"); StartCoroutine(BackToMainMenu()); break;
        }
    }

}
