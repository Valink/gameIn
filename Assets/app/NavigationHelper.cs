using UnityEngine;

public class NavigationHelper
{
    public static void LookToward(Transform obj, Vector3 targetPosition)
    {
        var direction = targetPosition - obj.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        obj.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static void MoveToward(Transform transform, Vector3 targetPosition, float speed)
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
