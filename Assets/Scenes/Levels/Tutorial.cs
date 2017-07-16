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
            BackToMainMenu(3f);
        }

        switch (holeNumber)
        {
            case 2: SetNewPosition(0, 0.5f, -6.75f); break;
            case 3: SetNewPosition(6.5f, 0.5f, -6.75f); break;
        }
        
    }

    private void SetNewPosition(float x, float y, float z)
    {
        player.transform.position = new UnityEngine.Vector3(x,y,z);
    }
}
