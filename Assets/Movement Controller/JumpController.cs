using System;
using UnityEngine;

namespace Movement_Controller
{
    public class JumpController : MonoBehaviour
    {
        public GroundCheck enemyGroundCheck;
        public Rigidbody2D body;

        public Action OnJumpAction;

        [Header("Jump Settings")] 
        public float jumpHeight;
        public float timeToJumpApex;
        public float gravityMultiplier;
        public float downwardMovementMultiplier;
        
        private Vector2 velocity;
        
        private bool desireJump;

        private void Start()
        {
            InputMan.JumpAction += OnJump;
        }

        public void OnJump()
        {
            desireJump = true;

            Vector2 newGravity = new Vector2(0, (-2 * jumpHeight) / (timeToJumpApex * timeToJumpApex));
            body.gravityScale = (newGravity.y / Physics2D.gravity.y) / gravityMultiplier;
            
            OnJumpAction?.Invoke();
        }

        private void FixedUpdate()
        {
            velocity = body.velocity;

            if (desireJump)
            {
                doAJump();
                body.velocity = velocity;
                return;
            }

            if (body.velocity.y == 0) gravityMultiplier = 1;
            if (body.velocity.y < -0.01) gravityMultiplier = downwardMovementMultiplier;
        }

        private void doAJump()
        {
            if (enemyGroundCheck.onGround)
            {
                desireJump = false;
                float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * body.gravityScale * jumpHeight);

                if (velocity.y > 0f)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0);
                }
                else if (velocity.y < 0f)
                {
                    jumpSpeed += Mathf.Abs(body.velocity.y);
                }

                velocity.y += jumpSpeed;
            }

            desireJump = false;
        }
    }
}
