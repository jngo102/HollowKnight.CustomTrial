using System.Collections;
using UnityEngine;
using Vasi;

namespace CustomTrial.Dialogue
{
    public class TextYN : MonoBehaviour
    {
        private PlayMakerFSM _pageCtrl;

        public static bool PlaceMark;
        
        private void Awake()
        {
            _pageCtrl = gameObject.LocateMyFSM("Dialogue Page Control");
        }

        private IEnumerator Start()
        {
            yield return new WaitWhile(() => HeroController.instance == null);
            
            _pageCtrl.GetState("Yes").InsertMethod(0, () => PlaceMark = true);
            _pageCtrl.GetState("No").InsertMethod(0, () => PlaceMark = false);
        }
    }
}