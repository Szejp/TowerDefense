using AFSInterview.Helpers;
using UnityEngine;

namespace AFSInterview.GameElements
{
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] bool refreshTargetPositon;
        
        protected Transform target;
        protected Vector3 positionToShootAt;

        protected Vector3 TargetPosition => target && refreshTargetPositon ? target.position : positionToShootAt;

        public bool RefreshTargetPositon => refreshTargetPositon;

        public virtual void Initialize(Transform target)
        {
            this.target = target;
        }

        public virtual void Initialize(Vector3 position)
        {
            positionToShootAt = position;
        }        

        protected void OnCollisionEnter(Collision other)
        {
            if (other.collider.gameObject.layer.Equals(Layers.Ground.Id))
                Destroy(gameObject);
            
            if(other.collider.gameObject.layer.Equals(Layers.Enemy.Id))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}