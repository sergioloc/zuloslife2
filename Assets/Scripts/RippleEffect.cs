using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RippleEffect : MonoBehaviour {

    [HideInInspector] public static RippleEffect instance;
    public Material rippleMaterial;
    private float currentFriction = 0f; // [Range(0,1)]
    [HideInInspector] public float currentAmount = 0f;

    private const float EXPECTED_DELTATIME_AT_60FPS = 1f / 60f;
    [HideInInspector]public const float LOWEST_AMOUNT_VALUE = 0.0001f;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this;
    }
 
    void FixedUpdate() { 
        rippleMaterial.SetFloat("_Amount", currentAmount);
        currentAmount *= currentFriction;
    }

    public void Play(float x, float y, float amount, float friction) {
        currentAmount = amount;
        currentFriction = friction;
        rippleMaterial.SetFloat("_CenterX", x);
        rippleMaterial.SetFloat("_CenterY", y);
    }

    public void Stop() {
        currentFriction = 0.8f;
    }
 
    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        Graphics.Blit(src, dst, rippleMaterial);
    }

}