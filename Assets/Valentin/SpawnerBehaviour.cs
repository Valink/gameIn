using System.Collections;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject ennemiPrefab;
    [SerializeField] GameObject friendPrefab;
    [SerializeField] Camera gameCamera;
    [SerializeField] TypedWordDetector keywordsDetector;

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
        var topRightPointPosition = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        var bottomLeftPointPosition = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        var width = topRightPointPosition.x - bottomLeftPointPosition.x;
        var height = topRightPointPosition.y - bottomLeftPointPosition.y;
        var perimeter = width * 2 + height * 2;

        var randPerimeterValue = UnityEngine.Random.Range(0, perimeter);

        var xPositionOffset = width / 2;
        var yPositionOffset = height / 2;

        var spawnableSize = 1;

        var position = Vector2.zero;
        if (randPerimeterValue <= width) // is on border top
        {
            position = new Vector2(randPerimeterValue - xPositionOffset, height / 2 + spawnableSize);
        }
        else if (randPerimeterValue <= width + height) // is on border right
        {
            position = new Vector2(width / 2 + spawnableSize, randPerimeterValue - width - yPositionOffset);
        }
        else if (randPerimeterValue <= width + height + width) // is on border bottom
        {
            position = new Vector2(randPerimeterValue - width - height - xPositionOffset, -(height / 2 + spawnableSize));
        }
        else // is on border left
        {
            position = new Vector2(-(width / 2 + spawnableSize), randPerimeterValue - width - height - width - yPositionOffset);
        }
        SpawnEntity(position);
    }

    private void SpawnEntity(Vector2 position)
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            var e = Instantiate(ennemiPrefab, position, Quaternion.identity);
            e.GetComponent<Enemy>().Init(player);
        }
        else
        {
            var f = Instantiate(friendPrefab, position, Quaternion.identity);
            f.name = GetRandomSpaceName();
            f.GetComponent<Satellite>().Init(player);
            keywordsDetector.friendNames.Add(f.name); // TODO rm from list
        }
    }

    private string GetRandomSpaceName()
    {
        var spaceShipNames = new string[] { "Cosmosat", "Starlink", "Skylark", "Orbitalis", "Galaxia", "Lunarix", "Skywatch", "Solaria", "Stellaris", "Cosmicon", "Spacehawk", "Skyseeker", "Starfinder", "Orbitalstar", "Galaxar", "Lunarion", "Skygazer", "Solarscan", "Stellaright", "Cosmovoyage", "Spacebeam", "Skydome", "Starstream", "Orbitalbeam", "Galaxylight", "Lunarview", "Skyobserver", "Solarview", "Stellarview", "Cosmovision", "Spaceprobe", "Skytracker", "Starnavigator", "Orbitaltracker", "Galaxynav", "Lunarprobe", "Skyexplorer", "Solarprobe", "Stellarprobe", "Cosmoseeker", "Spaceguard", "Skywatchman", "Starpatrol", "Orbitalguard", "Galaxysentry", "Lunarwatch", "Skymonitor", "Solarsentry", "Stellarwatch", "Cosmoguard", "Spacehawk", "Skyseeker", "Starfinder", "Orbitalstar", "Galaxar", "Lunarion", "Skygazer", "Solarscan", "Stellaright", "Cosmovoyage", "Spacebeam", "Skydome", "Starstream", "Orbitalbeam", "Galaxylight", "Lunarview", "Skyobserver", "Solarview", "Stellarview", "Cosmovision", "Spaceprobe", "Skytracker", "Starnavigator", "Orbitaltracker", "Galaxynav", "Lunarprobe", "Skyexplorer", "Solarprobe", "Stellarprobe", "Cosmoseeker", "Spaceguard", "Skywatchman", "Starpatrol", "Orbitalguard", "Galaxysentry", "Lunarwatch", "Skymonitor", "Solarsentry", "Stellarwatch", "Cosmoguard", "Spacehawk", "Skyseeker", "Starfinder", "Orbitalstar", "Galaxar", "Lunarion", "Skygazer", "Solarscan", "Stellaright", "Cosmovoyage" };
        return spaceShipNames[UnityEngine.Random.Range(0, spaceShipNames.Length)];
    }
}
