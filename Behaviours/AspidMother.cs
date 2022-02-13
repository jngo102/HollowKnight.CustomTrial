using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class AspidMother : MonoBehaviour
    {
        private GameObject _hatcherCage;

        private void Awake()
        {
            _hatcherCage = Instantiate(CustomTrial.GameObjects["aspidhatchling"]);
            _hatcherCage.SetActive(true);

            GetComponent<HealthManager>().OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            Destroy(_hatcherCage);
        }
    }
}