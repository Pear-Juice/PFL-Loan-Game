using System;
using UnityEngine;

namespace Movement_Controller
{
    public class GroundCheck : MonoBehaviour
    {
        public Vector2 verticalOffset;

        public Vector2 ray1Offset;
        public Vector2 ray2Offset;
        
        public float distance;
        public bool onGround;

        public bool debug;

        public void FixedUpdate()
        {
            var position = (Vector2)transform.position;
            RaycastHit2D hit1 = Physics2D.Raycast(position + verticalOffset + ray1Offset  , Vector2.down, distance);
            RaycastHit2D hit2 = Physics2D.Raycast(position + verticalOffset + ray2Offset, Vector2.down, distance);

            if (hit1 && hit1.transform.gameObject.CompareTag("Ground") || hit2 && hit2.transform.gameObject.CompareTag("Ground"))
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }
        }

        private void OnDrawGizmos()
        {
            if (debug)
            {
                var position = (Vector2)transform.position;
                Gizmos.DrawRay(position + verticalOffset + ray1Offset, Vector3.down * distance);
                Gizmos.DrawRay(position + verticalOffset + ray2Offset, Vector3.down * distance);
            }
        }
    }
}
