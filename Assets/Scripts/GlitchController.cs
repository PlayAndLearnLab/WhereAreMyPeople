using UnityEngine;

public class GlitchController : MonoBehaviour
{
    public Material glitchMaterial;
    public float maxGlitchTime = 0.5f;
    public float minDelay = 2f;
    public float maxDelay = 5f;

    private float glitchTimer = 0f;
    private float nextGlitchTime;

    // The name of the property you created in Shader Graph
    private static readonly int DisplacementID = Shader.PropertyToID("_DisplacementAmount");

    void Start()
    {
        // Get the Material on start
        if (glitchMaterial == null)
            glitchMaterial = GetComponent<Renderer>().material;
        
        SetNextGlitchTime();
        // Start with no displacement
        glitchMaterial.SetVector(DisplacementID, Vector3.zero);
    }

    void Update()
    {
        if (glitchTimer > 0)
        {
            // Glitching: lerp the intensity down
            glitchTimer -= Time.deltaTime;
            float intensity = glitchTimer / maxGlitchTime;
            
            // Set the DisplacementAmount property on the shader
            // Use random * intensity to keep the displacement chaotic
            float displacement = Mathf.Lerp(0f, 0.4f, intensity);
            glitchMaterial.SetVector(DisplacementID, new Vector3(displacement, displacement, displacement));
        }
        else if (Time.time > nextGlitchTime)
        {
            // Start a new glitch
            glitchTimer = maxGlitchTime;
            SetNextGlitchTime();
        }
    }

    void SetNextGlitchTime()
    {
        nextGlitchTime = Time.time + Random.Range(minDelay, maxDelay);
    }

    // Always reset the material properties on destroy to avoid breaking other materials
    private void OnDestroy()
    {
        glitchMaterial.SetVector(DisplacementID, Vector3.zero);
    }
}