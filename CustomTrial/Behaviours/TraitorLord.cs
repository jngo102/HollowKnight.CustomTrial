using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ModCommon;
using UnityEngine;

namespace CustomTrial.Behaviours
{
    public class TraitorLord : MonoBehaviour
    {
        private const float GroundY = 6.0f;

        private PlayMakerFSM _mantis;
        
        private void Awake()
        {
            _mantis = gameObject.LocateMyFSM("Mantis");
        }

        private IEnumerator Start()
        {
            _mantis.SetState("Init");
            
            yield return new WaitWhile(() => _mantis.ActiveStateName != "Emerge Dust");

            GetComponent<HealthManager>().hasSpecialDeath = false;
            
            gameObject.FindGameObjectInChildren("Emerge Dust").GetComponent<ParticleSystem>().Stop();
            GetComponent<MeshRenderer>().enabled = true;
            GameCameras.instance.cameraShakeFSM.Fsm.GetFsmBool("RumblingMed").Value = false;
            
            _mantis.SetState("Idle");
            _mantis.GetAction<FloatCompare>("DSlash").float2 = GroundY;
            _mantis.GetAction<SetPosition>("Land").y = GroundY;
        }
    }
}