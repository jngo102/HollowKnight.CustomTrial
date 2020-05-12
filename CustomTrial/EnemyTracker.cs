using UnityEngine;

namespace CustomTrial
{
    public class EnemyTracker : MonoBehaviour
    {
        private HealthManager _hm;

        private void Awake()
        {
            _hm = GetComponent<HealthManager>();
            _hm.OnDeath += OnDeath;
        }

        private void Start()
        {
            GetComponent<PlayMakerFSM>().SetState("Init");
        }

        private void OnDeath()
        {
            ColosseumManager.EnemyCount--;
        }
    }
}