using UnityEngine;
using SimpleClicker.Entety;

namespace SimpleClicker
{
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField] private int clickDamage;
        private Enemy _curEnemy;

        public void SetTarget(Enemy target)
        {
            _curEnemy = target;
        }

        public void OnTapScreen()
        {
            if (_curEnemy == null)
                Debug.Log("Enemy не найден!");
            _curEnemy.GetDamage(clickDamage);
        }

        
    }
}
