﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

	public SaveState state;
    public static SaveManager Instance { get; set; }

    public Material playerMaterial;
    public Color[] playerColours = new Color[13];
    public GameObject[] playerHats = new GameObject[13];
    public GameObject[] playerTrails = new GameObject[13];

    private void Awake()
    {
        ResetSave(); // Run once with this uncommented to reset the save file
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetString("Save", Tools.Serialise<SaveState>(state));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            state = Tools.Deserialise<SaveState>(PlayerPrefs.GetString("Save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found, creating new");
        }
    }

    public bool IsColourOwned(int index)
    { // If int bit is set, colour owned
        return (state.colourOwned & (1 << index)) != 0;
    }

    public bool IsHatOwned(int index)
    { // If int bit is set, colour owned
        return (state.hatOwned & (1 << index)) != 0;
    }

    public bool IsTrailOwned(int index)
    { // If int bit is set, colour owned
        return (state.trailOwned & (1 << index)) != 0;
    }

    public bool BuyColour(int index, int price)
    {
        if (state.coins >= price)
        {
            state.coins -= price;
            UnlockColour(index);
            Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool BuyHat(int index, int price)
    {
        if (state.coins >= price)
        {
            state.coins -= price;
            UnlockHat(index);
            Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool BuyTrail(int index, int price)
    {
        if (state.coins >= price)
        {
            state.coins -= price;
            UnlockTrail(index);
            Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UnlockColour(int index)
    { // | = toggle on, ^ = toggle off
        state.colourOwned |= 1 << index;
    }

    public void UnlockHat(int index)
    { // | = toggle on, ^ = toggle off
        state.hatOwned |= 1 << index;
    }

    public void UnlockTrail(int index)
    { // | = toggle on, ^ = toggle off
        state.trailOwned |= 1 << index;
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("Save");
    }
}
