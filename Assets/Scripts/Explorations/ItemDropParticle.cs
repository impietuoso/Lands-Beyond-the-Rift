using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Explorations {
    public class ItemDropParticle : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Collider2D collider2d;
        [SerializeField] private float duration = 1f;
        [SerializeField] private float duration2 = .25f;
        [SerializeField] private float popDistance = 0.5f;
        [SerializeField] private float bounceHeight = 0.5f;

        private static ComponentPool<ItemDropParticle> _pool;

        public void PopCopy(Vector3 position, Sprite spr, int amount)
        {
            _pool ??= new(this);
        
            for (var i = 0; i < amount; i++)
            {
                var clone = _pool.Recycle();
                clone.sprite.sprite = spr;
                clone.transform.position = position;
                clone.StartCoroutine(clone.PopAnimation());
            }
        }

        private IEnumerator PopAnimation()
        {
            collider2d.enabled = false;

            var startPos = transform.position;
            var randomDir = Random.insideUnitCircle.normalized * popDistance;
            var targetPos = startPos + (Vector3)randomDir;
            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var percent = elapsed / duration;
                var currentPos = Vector3.Lerp(startPos, targetPos, percent);
                var bounce = Mathf.Sin(percent * Mathf.PI) * bounceHeight;
                currentPos.y += bounce;
                transform.position = currentPos;
                yield return null;
            }

            // Second bounce
            elapsed = 0f;
            var secondBounceDuration = duration * duration2;
            var secondBounceHeight = bounceHeight * 0.3f;
            while (elapsed < secondBounceDuration)
            {
                elapsed += Time.deltaTime;
                var percent = elapsed / secondBounceDuration;
                var bounce = Mathf.Sin(percent * Mathf.PI) * secondBounceHeight;
                transform.position = targetPos + Vector3.up * bounce;
                yield return null;
            }

            transform.position = targetPos;
            collider2d.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.GetComponent<PlayerController>()) return;
            gameObject.SetActive(false);
            _pool.Trash(this);
        }
    }
}