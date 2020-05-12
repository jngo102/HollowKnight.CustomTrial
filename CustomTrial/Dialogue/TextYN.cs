using System.Collections;
using CustomTrial.Utilities;
using UnityEngine;

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
            
            _pageCtrl.InsertMethod("Yes", 0, () => PlaceMark = true);
            _pageCtrl.InsertMethod("No", 0, () => PlaceMark = false);
        }
    }
}