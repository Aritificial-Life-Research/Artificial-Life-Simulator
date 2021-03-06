﻿// Eco-Simulator
// Copyright (c) 2019 Brett Layman
// This file is subject to the terms and conditions defined in 'LICENSE.txt', which is part of this source code repository.

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for running simulation.
/// </summary>
public class SimRunnerTest : MonoBehaviour
{
    public GameObject stepsText;
    public GameObject mapSpriteObj;
    SpriteRenderer sr;
    Texture2D texture;

    float elapsedTime = 0.0f;
    float intervalTime = .25f; // updates every fraction of a second if possible
    ThreadManager threader;
    int intervalSteps = 1;
    bool paused = false;
    bool started = false;
    

    // Use this for initialization
    void Start()
    {
        

    }

    public void menuStart()
    {
        sr = mapSpriteObj.GetComponent<SpriteRenderer>();
        Debug.LogWarning("*******************              simRunner awake              ******************");
        // create threader
        threader = gameObject.GetComponent<ThreadManager>();
        // perform initial renderings
        //startRender(threader.getEcosystem());
        Ecosystem sys = threader.getEcosystem();
        texture = new Texture2D(sys.map.Count, sys.map[0].Count, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;
        //updateRenderT(sys); // is this one necessary? costly?
        // set steps for each interval
        threader.setSteps(intervalSteps);
        // initiate simulation on child thread
        threader.StartEcoSim();
        started = true;
    }

    public void flipPaused()
    {
        if (paused)
        {
            paused = false;
        }
        else
        {
            paused = true;
        }
    }

    public void setSteps()
    {
        string text = stepsText.GetComponent<Text>().text;
        bool valid = HelperValidator.validateIntegerString(text);
        if (valid)
        {
            threader.setSteps(Int32.Parse(text));
        }
        
    }

    public void getValFromSlider(float value)
    {
        // TODO: don't hard code slider
        intervalTime = .5f - value;
        //Debug.Log("interval time: " + value);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(StaticVariables.threadManagerAwake);
        if (!paused && started)
        {
            elapsedTime += Time.deltaTime;
            // only update every intervalTime seconds
            if (elapsedTime > intervalTime)
            {
                // call function to update ecosystem if the child thread has added ecosystem objects to the queue
                bool updated = threader.updateEcoIfReady();
                // only re-render when necessary (the ecosystem has changed)
                if (updated)
                {
                    updateRenderT(threader.getEcosystem());
                }
                elapsedTime = 0.0f; // reset timer
            }
        }
        else // paused
        {
            // TODO: render user changes?
        }
        

    }


    private void updateRenderT(Ecosystem sys)
    {
        texture.SetPixels(sys.colors);
        texture.Apply();
        sr.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 1, 0, SpriteMeshType.FullRect);
        sr.sharedMaterials[0].mainTexture = texture;
    }

    /*
    // first render: called from Start function
    private void startRender(Ecosystem sys)
    {
        //Debug.Log("in render");
        for (int x = 0; x < sys.map.Count; x++)
        {
            tiles.Add(new List<GameObject>());
            for (int y = 0; y < sys.map[x].Count; y++)
            {
                GameObject tile = GameObject.Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                float proportionStored = (float)sys.map[x][y].propertyDict["grass"].getProportionStored();
                //Debug.Log(proportionStored);
                tile.GetComponent<SpriteRenderer>().color = new Color(proportionStored, proportionStored, proportionStored);
                tiles[x].Add(tile);
            }
        }
    }

    // called from update loop
    private void updateRender(Ecosystem sys)
    {
        //Debug.Log("in render");
        GameObject tile;
        ResourceStore store;
        Color updatedColor = new Color(1, 1, 1);
        float proportionStored;
        List<List<Land>> map = sys.map;

        for (int x = 0; x < sys.map.Count; x++)
        {
            for (int y = 0; y < sys.map[x].Count; y++)
            {
                tile = tiles[x][y];
                if (map[x][y].creatureIsOn())
                {
                    updatedColor = Color.blue;
                    tile.GetComponent<SpriteRenderer>().color = updatedColor;
                }
                else
                {
                    store = map[x][y].propertyDict["grass"];
                    proportionStored = store.amountStored / store.maxAmount;
                    //Debug.Log(proportionStored);
                    updatedColor.r = proportionStored;
                    updatedColor.g = proportionStored;
                    updatedColor.b = proportionStored;
                    tile.GetComponent<SpriteRenderer>().color = updatedColor;
                }

            }
        }
    }
    */

    // TODO : make Getter classes for ecosystem, creature, and other classes to retrieve information from them to use in the UI

}
