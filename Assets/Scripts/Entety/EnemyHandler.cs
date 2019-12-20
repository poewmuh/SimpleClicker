using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleClicker.Events;
using UnityEngine.UI;

namespace SimpleClicker.Entety
{
    public class EnemyHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> enemyPool;
        [SerializeField] private int hpIncreaseRatio;
        [SerializeField] private ClickHandler clicker;
        [SerializeField] private Text curLevelText;
        private int _currEnemyLevel;
        private Enemy _curEnemy;

        private void Awake()
        {
            _currEnemyLevel = 1;
            curLevelText.text = _currEnemyLevel.ToString();
            ActivateEnemy();
            EventHandler.enemyDead.AddListener(SpawnNewEnemy);
        }

        private void ActivateEnemy()
        {
            int rand = Random.Range(0, enemyPool.Count);
            enemyPool[rand].SetActive(true);
            _curEnemy = enemyPool[rand].GetComponent<Enemy>();
            _curEnemy.Init(_currEnemyLevel, hpIncreaseRatio);
            clicker.SetTarget(_curEnemy);
        }

        private void SpawnNewEnemy()
        {
            _currEnemyLevel++;
            curLevelText.text = _currEnemyLevel.ToString();
            ActivateEnemy();
        }
    }
}
