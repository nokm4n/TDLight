/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using System;
using System.Collections;
using System.Globalization;
using SimpleJSON;

namespace NiobiumStudios
{
    /**
     * Cloud Time configuration
     **/
    [Serializable]
    public class CloudClock
    {
        public string name; // Name representation. Not really used
        public string url = "https://worldtimeapi.org/api/timezone/EST";  // The Server Clock JSON API
        public string timeFormat = "yyyy-MM-dd'T'HH:mm:ss.ffffffzzz";  // Server Clock Time Format
    }
}