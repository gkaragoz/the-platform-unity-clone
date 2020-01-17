using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private TrailRenderer _trailRenderer;
    [SerializeField]
    private ParticleSystem _crashFX;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private float _fallingSpeed = 1f;

    private Color _generatedRandomColor;

    private void Start() {
        SetRandomColor();
        SetTrailRendererColor();
        SetCrashParticleColor();
    }

    private void Update() {
        if (_rb.position.y < 0) {
            _rb.position = new Vector3(_rb.position.x, transform.localScale.y * 0.5f, _rb.position.z);
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;

            _renderer.enabled = false;
            _crashFX.Play();

            StartDestroyTimer();
        }
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

    public void SetCrashParticleColor() {
        ParticleSystem.MainModule main = _crashFX.main;
        main.startColor = _generatedRandomColor;
    }

    public void Fall() {
        _rb.velocity = Vector3.down * _fallingSpeed;
    }

    public void StartDestroyTimer() {
        Destroy(this.gameObject, 2f);
    }

}
