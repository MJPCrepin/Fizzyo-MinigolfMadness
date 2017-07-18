using System;

public class Tutorial : LevelContent {


    private void Start()
    {
        initCounters();
        wintxt.text = "";
        finalHole = 3;
    }

    private void Update()
    {
        if (player.isAtEndpoint)
        {
            EndpointReached();
        }
    }

    public new void EndpointReached()
    {
        player.isAtEndpoint = false;
        UpdateCurrHole();
        SpawnAtNewHole(currentHole);
        //wintxt.text = "YAY";
        //StartCoroutine(BackToMainMenu(3f));   
    }

    private void SpawnAtNewHole(int holeNumber)
    {
        if (currentHole == finalHole)
        {
            BackToMainMenu();
        }

        switch (holeNumber)
        {
            case 2: SetNewPosition(0, 1f, -6.75f); break;
            case 3: SetNewPosition(6.5f, 1f, -6.75f); break;
        }
        
    }


}
