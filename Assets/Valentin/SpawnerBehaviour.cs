using System;
using System.Collections;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject ennemi;
    [SerializeField] GameObject friend;
    [SerializeField] Camera playerCamera;

    void Awake()
    {
        StartCoroutine("SpawnEntity");
    }

    IEnumerator SpawnEntity()
    {
        for (; ; )
        {
            SpawnRadomlyOutsideOfView();
            yield return new WaitForSeconds(1f);
        }
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
        // var spawnableSize = 0;

        var position = Vector2.zero;

        if (randPerimeterValue <= width) // top
        {
            position = new Vector2(randPerimeterValue - xOffset, height / 2 + spawnableSize);
        }
        else if (randPerimeterValue <= width + height) // right
        {
            position = new Vector2(width / 2 + spawnableSize, randPerimeterValue - width - yOffset);
        }
        else if (randPerimeterValue <= width + height + width) // bottom
        {
            position = new Vector2(randPerimeterValue - width - height - xOffset, -(height / 2 + spawnableSize));
        }
        else // left
        {
            position = new Vector2(-(width / 2 + spawnableSize), randPerimeterValue - width - height - width - yOffset);
        }

        SpawnEntity(position);
    }

    private void SpawnEntity(Vector2 position)
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            Instantiate(ennemi, position, Quaternion.identity);
        }
        else
        {
            var f = Instantiate(friend, position, Quaternion.identity);
            f.name = GetRandomSpaceName();
            f.GetComponent<Satellite>().Init(player);
        }
    }

    private string GetRandomSpaceName()
    {
        var spaceShipNames = new string[] { "Cosmosat", "Starlink", "Skylark", "Orbitalis", "Galaxia", "Lunarix", "Skywatch", "Solaria", "Stellaris", "Cosmicon", "Spacehawk", "Skyseeker", "Starfinder", "Orbitalstar", "Galaxar", "Lunarion", "Skygazer", "Solarscan", "Stellaright", "Cosmovoyage", "Spacebeam", "Skydome", "Starstream", "Orbitalbeam", "Galaxylight", "Lunarview", "Skyobserver", "Solarview", "Stellarview", "Cosmovision", "Spaceprobe", "Skytracker", "Starnavigator", "Orbitaltracker", "Galaxynav", "Lunarprobe", "Skyexplorer", "Solarprobe", "Stellarprobe", "Cosmoseeker", "Spaceguard", "Skywatchman", "Starpatrol", "Orbitalguard", "Galaxysentry", "Lunarwatch", "Skymonitor", "Solarsentry", "Stellarwatch", "Cosmoguard", "Spacehawk", "Skyseeker", "Starfinder", "Orbitalstar", "Galaxar", "Lunarion", "Skygazer", "Solarscan", "Stellaright", "Cosmovoyage", "Spacebeam", "Skydome", "Starstream", "Orbitalbeam", "Galaxylight", "Lunarview", "Skyobserver", "Solarview", "Stellarview", "Cosmovision", "Spaceprobe", "Skytracker", "Starnavigator", "Orbitaltracker", "Galaxynav", "Lunarprobe", "Skyexplorer", "Solarprobe", "Stellarprobe", "Cosmoseeker", "Spaceguard", "Skywatchman", "Starpatrol", "Orbitalguard", "Galaxysentry", "Lunarwatch", "Skymonitor", "Solarsentry", "Stellarwatch", "Cosmoguard", "Spacehawk", "Skyseeker", "Starfinder", "Orbitalstar", "Galaxar", "Lunarion", "Skygazer", "Solarscan", "Stellaright", "Cosmovoyage" };
        return spaceShipNames[UnityEngine.Random.Range(0, spaceShipNames.Length)];
    }
}
