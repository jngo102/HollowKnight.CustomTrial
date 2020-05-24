using System.Collections;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class Oblobble : MonoBehaviour
    {
        private PlayMakerFSM _attack;
        private PlayMakerFSM _bounce;

        private void Awake()
        {
            _attack = gameObject.LocateMyFSM("Fatty Fly Attack");
            _bounce = gameObject.LocateMyFSM("fat fly bounce");
        }    
        
        private IEnumerator Start()
        {
            Modding.Logger.Log("Setting Init");
            _attack.SetState("Init");
            Modding.Logger.Log("Setting Initialise");
            _bounce.SetState("Initialise");
            
            Modding.Logger.Log("yield");
            yield return new WaitWhile(() => _bounce.ActiveStateName != "Swoop In");
            
            Modding.Logger.Log("Activate");
            _bounce.SetState("Activate");
        }
    }
}