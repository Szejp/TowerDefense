using AFSInterview.Helpers;
using AFSInterview.System.Spawning;
using UnityEngine;

namespace AFSInterview.System
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] Spawner spawner;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
                if (TryGetBuildPosition(out Vector3 spawnPosition))
                    spawner.SpawnTower(spawnPosition, TowerType.Simple);
            if (Input.GetMouseButtonDown(1))
                if (TryGetBuildPosition(out Vector3 spawnPosition))
                    spawner.SpawnTower(spawnPosition, TowerType.Other);
        }

        bool TryGetBuildPosition(out Vector3 position)
        {
            position = Vector3.zero;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var result = Physics.Raycast(ray, out var hit, LayerMask.GetMask(Layers.Ground.Name));

            if (result)
                position = hit.point;

            return result;
        }
    }
}