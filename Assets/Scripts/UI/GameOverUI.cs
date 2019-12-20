using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleClicker.Extensions;
using System.Net.Http;
using UnityEngine.Networking;
using SimpleClicker.Events;
using GoogleMobileAds.Api;
using SimpleClicker.Task;

namespace SimpleClicker.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private GameObject clickButton;
        [SerializeField] private GameObject gameOverUIobj, winObj, loseObj, scrollContent;
        [SerializeField] private Text bestText, curRecText, gameOverText;
        [SerializeField] private GameObject leaderPrefab;
        [SerializeField] private TaskManager tasker;


        private string jsonurl = "https://api.myjson.com/bins/1amu7o";
        private RewardBasedVideoAd rewardBasedVideo;
        private bool adLoaded = true;

        private void Start()
        {
            // Реклама в этом скрипте потомучто это тестовый проект
            string appId = "ca-app-pub-8031020776871928~6802399658";
            MobileAds.Initialize(appId);
            rewardBasedVideo = RewardBasedVideoAd.Instance;
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            RequestRewardBasedVideo();
        }

        public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
           
            adLoaded = false;
        }

        public void HandleRewardBasedVideoLoaded(object sender, System.EventArgs args)
        {
            Debug.Log("Loaded");
            MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
        }

        public void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
        {
            RequestRewardBasedVideo();
        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            print("User rewarded with: " + amount.ToString() + " " + type);
            
        }

        private void RewardAction()
        {
            loseObj.SetActive(false);
            gameOverUIobj.SetActive(false);
            clickButton.SetActive(true);
            tasker.TimerStart(true);
        }

        private void RequestRewardBasedVideo()
        {
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
            AdRequest request = new AdRequest.Builder().Build();
            rewardBasedVideo.LoadAd(request, adUnitId);
        }

        public void GameOver(bool itsWin, float record)
        {
            gameOverUIobj.SetActive(true);
            clickButton.SetActive(false);
            if (itsWin)
                WinCondition(record);
            else
                LoseCondition();
        }

        public void OnRestartAction()
        {
            SceneManager.LoadScene(0);
        }

        public void OnADWatchAction()
        {
#if UNITY_EDITOR
            RewardAction();
#endif
            if (adLoaded == false)
            {
                Debug.Log("Check the internet");
                return;
            }
            if (rewardBasedVideo.IsLoaded())
            {
                rewardBasedVideo.Show();
            }
        }

        private void WinCondition(float record)
        {
            gameOverText.text = "YOU WIN";
            winObj.SetActive(true);
            curRecText.text = record.ToString("##.#");
            float oldBest = PP.GetFloat("Record", 0f);
            if (oldBest < record)
                PP.SetFloat("Record", record);
            bestText.text = PP.GetFloat("Record", 0f).ToString("##.#");
            BeforeFillLeaderboards();
        }

        private void LoseCondition()
        {
            gameOverText.text = "YOU LOSE";
            loseObj.SetActive(true);
        }

        private void BeforeFillLeaderboards()
        {
            HTTPLoader loader = new HTTPLoader();
            EventHandler.jsonDownloaded.AddListener(FillLeaderboards);
            StartCoroutine(loader.GetRequest(jsonurl));

        }

        private void FillLeaderboards(Leader leaderboards)
        {
            foreach (LeaderInside leader in leaderboards.players)
            {
                GameObject leaderObj = Instantiate(leaderPrefab, scrollContent.transform);
                LeaderPlayer ListCode = leaderObj.GetComponent<LeaderPlayer>();
                ListCode.SetValues(leader.id, leader.name, leader.best_time);
            }
        }
    }
}
