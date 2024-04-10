using UnityEngine;

public class ExplosionService : MonoBehaviour
{

    [SerializeField] private int _minExplodableParts;
    [SerializeField] private int _maxExplodableParts;

    [SerializeField] private float _explotionForce;
    [SerializeField] private float _explotionRadius;
    [SerializeField] private ParticleSystem _explotionParticles;

    private float _explotionChanceDivider = 2;
    private float _scaleDivider = 2;

    public void Explode(Explodable explodable)
    {
        ShowExplotion(explodable.Position);
        SpawnParts(explodable);
    }

    private void ShowExplotion(Vector3 position)
    {
        Instantiate(_explotionParticles,position, Quaternion.identity);
    }

    private void SpawnParts(Explodable explodable)
    {
        int explodablePartsAmount = GetRandomPartsAmount();

        for (int i = 0; i < explodablePartsAmount; i++)
        {
            Explodable explodablePart = Instantiate(explodable, Random.insideUnitSphere + explodable.Position, Quaternion.identity);

            Color color = GetRandomColor();
            Vector3 scale = explodable.Scale / _scaleDivider;
            float explotionChance = explodable.ExplosionChance / _explotionChanceDivider;

            explodablePart.Initialize(color, scale, explotionChance);

            explodablePart.AddExplosionForce(_explotionForce, explodable.Position, _explotionRadius);
        }
    }

    private int GetRandomPartsAmount()
    {
        return Random.Range(_minExplodableParts, _maxExplodableParts + 1);
    }

    private Color GetRandomColor()
    {
        Color color = Random.ColorHSV();
        color.a = 1;

        return color;
    }
}
