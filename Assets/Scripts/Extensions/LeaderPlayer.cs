using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleClicker.Extensions
{
    public class LeaderPlayer : MonoBehaviour
    {
        [SerializeField] private Text playerPlace, playerName, playerRecord;

        public void SetValues(int place, string name, float record)
        {
            playerPlace.text = place.ToString();
            playerName.text = name;
            playerRecord.text = record.ToString("##.#");
        }
    }
}
