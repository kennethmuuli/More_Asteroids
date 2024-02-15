using UnityEngine;

public class RayController : MonoBehaviour
{
    [SerializeField] private Texture[] textures; 
    private LineRenderer lineRenderer;
    private int animationStep;
    [SerializeField]private float fps = 30f;
    private float fpsCounter;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        fpsCounter += Time.deltaTime;

        if(fpsCounter >= 1f / fps){
            animationStep++;

            if(animationStep == textures.Length) {
                animationStep = 0;
            }

            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0f;
        }
    }

    public void SetUpLinePositions(Vector2 startPos, Vector2 targetPos){
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, targetPos);
    }
}
