using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Parasync.Runtime.Actions.Movement;

namespace Parasync.Runtime.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private GameObject enemyObject;
        [SerializeField] private Transform enemySprite;
        [SerializeField] private GameObject attackIndicator;

        [SerializeField] private GameObject playerObject;
        [SerializeField] private BaseEnemy enemyTraits;

        [Header("Event Listeners")]
        [SerializeField] private UnityEvent onEnemyTurnEnd;

        private bool _isFacingRight = true;

        private GameObject[] _attackIndicators;

        private void Start()
        {
            _attackIndicators = CreateAttackIndicators();
        }

        public void OnPlayerTurnEnd()
        {
            StartCoroutine(MoveCoroutine());
        }

        IEnumerator MoveCoroutine()
        {
            Vector3 currPos = enemyObject.transform.position;
            Vector3 newVector = CalculateClosestVectorToPlayer(currPos);
            Vector3 newPos = currPos + newVector;
            bool faceRight = newVector.x > 0 && newVector.z > 0;
            TweenMovement(newPos, faceRight);

            UpdateAttackIndicators(_attackIndicators, newVector);

            yield return new WaitForSeconds(0.5f);
            onEnemyTurnEnd?.Invoke();
        }

        private Vector3 CalculateClosestVectorToPlayer(Vector3 currPos)
        {
            float currX = currPos.x, currZ = currPos.z;
            Vector3 playerPos = playerObject.transform.position;

            float minMagnitude = float.PositiveInfinity;
            Vector3 minVector = Vector3.zero;

            foreach (MovementAction move in enemyTraits.AvailableMoves)
            {
                Vector2 dir = move.MoveDirection;
                float dirX = dir.x, dirY = dir.y;
                Vector3 newPos = new Vector3(currX + dirX, 0, currZ + dirY);
                float newMag = (playerPos - newPos).magnitude;
                minMagnitude = Mathf.Min(minMagnitude, newMag);

                if (newMag == minMagnitude)
                    minVector = new Vector3(dirX, 0, dirY);
            }
            return minVector;
        }

        private GameObject[] CreateAttackIndicators()
        {
            Vector3 currPos = enemyObject.transform.position;
            float currX = currPos.x, currZ = currPos.z;

            MovementAction[] moves = enemyTraits.AvailableMoves;
            GameObject[] indicators = new GameObject[moves.Length];

            for (int i = 0; i < moves.Length; i++)
            {
                Vector2 dir = moves[i].MoveDirection;
                float dirX = dir.x, dirY = dir.y;
                Vector3 movePos = new Vector3(currX + dirX, 0, currZ + dirY);
                GameObject indicator = Instantiate(attackIndicator, movePos, Quaternion.identity);
                indicator.transform.parent = enemyObject.transform;

                indicators[i] = indicator;
            }
            return indicators;
        }

        private void UpdateAttackIndicators(GameObject[] indicators, Vector3 moveVector)
        {
            for (int i = 0; i < indicators.Length; i++)
            {
                Transform indicator = indicators[i].transform;
                Vector3 currPos = indicator.position;
                currPos += moveVector;
                TweenAttackIndicatorUpdate(indicator, currPos);
            }
        }

        private void TweenMovement(Vector3 target, bool faceRight)
        {
            float time = 0.2f;

            if (!_isFacingRight && faceRight)
            {
                _isFacingRight = true;
                enemySprite.DORotate(new Vector3(0, -90, 0), time).SetEase(Ease.InQuad);
                enemySprite.DOScale(new Vector3(1, 1, 1), 0).SetDelay(time);
                enemySprite.DORotate(new Vector3(0, 90, 0), 0).SetDelay(time).SetEase(Ease.OutQuad);
                enemySprite.DORotate(new Vector3(0, 0, 0), time).SetDelay(time);
            }
            else if (_isFacingRight && !faceRight)
            {
                _isFacingRight = false;
                enemySprite.DORotate(new Vector3(0, -90, 0), time).SetEase(Ease.InQuad);
                enemySprite.DOScale(new Vector3(-1, 1, 1), 0).SetDelay(time);
                enemySprite.DORotate(new Vector3(0, 90, 0), 0).SetDelay(time).SetEase(Ease.OutQuad);
                enemySprite.DORotate(new Vector3(0, 0, 0), time).SetDelay(time);
            }
            enemyObject.transform.DOMove(target, 0.15f);
        }

        private void TweenAttackIndicatorUpdate(Transform indicator, Vector3 updatedPos)
        {
            indicator.DOMove(updatedPos, 0.2f);
        }
    }
}
