using UnityEngine;

namespace AFSInterview.GameElements
{
    public class HomingBullet : Bullet
    {
        [SerializeField] float speed;

        private Vector3 direction;

        protected void Update()
        {
            if (target != null)
                direction = (TargetPosition - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
        }
    }
}