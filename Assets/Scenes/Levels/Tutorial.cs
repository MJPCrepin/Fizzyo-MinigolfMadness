using System;

public class Tutorial : LevelContent {


    private void Start()
    {
        initCounters();
        HidePopup();
    }

    private void Update()
    {
        if (player.isAtEndpoint)
        {
            GoToNextHole();
        }

        if (currentHole == 1)
        {
            player.direction = 0;
            player.pc.stopRotating();
            //ShowPopup("Huff to move the ball!");
            SetPar(2);
        }

        if (currentHole == 2)
        {
           // ShowPopup("Click to change direction!");
        }

        if (currentHole == 3)
        {
            ShowPopup("Pick up extra coins to spend in the shop!");
        }

        if (currentHole == 4)
        {
            ShowPopup("Tutorial Complete!");
        }

    }

    public void GoToNextHole()
    {
        EndpointReached();
        SpawnAtNewHole(currentHole);
    }

    private void SpawnAtNewHole(int holeNumber)
    {

        switch (holeNumber)
        {
            case 2: SetNewPosition(0, 1f, -6.75f); break;
            case 3: SetNewPosition(6.5f, 1f, -6.75f); break;
            default: StartCoroutine(BackToMainMenu()); break;
        }
        
    }


}
