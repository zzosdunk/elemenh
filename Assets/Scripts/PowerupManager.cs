﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {

    private bool doublePoints;
    private bool safeMode;

    private bool powerupActive;

    private float powerupLenghtCounter;

    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;
    private GameManager theGameManager;

    private float normalPointsPerSecond;
    private float spikeRate;

    private PlatformDestroyer[] spikeList;

    // Use this for initialization
    void Start () {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theGameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (powerupActive)
        {
            powerupLenghtCounter -= Time.deltaTime;

            if (theGameManager.powerupReset)
            {
                powerupLenghtCounter = 0;
                theGameManager.powerupReset = false;
            }

            if (doublePoints)
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond * 2.75f;
                theScoreManager.shouldDouble = true;
            }
            if (safeMode)
            {
                thePlatformGenerator.randomSpikeThreshold = 0f;
            }

            if (powerupLenghtCounter <= 0)
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond;
                thePlatformGenerator.randomSpikeThreshold = spikeRate;
                theScoreManager.shouldDouble = false;
                powerupActive = false;
            }
        }
	}
    public void ActivatePowerup(bool points, bool safe, float time)
    {
        doublePoints = points;
        safeMode = safe;
        powerupLenghtCounter = time;

        normalPointsPerSecond = theScoreManager.pointsPerSecond;
        spikeRate = thePlatformGenerator.randomSpikeThreshold;

        if (safeMode)
        {
            spikeList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains ("spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }

        powerupActive = true;
    }
}
