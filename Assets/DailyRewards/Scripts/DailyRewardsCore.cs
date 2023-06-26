/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using System;
using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace NiobiumStudios
{
    /**
     * Daily Rewards common methods
     **/
    public abstract class DailyRewardsCore<T> : MonoBehaviour where T : DailyRewardsCore<T>
    {
        public int instanceId;
        public bool isSingleton = true;                         // Checks if should be used as singleton

        public List<CloudClock> cloudClockList;                 // List of the clock servers in order of priority

        public bool useCloudClock = true;                       // Use Server Clock?

        public string errorMessage;                             // Error Message
        public bool isErrorConnect;                             // Some error happened on connecting?
        public DateTime now;                                    // The actual date. Either returned by the using the cloud server or the player device clock

        public int maxRetries = 3;                              // The maximum number of retries for each Time Server connection

        public delegate void OnInitialize(bool error = false, string errorMessage = ""); // When the timer initializes. Sends an error message in case it happens. Should wait for this delegate if using Time Server API
        public OnInitialize onInitialize;

        protected bool isInitialized = false;

        // Initializes the current DateTime. If the player is using the Time Server initializes it
        public IEnumerator InitializeDate()
        {
            if (useCloudClock)
            {
                if (CloudClockBuilder.currentState == CloudClockBuilder.State.NotInitialized)
                {
                    // Time Server is not initialized, we initialize it
                    yield return StartCoroutine(CloudClockBuilder.InitializeCloudClock(cloudClockList, maxRetries));
                }
                else if (CloudClockBuilder.currentState == CloudClockBuilder.State.Initializing)
                {
                    // Time Server is being initialized by another script, we wait until it finishes
                    while (CloudClockBuilder.currentState == CloudClockBuilder.State.Initializing)
                        yield return null;
                }

                if (CloudClockBuilder.currentState == CloudClockBuilder.State.Initialized)
                {
                    now = CloudClockBuilder.cloudClockDate;
                    isInitialized = true;
                }
                else if (CloudClockBuilder.currentState == CloudClockBuilder.State.FailedToInitialize)
                {
                    isErrorConnect = true;
                    errorMessage = CloudClockBuilder.errorMessage;
                }
            }
            else
            {
                now = DateTime.Now;
                isInitialized = true;
            }
        }

        public void RefreshTime()
        {
            if (useCloudClock)
                now = CloudClockBuilder.cloudClockDate;
            else
                now = DateTime.Now;
        }

        public static T GetInstance(int id = 0)
        {
            var instances = FindObjectsOfType<T>();
            for (int i = 0; i < instances.Length; i++)
            {
                if ((instances[i]).instanceId == id)
                    return instances[i];
            }
            return null;
        }

        //Updates the current time
        public virtual void TickTime()
        {
            if (!isInitialized)
                return;

            now = now.AddSeconds(Time.unscaledDeltaTime);

            if (useCloudClock)
                CloudClockBuilder.cloudClockDate = now;
        }

        public string GetFormattedTime(TimeSpan span)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);
        }

        protected virtual void Awake()
        {
            if (isSingleton)
            {
                DontDestroyOnLoad(this.gameObject);

                var instanceCount = GetInstanceCount();

                if (instanceCount > 1)
                    Destroy(gameObject);
            }
        }

        // Returns the amount of instances with the same id
        private int GetInstanceCount()
        {
            var instances = FindObjectsOfType<T>();
            var count = 0;
            for (int i = 0; i < instances.Length; i++)
            {
                if ((instances[i]).instanceId == instanceId)
                    count++;
            }
            return count;
        }

        protected virtual void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
                RefreshTime();
        }
    }
}