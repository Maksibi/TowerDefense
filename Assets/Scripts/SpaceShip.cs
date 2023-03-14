using System;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        #region Properties
        public float ThrustControl
        {
            get => _thrustControl;
            set
            {
                _thrustControl = value;
            }
        }
        public float TorqueControl
        {
            get => _torqueControl;
            set
            {
                _torqueControl = value;
            }
        }

        #endregion


        #region Editor Fields
        [Header("Space Ship")]
        [SerializeField]
        private float m_mass, m_thrust, m_mobility, m_maxLinearVelocity, m_maxAngularVelocity;
        public float Agility => m_maxAngularVelocity;
        public float Speed => m_maxLinearVelocity;


        [SerializeField] private GameObject impactEffect;

        public Sprite PreviewImage;
        public string ShipType; 
        #endregion

        #region Fields
        private Rigidbody2D m_rigid;
        private float _thrustControl, _torqueControl;

        #endregion

        #region Unity Events
        protected override void Start()
        {
            base.Start();

            m_rigid = GetComponent<Rigidbody2D>();
            m_rigid.mass = m_mass;

            m_rigid.inertia = 1.0f;
        }
        protected override void OnDeath()
        {
         if (impactEffect != null)   Instantiate(impactEffect, transform.position, Quaternion.identity);

            base.OnDeath();
        }
        private void FixedUpdate()
        {
            UpdateRigidbody();
        }
        private void UpdateRigidbody()
        {
            m_rigid.AddForce(m_thrust * ThrustControl * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_rigid.AddForce(-m_rigid.velocity * (m_thrust / m_maxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_rigid.AddTorque(TorqueControl * m_mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_rigid.AddTorque(-m_rigid.angularVelocity * (m_mobility / m_maxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        #endregion

        public void Use(EnemyAsset asset)
        {
            m_maxLinearVelocity = asset.movespeed;
            m_hitpoints = asset.hitpoints;
        }
    }
}

