using UnityEngine;

public class DeathPart : MonoBehaviour
{
    public Material newMaterial;

    private void OnEnable()
    {
        Debug.Log("DeathPart enabled");

        Renderer rend = GetComponent<Renderer>();
        if (rend != null && newMaterial != null)
        {
            rend.material = newMaterial;
        }
        else
        {
            Debug.LogWarning("Renderer or newMaterial is missing.");
        }
    }
}