using UnityEngine;
using Transform = UnityEngine.Transform;

public class PositioningUtils
{

    /*
    public static Vector3 LookFromToAt(Vector3 position, Vector3 target)
    {
        float x = target.x - position.x;
        float y = target.y - position.y;

        float newZ = Mathf.Atan2(y,x) * Mathf.Rad2Deg;

        return new Vector3(0, 0, newZ);
    }
    */

    public static Quaternion LookFromToAt(Vector3 origin, Vector3 target)
    {
        Vector2 yx = (Vector2)target - (Vector2)origin;
        return Quaternion.AngleAxis(Mathf.Atan2(yx.y, yx.x) * Mathf.Rad2Deg, Vector3.forward);
    }

    /*
     * Source for the predictive aim function below because I am bad at math:
     * https://gamedevelopment.tutsplus.com/tutorials/unity-solution-for-hitting-moving-targets--cms-29633
     */
    /// <summary>
    /// Returns an (x,y) that a projectile should be fired *at* to hit the target
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="targetTransform"></param>
    /// <param name="projectileVelocity"></param>
    /// <param name="targetVelocity"></param>
    /// <returns></returns>
    public static Vector2 PredictShotToTarget(Vector2 origin, Transform targetTransform, float projectileVelocity, Vector2 targetVelocity)
    {
        //Debug.DrawLine(transform.position, targetTransform.position, Color.red, 1f);
        float a = (targetVelocity.x * targetVelocity.x) + (targetVelocity.y * targetVelocity.y) - (projectileVelocity * projectileVelocity);
        float b = 2 * (targetVelocity.x * (targetTransform.position.x - origin.x)
            + targetVelocity.y * (targetTransform.transform.position.y - origin.y));
        float c = ((targetTransform.transform.position.x - origin.x) * (targetTransform.transform.position.x - origin.x)) +
        ((targetTransform.transform.position.y - origin.y) * (targetTransform.transform.position.y - origin.y));

        float disc = b * b - (4 * a * c);
        if (disc < 0)
        {
            return new Vector2(targetTransform.position.x, targetTransform.position.y);
        }
        //else 
        //{
        float t1 = (-1 * b + Mathf.Sqrt(disc)) / (2 * a);
        float t2 = (-1 * b - Mathf.Sqrt(disc)) / (2 * a);
        float t = Mathf.Max(t1, t2);// let us take the larger time value 
        float aimX = (targetVelocity.x * t) + targetTransform.transform.position.x;
        float aimY = targetTransform.gameObject.transform.position.y + (targetVelocity.y * t);

        return new Vector2(aimX, aimY);

        //}
    }

}
