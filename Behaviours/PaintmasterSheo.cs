using Modding;
using System.Collections;
using UnityEngine;
using Vasi;

namespace CustomTrial.Behaviours
{
    internal class PaintmasterSheo : MonoBehaviour
    {
        private PlayMakerFSM _sheo;

        private void Awake()
        {
            _sheo = gameObject.LocateMyFSM("nailmaster_sheo");

            var corpse = ReflectionHelper.GetField<EnemyDeathEffects, GameObject>(GetComponent<EnemyDeathEffectsUninfected>(), "corpse");
            corpse.AddComponent<SheoCorpse>();
        }

        private IEnumerator Start()
        {
            _sheo.SetState("Init");

            yield return new WaitWhile(() => _sheo.ActiveStateName != "Painting");

            _sheo.SetState("Battle Start");
        }
    }

    internal class SheoCorpse : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.LocateMyFSM("Control").GetState("Bow").AddCoroutine(TweenOut);
        }

        private IEnumerator TweenOut()
        {
            yield return new WaitForSeconds(5);

            GetComponent<BoxCollider2D>().isTrigger = true;
            yield return new WaitUntil(() =>
            {
                transform.Translate(Vector3.down * 25 * Time.deltaTime);
                return transform.position.y <= ArenaInfo.BottomY - 10;
            });

            Destroy(gameObject);
        }
    }
}