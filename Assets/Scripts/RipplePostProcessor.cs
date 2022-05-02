using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RipplePostProcessor : MonoBehaviour {

    public Material rippleMaterial;
    private float currentFriction = 0f; // [Range(0,1)]
    private float currentAmount = 0f;
 
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