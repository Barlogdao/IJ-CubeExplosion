using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Explodable : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ExplosionService _explosionService;

    private float _exposionChance = 100;

    public Vector3 Position => transform.position;
    public Vector3 Scale => transform.localScale;
    public float ExplosionChance => _exposionChance;

    public void Initialize(Color color, Vector3 scale, float explosionChance)
    {
        _meshRenderer.material.color = color;
        transform.localScale = scale;
        _exposionChance = explosionChance;
    }

    private void OnMouseUpAsButton()
    {
        TryExplode();
    }

    public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explotionRadius)
    {
        _rigidbody.AddExplosionForce(explosionForce, explosionPosition, explotionRadius);
    }

    private void TryExplode()
    {
        if (_exposionChance > GetRandomChance())
        {
            _explosionService.Explode(this);
        }

        Destroy(gameObject);
    }

    private float GetRandomChance() => Random.Range(0f, 100f);
}