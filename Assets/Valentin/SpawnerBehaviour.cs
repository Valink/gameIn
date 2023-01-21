using System;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject ennemi;
    [SerializeField] GameObject friend;
    [SerializeField] Camera playerCamera;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        SpawnRadomlyOutsideOfView();
        // }
    }

    private void SpawnRadomlyOutsideOfView()
    {
        var topRightPointPosition = playerCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        var bottomLeftPointPosition = playerCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        var width = topRightPointPosition.x - bottomLeftPointPosition.x;
        var height = topRightPointPosition.y - bottomLeftPointPosition.y;
        var perimeter = width * 2 + height * 2;

        var randPerimeterValue = UnityEngine.Random.Range(0, perimeter);

        var xOffset = width / 2;
        var yOffset = height / 2;

        var spawnableSize = 1;

        if (randPerimeterValue <= width) // top
        {
            Instantiate(friend, new Vector2(randPerimeterValue - xOffset, height / 2 + spawnableSize), Quaternion.identity);
        }
        else if (randPerimeterValue <= width + height) // right
        {
            Instantiate(friend, new Vector2(width / 2 + spawnableSize, randPerimeterValue - width - yOffset), Quaternion.identity);
        }
        else if (randPerimeterValue <= width + height + width) // bottom
        {
            Instantiate(friend, new Vector2(randPerimeterValue - width - height - xOffset, -(height / 2 + spawnableSize)), Quaternion.identity);
        }
        else // left
        {
            Instantiate(friend, new Vector2(-(width / 2 + spawnableSize), randPerimeterValue - width - height - width - yOffset), Quaternion.identity);
        }

    }
}
