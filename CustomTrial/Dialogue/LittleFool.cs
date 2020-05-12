using System.Collections;
using CustomTrial.Utilities;
using HutongGames.PlayMaker.Actions;
using ModCommon;
using UnityEngine;

namespace CustomTrial.Dialogue
{
    public class LittleFool : MonoBehaviour
    {
        private PlayMakerFSM _convo;
        private PlayMakerFSM _npcCtrl;
        
        private void Awake()
        {
            _convo = gameObject.LocateMyFSM("Conversation Control");
            _npcCtrl = gameObject.LocateMyFSM("npc_control");
        }

        private IEnumerator Start()
        {
            yield return new WaitWhile(() => HeroController.instance == null);
            
            _convo.RemoveAction<SendEventByName>("Convo Choice");
            _convo.InsertCoroutine("Convo Choice", 3, StartCustomTrial);
        }

        private IEnumerator StartCustomTrial()
        {
            GameObject gameCameras = GameCameras.instance.gameObject;
            GameObject dialogueManager = gameCameras.FindGameObjectInChildren("DialogueManager");
            PlayMakerFSM boxOpen = dialogueManager.LocateMyFSM("Box Open");
            PlayMakerFSM boxOpenYN = dialogueManager.LocateMyFSM("Box Open YN");
            GameObject text = dialogueManager.FindGameObjectInChildren("Text");
            GameObject textYN = dialogueManager.FindGameObjectInChildren("Text YN");
            PlayMakerFSM dialogPageCtrl = textYN.LocateMyFSM("Dialogue Page Control");
            text.PrintSceneHierarchyTree();
            var dialogueBox = text.GetComponent<DialogueBox>();
            dialogueBox.StartConversation("CUSTOM_TRIAL_DIALOGUE", "");

            yield return new WaitWhile(() => dialogueBox.currentPage <= 1);

            boxOpen.SendEvent("BOX DOWN");
            text.SetActive(false);

            boxOpenYN.SendEvent("BOX UP YN");

            dialogueBox = textYN.GetComponent<DialogueBox>();
            dialogueBox.StartConversation("START_CUSTOM_TRIAL", "");

            Log("WaitWhile Idle");
            yield return new WaitWhile(() =>
            {
                Log("Dialogue Page Control State: " + dialogPageCtrl.ActiveStateName);
                return dialogPageCtrl.ActiveStateName == "Idle";
            });
            
            Log("WaitWhile Input");
            yield return new WaitWhile(() =>
            {
                Log("Dialogue Page Control State: " + dialogPageCtrl.ActiveStateName);
                return dialogPageCtrl.ActiveStateName == "Ready for Input";
            });

            Log("Take Geo");
            boxOpenYN.SendEvent("BOX DOWN YN");

            _convo.SetState("Talk Finish");

            if (TextYN.PlaceMark)
            {
                GameObject.Find("Gold Trial Board").LocateMyFSM("Conversation Control").SetState("Mark");
            }

            _npcCtrl.SendEvent("CONVO END");
            _convo.SendEvent("RESET CONVO");
            _convo.SetState("Idle");
            
            PlayMakerFSM gateCtrl = GameObject.Find("Colosseum Gate").LocateMyFSM("Control");
            PlayMakerFSM hatchClose = GameObject.Find("Colosseum Hatch").LocateMyFSM("Close");
            gateCtrl.SendEvent("GATE OPEN");
            hatchClose.SendEvent("GATE OPEN");
            _convo.Fsm.GetFsmBool("Gate Open").Value = true;

            Log("Finished.");
        }

        private void Log(object message) => Modding.Logger.Log("[Little Fool] " + message);
    }
}