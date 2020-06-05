using AFSInterview.GameElements;
using UnityEngine;

namespace AFSInterview.Shooting
{
    [CreateAssetMenu(fileName = "ShootingSystemConfig", menuName = "Game/Shooting/ShootingSystemConfig")]
    public class ShootingSystemConfig : ScriptableObject
    {
        public Bullet bulletToShoot;
        public int bulletsCount = 1;
        public float fireRate = 1;
        public float burstDelay = 0;
        public float angle = 0;
    }
}