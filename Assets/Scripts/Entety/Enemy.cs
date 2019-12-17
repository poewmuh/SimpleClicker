using UnityEngine;
using SimpleClicker.Events;
using System.Collections;
using UnityEngine.UI;

namespace SimpleClicker.Entety
{
    public class Enemy : MonoBehaviour
    {
        private int _hp;
        private int _maxHp;
        private bool _iDead => _hp <= 0;
        private Animator animator;
        [SerializeField] private Slider healthBar;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void Init(int level, int ratio)
        {
            _maxHp = 100 + (level * ratio);
            _hp = _maxHp;
            healthBar.value = 1f;
        }

        protected IEnumerator DeathRattle()
        {
            animator.SetTrigger("OnDeath");
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
            EventHandler.enemyDead.Invoke();

        }

        public void GetDamage(int amount)
        {
            if (_iDead)
                return;
            animator.SetTrigger("OnDamage");
            _hp -= amount;
            healthBar.value = (float)_hp / _maxHp;
            if (_iDead)
            {
                StartCoroutine(DeathRattle());
            }
        }
    }
}