using UnityEngine;

// Abstracted out as needs to be used in multiple instances
// Generates random Positions and Directions
public class RandomPositionGenerator {
    private Vector2 _randomDirection;

    public void GenerateRandomDirection() {
        float randomX = Random.Range(-100, 100);
        randomX /= 100;
        float randomY = Random.Range(-100, 100);
        randomY /= 100;

        _randomDirection.x = randomX;
        _randomDirection.y = randomY;
        _randomDirection = _randomDirection.normalized;
    }

    public Vector2 GetRandomDirection() {
        GenerateRandomDirection();
        return _randomDirection;
    }

    public Vector2 GetRandomPosition() {
        float randomX = Random.Range(-100, 100);
        randomX /= 100;
        float randomY = Random.Range(-100, 100);
        randomY /= 100;
        return new Vector2(randomX, randomY);
    }
}