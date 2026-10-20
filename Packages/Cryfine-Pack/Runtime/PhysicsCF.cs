using System.Collections.Generic;
using UnityEngine;

namespace Cryfine
{
    public static class PhysicsCF
    {
        private const int BufferSize = 64;
        private static readonly Collider[] buffer3D = new Collider[BufferSize];
        private static readonly Collider2D[] buffer2D = new Collider2D[BufferSize];
        private static readonly HashSet<Rigidbody> bodies3D = new HashSet<Rigidbody>();
        private static readonly HashSet<Rigidbody2D> bodies2D = new HashSet<Rigidbody2D>();

        public static bool IsGrounded3D(Vector3 position, float radius, float distance, LayerMask groundMask)
        {
            return Physics.CheckSphere(position + Vector3.down * distance, radius, groundMask);
        }

        public static bool IsGrounded2D(Vector2 position, float radius, float distance, LayerMask groundMask)
        {
            return Physics2D.OverlapCircle(position + Vector2.down * distance, radius, groundMask) != null;
        }

        public static bool HasLineOfSight3D(Vector3 from, Vector3 to, LayerMask obstacleMask)
        {
            return !Physics.Linecast(from, to, obstacleMask);
        }

        public static bool HasLineOfSight2D(Vector2 from, Vector2 to, LayerMask obstacleMask)
        {
            return !Physics2D.Linecast(from, to, obstacleMask);
        }

        public static bool IsAnyInRadius3D(Vector3 position, float radius, LayerMask mask)
        {
            return Physics.CheckSphere(position, radius, mask);
        }

        public static bool IsAnyInRadius2D(Vector2 position, float radius, LayerMask mask)
        {
            return Physics2D.OverlapCircle(position, radius, mask) != null;
        }

        public static bool CanSee3D(Vector3 origin, Vector3 forward, Vector3 target, float fov, float range, LayerMask obstacleMask)
        {
            float sqrDistance = MathCF.DisSqr3D(origin.x, origin.y, origin.z, target.x, target.y, target.z);
            if (sqrDistance > range * range)
            {
                return false;
            }

            if (Vector3.Angle(forward, target - origin) > fov * 0.5f)
            {
                return false;
            }

            return HasLineOfSight3D(origin, target, obstacleMask);
        }

        public static bool CanSee2D(Vector2 origin, Vector2 forward, Vector2 target, float fov, float range, LayerMask obstacleMask)
        {
            float sqrDistance = MathCF.DisSqr2D(origin.x, origin.y, target.x, target.y);
            if (sqrDistance > range * range)
            {
                return false;
            }

            if (Vector2.Angle(forward, target - origin) > fov * 0.5f)
            {
                return false;
            }

            return HasLineOfSight2D(origin, target, obstacleMask);
        }

        public static Collider GetClosestInRadius3D(Vector3 position, float radius, LayerMask mask)
        {
            int count = Physics.OverlapSphereNonAlloc(position, radius, buffer3D, mask);
            Collider closest = null;
            float closestSqrDistance = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                Vector3 center = buffer3D[i].bounds.center;
                float sqrDistance = MathCF.DisSqr3D(position.x, position.y, position.z, center.x, center.y, center.z);
                if (sqrDistance < closestSqrDistance)
                {
                    closestSqrDistance = sqrDistance;
                    closest = buffer3D[i];
                }
            }

            return closest;
        }

        public static Collider2D GetClosestInRadius2D(Vector2 position, float radius, LayerMask mask)
        {
            int count = Physics2D.OverlapCircleNonAlloc(position, radius, buffer2D, mask);
            Collider2D closest = null;
            float closestSqrDistance = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                Vector2 center = buffer2D[i].bounds.center;
                float sqrDistance = MathCF.DisSqr2D(position.x, position.y, center.x, center.y);
                if (sqrDistance < closestSqrDistance)
                {
                    closestSqrDistance = sqrDistance;
                    closest = buffer2D[i];
                }
            }

            return closest;
        }

        public static int GetAllInRadius3D(Vector3 position, float radius, LayerMask mask, Collider[] results)
        {
            return Physics.OverlapSphereNonAlloc(position, radius, results, mask);
        }

        public static int GetAllInRadius2D(Vector2 position, float radius, LayerMask mask, Collider2D[] results)
        {
            return Physics2D.OverlapCircleNonAlloc(position, radius, results, mask);
        }

        public static bool RaycastDebug3D(Vector3 origin, Vector3 direction, float distance, LayerMask mask, out RaycastHit hit, float duration = 0f)
        {
            bool hasHit = Physics.Raycast(origin, direction, out hit, distance, mask);
            Vector3 end = hasHit ? hit.point : origin + direction.normalized * distance;
            Debug.DrawLine(origin, end, hasHit ? Color.green : Color.red, duration);
            return hasHit;
        }

        public static bool RaycastDebug2D(Vector2 origin, Vector2 direction, float distance, LayerMask mask, out RaycastHit2D hit, float duration = 0f)
        {
            hit = Physics2D.Raycast(origin, direction, distance, mask);
            bool hasHit = hit.collider != null;
            Vector2 end = hasHit ? hit.point : origin + direction.normalized * distance;
            Debug.DrawLine(origin, end, hasHit ? Color.green : Color.red, duration);
            return hasHit;
        }

        public static bool MouseRaycast3D(Camera camera, LayerMask mask, out RaycastHit hit)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit, Mathf.Infinity, mask);
        }

        public static bool MouseRaycast2D(Camera camera, LayerMask mask, out RaycastHit2D hit)
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = Mathf.Abs(camera.transform.position.z);
            Vector2 worldPoint = camera.ScreenToWorldPoint(screenPoint);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, mask);
            return hit.collider != null;
        }

        public static void ClampSpeed3D(Rigidbody body, float maxSpeed)
        {
            if (body.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                body.velocity = body.velocity.normalized * maxSpeed;
            }
        }

        public static void ClampSpeed2D(Rigidbody2D body, float maxSpeed)
        {
            if (body.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                body.velocity = body.velocity.normalized * maxSpeed;
            }
        }

        public static void SetHorizontalVelocity3D(Rigidbody body, Vector3 velocity)
        {
            body.velocity = new Vector3(velocity.x, body.velocity.y, velocity.z);
        }

        public static void SetHorizontalVelocity2D(Rigidbody2D body, float x)
        {
            body.velocity = new Vector2(x, body.velocity.y);
        }

        public static float GetHorizontalSpeed3D(Rigidbody body)
        {
            return MathCF.DisEuc2D(body.velocity.x, body.velocity.z, 0f, 0f);
        }

        public static float GetHorizontalSpeed2D(Rigidbody2D body)
        {
            return Mathf.Abs(body.velocity.x);
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

        public static void Explosion3D(Vector3 center, float radius, float force, LayerMask mask, float upwardsModifier = 0f)
        {
            int count = Physics.OverlapSphereNonAlloc(center, radius, buffer3D, mask);
            bodies3D.Clear();

            for (int i = 0; i < count; i++)
            {
                Rigidbody body = buffer3D[i].attachedRigidbody;
                if (body != null && bodies3D.Add(body))
                {
                    body.AddExplosionForce(force, center, radius, upwardsModifier, ForceMode.Impulse);
                }
            }
        }

        public static void Explosion2D(Vector2 center, float radius, float force, LayerMask mask)
        {
            int count = Physics2D.OverlapCircleNonAlloc(center, radius, buffer2D, mask);
            bodies2D.Clear();

            for (int i = 0; i < count; i++)
            {
                Rigidbody2D body = buffer2D[i].attachedRigidbody;
                if (body != null && bodies2D.Add(body))
                {
                    Vector2 direction = body.position - center;
                    float falloff = 1f - MathCF.Clamp01(direction.magnitude / radius);
                    body.AddForce(direction.normalized * force * falloff, ForceMode2D.Impulse);
                }
            }
        }

        public static Vector3 ProjectilePosition3D(Vector3 start, Vector3 velocity, float time)
        {
            return start + velocity * time + Physics.gravity * (0.5f * time * time);
        }

        public static Vector2 ProjectilePosition2D(Vector2 start, Vector2 velocity, float time, float gravityScale = 1f)
        {
            return start + velocity * time + Physics2D.gravity * gravityScale * (0.5f * time * time);
        }

        public static Vector3 LeadTarget3D(Vector3 shooter, Vector3 target, Vector3 targetVelocity, float projectileSpeed)
        {
            if (projectileSpeed <= 0f)
            {
                return target;
            }

            float distance = MathCF.DisEuc3D(shooter.x, shooter.y, shooter.z, target.x, target.y, target.z);
            float time = distance / projectileSpeed;
            return target + targetVelocity * time;
        }

        public static Vector2 LeadTarget2D(Vector2 shooter, Vector2 target, Vector2 targetVelocity, float projectileSpeed)
        {
            if (projectileSpeed <= 0f)
            {
                return target;
            }

            float distance = MathCF.DisEuc2D(shooter.x, shooter.y, target.x, target.y);
            float time = distance / projectileSpeed;
            return target + targetVelocity * time;
        }

        public static Vector3 GetLaunchVelocity3D(Vector3 start, Vector3 target, float time)
        {
            if (time <= 0f)
            {
                return Vector3.zero;
            }

            return (target - start - Physics.gravity * (0.5f * time * time)) / time;
        }

        public static Vector2 GetLaunchVelocity2D(Vector2 start, Vector2 target, float time, float gravityScale = 1f)
        {
            if (time <= 0f)
            {
                return Vector2.zero;
            }

            return (target - start - Physics2D.gravity * gravityScale * (0.5f * time * time)) / time;
        }

        public static void DrawTrajectory3D(Vector3 start, Vector3 velocity, int steps, float stepTime, Color color, float duration = 0f)
        {
            Vector3 previous = start;

            for (int i = 1; i <= steps; i++)
            {
                Vector3 next = ProjectilePosition3D(start, velocity, i * stepTime);
                Debug.DrawLine(previous, next, color, duration);
                previous = next;
            }
        }

        public static void DrawTrajectory2D(Vector2 start, Vector2 velocity, int steps, float stepTime, Color color, float duration = 0f, float gravityScale = 1f)
        {
            Vector2 previous = start;

            for (int i = 1; i <= steps; i++)
            {
                Vector2 next = ProjectilePosition2D(start, velocity, i * stepTime, gravityScale);
                Debug.DrawLine(previous, next, color, duration);
                previous = next;
            }
        }

        public static bool GetGroundNormal3D(Vector3 position, float distance, LayerMask mask, out Vector3 normal)
        {
            RaycastHit hit;
            if (Physics.Raycast(position, Vector3.down, out hit, distance, mask))
            {
                normal = hit.normal;
                return true;
            }

            normal = Vector3.up;
            return false;
        }

        public static bool GetGroundNormal2D(Vector2 position, float distance, LayerMask mask, out Vector2 normal)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, distance, mask);
            if (hit.collider != null)
            {
                normal = hit.normal;
                return true;
            }

            normal = Vector2.up;
            return false;
        }

        public static float GetSlopeAngle3D(Vector3 position, float distance, LayerMask mask)
        {
            Vector3 normal;
            if (GetGroundNormal3D(position, distance, mask, out normal))
            {
                return Vector3.Angle(normal, Vector3.up);
            }

            return 0f;
        }

        public static float GetSlopeAngle2D(Vector2 position, float distance, LayerMask mask)
        {
            Vector2 normal;
            if (GetGroundNormal2D(position, distance, mask, out normal))
            {
                return Vector2.Angle(normal, Vector2.up);
            }

            return 0f;
        }
    }
}
