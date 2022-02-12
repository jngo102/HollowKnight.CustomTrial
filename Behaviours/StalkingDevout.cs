using System.Collections;
using UnityEngine;
    
namespace CustomTrial.Behaviours
{
    internal class StalkingDevout : MonoBehaviour
    {
        private PlayMakerFSM _spider;

        private void Awake()
        {
            _spider = gameObject.LocateMyFSM("Slash Spider");
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => _spider.ActiveStateName == "Rest");

            _spider.SendEvent("WAKE");

            float sign = Mathf.Sign(HeroController.instance.transform.position.x - transform.position.x);
            transform.SetScaleX(sign);

            _spider.Fsm.GetFsmBool("Facing Right").Value = sign > 0;
            _spider.Fsm.GetFsmFloat("Slash Speed").Value = 10 * sign;
            _spider.Fsm.GetFsmFloat("Walk Speed").Value = 8 * sign;
        }
    }
}
