using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }
        #region Editor Fields
        [SerializeField] private AIBehaviour behaviour;

        [SerializeField] private AIPointControl patrolPoint;

        [SerializeField] private Projectile projectile;

        [SerializeField] public UnityEvent OnEndPath;
        
        public AIPointControl PatrolPoint
        {
            set
            {
                patrolPoint = value;
            }
        }

        [Range(0.0f, 1.0f)]
        [SerializeField] private float navigationLinear, navigationAngular;

        [SerializeField] private float randomSelectMovePointTime;

        [SerializeField] private float newTargetSearchTime;

        [SerializeField] private float shootDelay;

        [SerializeField] private float evadeRayLength;
        #endregion
        #region Fields
        private SpaceShip spaceShip;

        private Vector3 movePosition, leadPosition;

        private Enemy selectedTarget;

        private Rigidbody2D selectedTargetRB;

        private Path path;

        private int pathIndex;

        //private Timer randomizeDirectionTimer, fireTimer, findNewTargetTimer;
        #endregion
        private void Start()
        {
            spaceShip = GetComponent<SpaceShip>();
            InitTimers();

        }
        private void Update()
        {
            UpdateTimers();

            UpdateAI();

        }
        #region AI
        private void UpdateAI()
        {
            if (behaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }
        private void UpdateBehaviourPatrol()
        {
            ActionFindNewPosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();

        }
        private void SetPatrolBehaviour(AIPointControl point)
        {
            behaviour = AIBehaviour.Patrol;
            patrolPoint = point;
        }
        private void ActionFindNewPosition()
        {
            if (behaviour == AIBehaviour.Patrol)
            {
                if (selectedTarget != null)
                {
                    MakeLead();
                    movePosition = leadPosition;
                }
                else
                {
                    if (patrolPoint != null)
                    {
                        bool IsInsidePatrolArea = (patrolPoint.transform.position - transform.position).sqrMagnitude < patrolPoint.Radius * patrolPoint.Radius;

                        if (IsInsidePatrolArea)
                        {
                            pathIndex++;
                            if (path.Lenght > pathIndex) SetPatrolBehaviour(path[pathIndex]);

                            else
                            {
                                OnEndPath?.Invoke();

                                Destroy(gameObject);
                            }
                        }
                        else movePosition = patrolPoint.transform.position;
                    }
                }
            }
        }
        private void ActionControlShip()
        {
            spaceShip.ThrustControl = navigationLinear;
            spaceShip.TorqueControl = ComputeAlignTorqueNormalized(movePosition, spaceShip.transform) * navigationAngular;
        }
        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, evadeRayLength))
            {
                movePosition = transform.position + transform.right * 100.0f;
            }
        }
        private void ActionFindNewAttackTarget()
        {
            /*if (findNewTargetTimer.IsFinished)
            {
                selectedTarget = FindNearestDestructibleTarget();

                if( selectedTarget != null)
                {
                    MakeLead();

                    findNewTargetTimer.Start(newTargetSearchTime);
                }
            }*/
        }
        private void ActionFire()
        {
            if (selectedTarget != null)
            {
                MakeLead();
                /*if (fireTimer.IsFinished)
                {

                    //spaceShip.Fire(TurretMode.Primary);

                    findNewTargetTimer.Start(shootDelay);
                }*/
            }
        }
        private void MakeLead()
        {
            //leadPosition = Utils.MakeLead(projectile, selectedTarget.transform, transform, selectedTargetRB);
            movePosition = leadPosition;
        }
        private Enemy FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue;

            Enemy potentialTarget = null;

            foreach (var v in Enemy.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == spaceShip) continue;

                if (v.TeamID == Enemy.TeamIDNeutral) continue;

                if (v.TeamID == spaceShip.TeamID) continue;

                float dist = Vector2.Distance(spaceShip.transform.position, v.transform.position);

                if (dist < maxDist) maxDist = dist;
                potentialTarget = v;
            }
            if (potentialTarget != null) selectedTargetRB = potentialTarget.GetComponent<Rigidbody2D>();
            return potentialTarget;
        }
        private const float MAX_ANGLE = 45.0f;

        private static float ComputeAlignTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }
        public void SetPath(Path path)
        {
            this.path = path;

            pathIndex = 0;

            SetPatrolBehaviour(path[pathIndex]);
        }
        #endregion
        #region TImers
        private void InitTimers()
        {
            //randomizeDirectionTimer = new Timer(randomSelectMovePointTime);
            //fireTimer = new Timer(shootDelay);
            //findNewTargetTimer = new Timer(newTargetSearchTime);
        }
        private void UpdateTimers()
        {
            //randomizeDirectionTimer.DecreaseTime(Time.deltaTime);
            //fireTimer.DecreaseTime(Time.deltaTime);
            //findNewTargetTimer.DecreaseTime(Time.deltaTime);
        }
        #endregion
    }
}