using UnityEngine;

namespace Cryfine
{
    public static class PhysicsCF
    {
        public static bool IsGrounded3D(Vector3 position, float radius, float distance, LayerMask groundMask)
        {
            Vector3 checkPoint = position + Vector3.down * distance;
            return Physics.CheckSphere(checkPoint, radius, groundMask);
        }

        public static bool IsGrounded2D(Vector2 position, float radius, float distance, LayerMask groundMask)
        {
            Vector2 checkPoint = position + Vector2.down * distance;
            Collider2D found = Physics2D.OverlapCircle(checkPoint, radius, groundMask);
            return found != null;
        }

        public static bool HasLineOfSight3D(Vector3 from, Vector3 to, LayerMask obstacleMask)
        {
            bool blocked = Physics.Linecast(from, to, obstacleMask);
            return !blocked;
        }

        public static bool HasLineOfSight2D(Vector2 from, Vector2 to, LayerMask obstacleMask)
        {
            bool blocked = Physics2D.Linecast(from, to, obstacleMask);
            return !blocked;
        }

        public static Collider GetClosestInRadius3D(Vector3 position, float radius, LayerMask mask)
        {
            Collider[] found = Physics.OverlapSphere(position, radius, mask);
            Collider closest = null;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < found.Length; i++)
            {
                Vector3 other = found[i].transform.position;
                float distance = MathCF.DisSqr3D(position.x, position.y, position.z, other.x, other.y, other.z);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = found[i];
                }
            }

            return closest;
        }

        public static Collider2D GetClosestInRadius2D(Vector2 position, float radius, LayerMask mask)
        {
            Collider2D[] found = Physics2D.OverlapCircleAll(position, radius, mask);
            Collider2D closest = null;
            float closestDistance = float.MaxValue;

            for (int i = 0; i < found.Length; i++)
            {
                Vector2 other = found[i].transform.position;
                float distance = MathCF.DisSqr2D(position.x, position.y, other.x, other.y);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = found[i];
                }
            }

            return closest;
        }

        public static void ClampSpeed3D(Rigidbody body, float maxSpeed)
        {
            if (body.velocity.magnitude > maxSpeed)
            {
                body.velocity = body.velocity.normalized * maxSpeed;
            }
        }

        public static void ClampSpeed2D(Rigidbody2D body, float maxSpeed)
        {
            if (body.velocity.magnitude > maxSpeed)
            {
                body.velocity = body.velocity.normalized * maxSpeed;
            }
        }

        public static void Knockback3D(Rigidbody body, Vector3 direction, float force)
        {
            body.AddForce(direction.normalized * force, ForceMode.Impulse);
        }

        public static void Knockback2D(Rigidbody2D body, Vector2 direction, float force)
        {
            body.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }

        public static void StopBody3D(Rigidbody body)
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        public static void StopBody2D(Rigidbody2D body)
        {
            body.velocity = Vector2.zero;
            body.angularVelocity = 0f;
        }
    }
}
