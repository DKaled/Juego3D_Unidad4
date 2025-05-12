using UnityEngine;

public class BallControler : MonoBehaviour
{
    public float impulseForce = 3f;
    public Rigidbody rb;

    private bool ignoreCollision;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (ignoreCollision)
            return;

        DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
        if (deathPart) {
            GameManager.singleton.RestartLevel();
            return;
        }

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreCollision = true;
        Invoke("AllowNextCollision", 0.2f);
    }

    private void AllowNextCollision()
    {
        ignoreCollision = false;
    }

    public void ResetBall()
    {
        transform.position = startPosition;
    }

}
