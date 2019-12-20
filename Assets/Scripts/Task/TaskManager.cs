using System.Collections;
using SimpleClicker.Events;
using UnityEngine;
using UnityEngine.UI;
using SimpleClicker.UI;

namespace SimpleClicker.Task
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private Text timeText, questText;
        [SerializeField] private int maxKillsCount;
        [SerializeField] private float timeCount;
        [SerializeField] private GameOverUI gameoverUI;

        private int curKillsCount;
        private float timer;
        private Coroutine curCoroutine;

        private void Start()
        {
            timeText.text = timeCount.ToString();
            questText.text = "0/" + maxKillsCount.ToString();

            curKillsCount = 0;
            timer = 0;
            EventHandler.enemyDead.AddListener(TaskFill);

            TimerStart();
        }

        public void TimerStart(bool fromAD = false)
        {
            curCoroutine = StartCoroutine(TimerCoroutine(fromAD));
        }

        private IEnumerator TimerCoroutine(bool fromAd)
        {
            if (fromAd == true)
                timeCount = 30;
            float timerCorutine = 0;
            while (timerCorutine <= timeCount)
            {
                timerCorutine += Time.deltaTime;
                timeText.text = (timeCount - timerCorutine).ToString("##");
                yield return null;
            }
            timer += timerCorutine;
            gameoverUI.GameOver(false, timer);
            Debug.Log("YOU LOSE");
        }

        private void TaskFill()
        {
            curKillsCount++;
            questText.text = curKillsCount.ToString() + '/' + maxKillsCount.ToString();
            if (curKillsCount >= maxKillsCount)
            {
                gameoverUI.GameOver(true, timer);
                Debug.Log("YOU WIN");
                StopCoroutine(curCoroutine);
            }
        }
    }
}
