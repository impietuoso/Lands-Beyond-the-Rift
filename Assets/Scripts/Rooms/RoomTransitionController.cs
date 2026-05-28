using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Rooms {
    public class RoomTransitionController : MonoBehaviour {
        private static readonly int TimeID = Shader.PropertyToID("_time");
        private static readonly int DistanceID = Shader.PropertyToID("_distance");
        private static readonly int OriginID = Shader.PropertyToID("_origin");

        [SerializeField] private TilemapRoom[] rooms;
        [SerializeField] private TilemapRenderer fogPrefab;
        [SerializeField] private Tile doorTile;
        [SerializeField] private Tile fogTile;
        [SerializeField] private Material fogMat1;
        [SerializeField] private Material fogMat2;
        [SerializeField] private Material dissolveMat1;
        [SerializeField] private Material dissolveMat2;
        [SerializeField] private Material radialMat1;
        [SerializeField] private Material radialMat2;
        [SerializeField] private float duration = 2f;
        [SerializeField, Range(0, .2f)] private float dissolveDelay = .1f;
        [SerializeField, Range(0, .2f)] private float radialDelay = .1f;
        [SerializeField] private AnimationCurve curve;
        private Coroutine _routine;

        public float Duration => duration;

        private void Awake() {
            dissolveMat1 = new Material(dissolveMat1);
            dissolveMat2 = new Material(dissolveMat2);
            radialMat1 = new Material(radialMat1);
            radialMat2 = new Material(radialMat2);
        }

        private IEnumerator Start() {
            if(rooms.Length == 0) yield break;

            foreach (var room in rooms)
                Game.LoadTasks.Add(CreateFogLayer(room));
            yield return Game.LoadTasks.WaitAll();

            var startingRoom = rooms[0];
            startingRoom.Entities.SetActive(true);
            startingRoom.Collider.enabled = true;
            startingRoom.Fog1.material = radialMat1;
            startingRoom.Fog2.material = radialMat2;

            SetDissolve(1);
            SetRadial(1, 100);
        }

        public void SetDissolve(float time) {
            var time1 = curve.Evaluate(time - dissolveDelay);
            var time2 = curve.Evaluate(time);
            dissolveMat1.SetFloat(TimeID, time1);
            dissolveMat2.SetFloat(TimeID, time2);
        }

        public void SetRadial(float time, float distance) {
            var time1 = curve.Evaluate(time - radialDelay);
            var time2 = curve.Evaluate(time);
            radialMat1.SetFloat(DistanceID, time1 * distance);
            radialMat2.SetFloat(DistanceID, time2 * distance);
        }

        public void SetOrigin(Vector2 position) {
            radialMat1.SetVector(OriginID, position);
            radialMat2.SetVector(OriginID, position);
        }

        public Coroutine PlayTransition(TilemapRoom from, TilemapRoom to, Vector2 origin) {
            if(_routine != null) return null;
            return _routine = StartCoroutine(Transition(from, to, origin));
        }

        private IEnumerator Transition(TilemapRoom from, TilemapRoom to, Vector2 origin) {
            var bounds = to.Fog1.bounds.size;
            var maxDist = Math.Max(bounds.x, bounds.y) + 2;

            SetOrigin(origin);
            SetRadial(0, maxDist);
            SetDissolve(1);

            from.Fog1.gameObject.SetActive(true);
            from.Fog2.gameObject.SetActive(true);
            from.Fog1.material = dissolveMat1;
            from.Fog2.material = dissolveMat2;
            to.Fog1.material = radialMat1;
            to.Fog2.material = radialMat2;
            to.Entities.SetActive(true);

            for (var i = 0f; i < 1; i += Time.unscaledDeltaTime / duration) {
                SetRadial(i, maxDist);
                SetDissolve(1 - i);
                yield return null;
            }

            SetRadial(1, maxDist);
            SetDissolve(0);

            from.Fog1.material = fogMat1;
            from.Fog2.material = fogMat2;
            from.Entities.SetActive(false);

            _routine = null;
        }

        private IEnumerator<TilemapRenderer> CreateFogLayer(TilemapRoom room) {
            var source = room.MinimapBuilder.tilemap;
            var rend = Instantiate(fogPrefab, room.transform, true);
            var map = rend.GetComponent<Tilemap>();
            rend.name = "Fog 1";
            rend.material = fogMat1;

            var count = 0;
            foreach (var pos in source.cellBounds.allPositionsWithin) {
                var t = source.GetTile(pos);

                if(t && t != doorTile) {
                    map.SetTile(pos, fogTile);
                    count++;
                }

                if(count % Game.TileBuildSpeed == 0) yield return null;
            }
            room.SetFog(rend, fogMat2);
        }
    }
}