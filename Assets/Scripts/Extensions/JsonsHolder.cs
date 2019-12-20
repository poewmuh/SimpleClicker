using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SimpleClicker.Extensions
{
    [System.Serializable]
    public class LeaderInside
    {
        public int id;
        public string name;
        public float best_time;
    }

    [System.Serializable]
    public class Leader
    {
        public List<LeaderInside> players;
    }
}
