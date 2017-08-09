using System.Collections;
using UnityEngine;

public class Tutorial : LevelContent {

    // Tutorial level specific content

    private bool preventRotation = false;

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
        if (preventRotation == true) player.pc.stopRotating();
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
                player.direction = 0;
                preventRotation = true;
                ShowPopup("Huff to move the ball!", 5f);
                SetNewPosition(-6.6f, 0.1f, -6.75f);
                SetPar(2);
                break;
            case 2:
                preventRotation = false;
                ShowPopup("Click to change direction!", 5f);
                SetNewPosition(0, 0.1f, -6.75f);
                SetPar(3);
                break;
            case 3:
                ShowPopup("Collect coins to spend in the shop!", 5f);
                SetNewPosition(7f, 0.1f, -6.75f);
                SetPar(2);
                break;
            default: Debug.Log("This shouldn't happen"); StartCoroutine(BackToMainMenu()); break;
        }
    }
}
