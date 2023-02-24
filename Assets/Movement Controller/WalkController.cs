using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement_Controller
{
    //Huge thanks to Game Makers Toolkit <3 https://gmtk.itch.io/platformer-toolkit/devlog/395523/behind-the-code
    public class WalkController : MonoBehaviour
    {
        public GroundCheck enemyGroundCheck;
        public Rigidbody2D body;
        
        [Header("Walking")]
        public float maxSpeed;
        public float maxAcceleration;
        public float maxDeceleration;
        public float maxTurnSpeed;

        [Header("In Air")] 
        public float maxAirAcceleration;
        public float maxAirDeceleration;
        public float maxAirTurnSpeed;

        private float directionX;
        private float desiredVelocity;

        private float velocity;

        private void Start()
        {
            InputMan.MoveAction += OnMove;
        }

        public void OnMove(InputValue value)
        {
            directionX = value.Get<Vector2>().x;
        }

        public void Update()
        {
            desiredVelocity = directionX * maxSpeed;
        }

        public void FixedUpdate()
        {
            float acceleration = enemyGroundCheck.onGround ? maxAcceleration : maxAirAcceleration;
            float deceleration = enemyGroundCheck.onGround ? maxDeceleration : maxAirDeceleration;
            float turnSpeed = enemyGroundCheck.onGround ? maxTurnSpeed : maxAirTurnSpeed;

            float maxSpeedChange = 0;
            
            //No point in running veloocity calculations if you aren't moving
            if (directionX != 0)
            {
                //If trying to walk same way as velocity, use acceleration, else use max turn speed
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (Mathf.Sign(directionX) != Mathf.Sign(velocity))
                    maxSpeedChange = turnSpeed * Time.deltaTime;
                else
                    maxSpeedChange = acceleration * Time.deltaTime;
            }
            else
            {
                maxSpeedChange = deceleration * Time.deltaTime;
            }

            velocity = Mathf.MoveTowards(velocity, desiredVelocity, maxSpeedChange);

            body.velocity = new Vector2(velocity, body.velocity.y);
        }
    }
}
