﻿using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Rock : MonoBehaviour, IPooledObject {

    [Header("Initializations")]
    [SerializeField]
    private Renderer _renderer = null;
    [SerializeField]
    private TrailRenderer _trailRenderer = null;
    [SerializeField]
    private ParticleSystem _crashFX = null;
    [SerializeField]
    private Rigidbody _rb = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private Enemy _enemy = null;
    [SerializeField]
    [Utils.ReadOnly]
    private Color _generatedRandomColor;

    private void Awake() {
        _enemy = GetComponent<Enemy>();
    }

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

            _enemy.OnCrashed();
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
        _rb.velocity = Vector3.down * _enemy.EnemyStats.GetMovementSpeed();
    }

    public void OnObjectReused() {
        SetRandomColor();
        SetTrailRendererColor();
        SetCrashParticleColor();

        _renderer.enabled = true;
        _rb.isKinematic = false;

        this.gameObject.SetActive(true);

        Fall();
    }

}
