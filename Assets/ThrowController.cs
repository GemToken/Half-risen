using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public float velocityMod = 1;
    public Transform Projectile;
    public  Transform ThrowOrigin;


    public IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Projectile.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Projectile.position = ThrowOrigin.position + new Vector3(0.0f, 0.5f, 0.65f);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        Projectile.GetComponent<Rigidbody>().AddForce((Target.position - transform.position) * Time.deltaTime * projectile_Velocity * velocityMod, ForceMode.Impulse);

        yield return null;
    }
}
