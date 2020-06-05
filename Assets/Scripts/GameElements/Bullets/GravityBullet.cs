using UnityEngine;

namespace AFSInterview.GameElements
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityBullet : Bullet
    {
        [SerializeField] float flyTime = 1f;
        [SerializeField] float heightFactor = 2f;

        Rigidbody rb;

        public override void Initialize(Vector3 positon)
        {
            base.Initialize(positon);
            Vector3 horizontalDistance = new Vector3(TargetPosition.x - transform.position.x, 0,
                TargetPosition.z - transform.position.z);
            float verticalDistance = -transform.position.y + 2 * heightFactor;
            rb.velocity = new Vector3(horizontalDistance.x / flyTime, verticalDistance / flyTime,
                horizontalDistance.z / flyTime);
        }

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, TargetPosition);
        }
    }
}