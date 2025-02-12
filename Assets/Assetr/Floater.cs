using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public int floaterCounter = 1;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    private void FixedUpdate()
    {
        rigidBody.AddForceAtPosition(Physics.gravity / floaterCounter, transform.position, ForceMode.Acceleration);
        float waveheight = WaveManager.instance.GetWaveHeight(transform.position.x);
        if (transform.position.y < waveheight)

        {
            float displaceMultiplier = Mathf.Clamp01((waveheight - transform.position.y)-transform.position.y/ depthBeforeSubmerged) * displacementAmount; 
            rigidBody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displaceMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rigidBody.AddForce(displaceMultiplier * -rigidBody.linearVelocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidBody.AddTorque(displaceMultiplier * -rigidBody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

}