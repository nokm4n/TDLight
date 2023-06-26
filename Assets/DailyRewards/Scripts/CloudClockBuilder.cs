/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using SimpleJSON;

namespace NiobiumStudios
{
    /**
     * Representation of the world clock JSON Object as shown at http://worldclockapi.com/
     **/
    [Serializable]
    public class CloudClockResult
    {
        public string datetime;
        public string currentDateTime;
        public string utcOffset;
        public string dayOfTheWeek;
        public string timeZoneName;
        public double currentFileTime;
        public string ordinalDate;
        public string serviceResponse;
    }

    /**
    * Handles the Cloud Clock global instance
    **/
    public static class CloudClockBuilder
    {
        public static string errorMessage = String.Empty;              // Failed error message
        public static DateTime cloudClockDate;                        // Global DateTime
        public static State currentState;                              // Time Server current status

        private static int connectionRetries;                          // Retries counter
        private static int serverClockIndex;                           // Index of the working Time Server

        // Cloud Clock possible states
        public enum State
        {
            NotInitialized,
            Initializing,
            Initialized,
            FailedToInitialize
        };

        public static IEnumerator InitializeCloudClock(List<CloudClock> cloudClockList, int maxRetries)
        {
            currentState = State.Initializing;

            // Try to connect to multiple Time Server configurations. If succeded breaks the loop
            foreach (var cloudClock in cloudClockList)
            {
                if (currentState == State.Initialized)
                    yield break;

                connectionRetries = 0;

                string result = String.Empty;
                while (connectionRetries < maxRetries)
                {
                    WWW www = new WWW(cloudClock.url);

                    while (!www.isDone)
                        yield return null;

                    if (!string.IsNullOrEmpty(www.error))
                    {
                        connectionRetries++;
                        Debug.LogWarning("Error Loading Cloud Clock " + cloudClock.url + ". Retrying connection " + connectionRetries);
                        errorMessage = www.error;
                    }
                    else
                    {
                        result = www.text;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(result))
                {
                    var clockJson = result;

                    // Loads the Cloud Clock from JSON
#if UNITY_5_3_OR_NEWER
                    var cloudClockResultFromJson = JsonUtility.FromJson<CloudClockResult>(clockJson);
#else
                    var cloudClockResultJson = JSON.Parse(clockJson);

                    var cloudClockResultFromJson = new CloudClockResult();

                    if (cloudClockResultJson["currentDateTime"] != null)
                        cloudClockResultFromJson.currentDateTime = cloudClockResultJson["currentDateTime"].Value;
                    else if (cloudClockResultJson["datetime"] != null)
                        cloudClockResultFromJson.datetime = cloudClockResultJson["datetime"].Value;
#endif
                    // save the working connection result
                    var cloudClockResult = cloudClockResultFromJson;

                    var dateTimeStr = cloudClockResultFromJson.currentDateTime;
                    if (string.IsNullOrEmpty(cloudClockResultFromJson.currentDateTime))
                        dateTimeStr = cloudClockResultFromJson.datetime;

                    cloudClockDate = DateTime.ParseExact(dateTimeStr, cloudClock.timeFormat, CultureInfo.InvariantCulture);

                    // Most time servers don't count the seconds. So we pick the seconds from the local machine
                    cloudClockDate = cloudClockDate.AddSeconds(DateTime.Now.Second);

                    var time = string.Format("{0:D4}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}", cloudClockDate.Year, cloudClockDate.Month, cloudClockDate.Day, cloudClockDate.Hour, cloudClockDate.Minute, cloudClockDate.Second);

                    Debug.Log("Time Cloud Clock: " + time);
                    currentState = State.Initialized;
                }
                else
                {
                    Debug.LogError("Error Loading Cloud Clock: " + cloudClock.url + " Error:" + errorMessage);
                    currentState = State.FailedToInitialize;
                }
            }
        }
    }
}