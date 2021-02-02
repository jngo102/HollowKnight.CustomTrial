using System.Collections;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

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
            
            _convo.GetState("Convo Choice").RemoveAction<SendEventByName>();
            _convo.GetState("Convo Choice").InsertCoroutine(3, StartCustomTrial);
        }

        private IEnumerator StartCustomTrial()
        {
            GameObject gameCameras = GameCameras.instance.gameObject;
            GameObject dialogueManager = gameCameras.transform.Find("DialogueManager").gameObject;
            PlayMakerFSM boxOpen = dialogueManager.LocateMyFSM("Box Open");
            PlayMakerFSM boxOpenYN = dialogueManager.LocateMyFSM("Box Open YN");
            GameObject text = dialogueManager.transform.Find("Text").gameObject;
            GameObject textYN = dialogueManager.transform.Find("Text YN").gameObject;
            PlayMakerFSM dialogPageCtrl = textYN.LocateMyFSM("Dialogue Page Control");
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