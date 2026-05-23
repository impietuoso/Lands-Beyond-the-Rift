using System;
using System.Collections;
using UnityEngine;

namespace Rooms {
    public class RoomDoorway : MonoBehaviour
    {
        [SerializeField] private RoomTransitionController transition;
        [SerializeField] private TilemapRoom roomA;
        [SerializeField] private TilemapRoom roomB;
        [SerializeField] private float duration = .75f;
        [SerializeField] private float advance = 1.7f;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.GetComponent<PlayerController>()) return;
            var (a, b) = roomA.Entities.activeSelf ? (roomA, roomB) : (roomB, roomA);
            var pos = other.transform.position;
            var dir = b.Entities.transform.position - a.Entities.transform.position;
            if (Math.Abs(dir.x) > Math.Abs(dir.y)) dir.y = 0;
            if (Math.Abs(dir.y) > Math.Abs(dir.x)) dir.x = 0;

            var landing = pos + dir.normalized * advance;
            var ie = transition.PlayTransition(a, b, landing);
            var move = MovePlayer(other.transform, a, b, landing, ie);
            transition.StartCoroutine(move);
        }

        private IEnumerator MovePlayer(Transform player, TilemapRoom a, TilemapRoom b, Vector3 tgtPos, Coroutine ie)
        {
            gameObject.SetActive(false);
            var posA = player.position;
            Time.timeScale = 0;
            a.Collider.enabled = false;

            for (var i = 0f; i < 1; i += Time.unscaledDeltaTime / duration)
            {
                player.position = Vector3.Lerp(posA, tgtPos, i);
                yield return null;
            }

            Time.timeScale = 1;
            b.Collider.enabled = true;

            yield return ie;
            gameObject.SetActive(true);
        }
    }
}