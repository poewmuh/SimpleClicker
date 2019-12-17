using System.Collections;
using SimpleClicker.Events;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleClicker.Task
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private Text timeText, questText;
        [SerializeField] private int maxKillsCount;
        [SerializeField] private float timeCount;

        private int curKillsCount;
        private Coroutine curCoroutine;

        private void Start()
        {
            timeText.text = timeCount.ToString();
            questText.text = "0/" + maxKillsCount.ToString();

            curKillsCount = 0;
            
            EventHandler.enemyDead.AddListener(TaskFill);

            TimerStart();
        }

        public void TimerStart()
        {
            curCoroutine = StartCoroutine(TimerCoroutine());
        }

        public IEnumerator TimerCoroutine(bool fromAd = false)
        {
            float timer = 0;
            if (fromAd == true)
                timeCount = 30;
            while (timer <= timeCount)
            {
                timer += Time.deltaTime;
                timeText.text = (timeCount - (int)timer).ToString();
                yield return null;
            }
            //TODO: SHOW LOSE WINDOW
            Debug.Log("YOU LOSE");
        }

        private void TaskFill()
        {
            curKillsCount++;
            questText.text = curKillsCount.ToString() + '/' + maxKillsCount.ToString();
            if (curKillsCount >= maxKillsCount)
            {
                //TODO: SHOW WIN WINDOW
                Debug.Log("YOU WIN");
                StopCoroutine(curCoroutine);
            }
        }
    }
}
