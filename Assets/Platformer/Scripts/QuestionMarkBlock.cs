using System.Collections;
using UnityEngine;

public class QuestionMarkBlock : MonoBehaviour
{
    public Material brownMaterial;     
    public Material questionMaterial;   
    public float jumpForce = 0.5f;      
    public float jumpDuration = 0.1f;   

    private Vector3 originalPosition;
    public bool isHit = false;        
    private new Renderer renderer;

    void Start()
    {
        originalPosition = transform.localPosition;
        renderer = GetComponent<Renderer>();
        renderer.material = questionMaterial;
    }

    void OnMouseDown()
    {
        if (!isHit)
        {
            isHit = true;
            StartCoroutine(Jump());
        }
    }

    public IEnumerator Jump()
    {
        // Move up
        Vector3 targetPosition = originalPosition + new Vector3(0, jumpForce, 0);
        float elapsedTime = 0f;
        while (elapsedTime < jumpDuration)
        {
            transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move down
        elapsedTime = 0f;
        while (elapsedTime < jumpDuration)
        {
            transform.localPosition = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        renderer.material = brownMaterial;
    }
}