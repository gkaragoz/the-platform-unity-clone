using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private TrailRenderer _trailRenderer;

    private Color _generatedRandomColor;

    private void Awake() {
        SetRandomColor();
        SetTrailRendererColor();
    }

    public void SetRandomColor() {
        _generatedRandomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        _renderer.material.color = _generatedRandomColor;
    }

    public void SetTrailRendererColor() {
        float alpha = 1.0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(_generatedRandomColor, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );

        _trailRenderer.colorGradient = gradient;
    }

}
