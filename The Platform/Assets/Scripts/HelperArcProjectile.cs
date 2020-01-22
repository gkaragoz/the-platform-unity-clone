using UnityEngine;

/// <summary>
/// Referenced from https://en.wikipedia.org/wiki/Trajectory_of_a_projectile
/// </summary>

public static class HelperArcProjectile {

    private static float GRAVITY = Mathf.Abs(Physics.gravity.y); // Force of gravity on Y axis.

    // Rotate itself to target.
    private static Quaternion GetAngleBetweenTarget(Vector3 targetPosition, Vector3 startPosition) {
        Vector3 direction = targetPosition - startPosition;
        return Quaternion.LookRotation(direction, Vector3.up);
    }

    // Get required force to hit the target on X, Y.
    private static float GetForceToHitTarget(float degree, Vector3 targetPosition, Vector3 startPosition) {
        float distance = Vector3.Distance(targetPosition, startPosition);
        return Mathf.Sqrt(distance * GRAVITY / Mathf.Sin(2 * (Mathf.Deg2Rad * degree)));
    }

    // Get final force vector calculations to hit the target.
    public static Vector3 MagicShoot(float degree, Vector3 targetPosition, Vector3 startPosition) {
        float forceAmount = GetForceToHitTarget(degree, targetPosition, startPosition);
        Quaternion angleBetweenTarget = GetAngleBetweenTarget(targetPosition, startPosition);

        return angleBetweenTarget * Quaternion.Euler(-degree, 0, 0) * Vector3.forward * forceAmount;
    }

    // y0 = initial height
    // v0 = initial speed vector (included force)
    // teta = initial angle
    // x = travelled distance from origin
    // x0 = origin (0)

}
