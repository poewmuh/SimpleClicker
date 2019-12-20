using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleClicker.Events;

namespace SimpleClicker.Extensions
{
    public class HTTPLoader : MonoBehaviour
    {
        public IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    Leader leaderboards = new Leader();
                    leaderboards = JsonUtility.FromJson<Leader>(webRequest.downloadHandler.text);
                    EventHandler.jsonDownloaded.Invoke(leaderboards);
                }
            }

            
        }
    }
}
