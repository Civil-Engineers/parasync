using UnityEngine;
using Parasync.Runtime.Actions.Attack;
using Parasync.Runtime.Actions.Movement;

namespace Parasync.Runtime.Enemies
{
    [CreateAssetMenu(menuName = "Enemy")]
    public class BaseEnemy : ScriptableObject
    {
        [Header("Default Enemy Fields")]
        [SerializeField] private string enemyName = "New Enemy";
        [Tooltip("Number of units this enemy can move")]
        [Range(0, 10)]
        [SerializeField] private int moveDistance = 1;
        [Tooltip("Moves this enemy can perform")]
        [SerializeField] private MovementAction[] availableMoves;
        [Tooltip("Attacks this enemy can perform")]
        [SerializeField] private AttackAction[] availableAttacks;

        public string EnemyName { get { return enemyName; } }
        public int MoveDistance { get { return moveDistance; } }
        public MovementAction[] AvailableMoves { get { return availableMoves; } }
        public AttackAction[] AvailableAttacks { get { return availableAttacks; } }
    }
}
