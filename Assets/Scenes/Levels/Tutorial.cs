using System;

public class Tutorial : LevelContent {


    private void Start()
    {
        initCounters();
        wintxt.text = "";
    }

    private void Update()
    {
        
    }

    public new void EndpointReached()
    {
        // FIX THIS (only parent being picked up)
        wintxt.text = "YAY";
        StartCoroutine(BackToMainMenu(0.3f));   
    }
}
