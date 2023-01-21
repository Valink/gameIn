using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] SondeBehaviour player;
    [SerializeField] GameObject ennemiPrefab;
    [SerializeField] GameObject friendPrefab;
    [SerializeField] List<GameObject> spawnedFriends;
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
            e.GetComponent<Enemy>().Init(player.gameObject);
        }
        else
        {
            var f = Instantiate(friendPrefab, position, Quaternion.identity);
            var s = f.GetComponent<Satellite>();

            spawnedFriends.Add(f);

            f.name = GetRandomSpaceName();

            s.Init(player);

            keywordsDetector.friendNames.Add(f.name); // TODO rm from list

            player.AddFriend(s);
        }
    }

    private string GetRandomSpaceName()
    {
        var spaceShipNames = new string[] { "COSMOSAT", "STARLINK", "SKYLARK", "ORBITALIS", "GALAXIA", "LUNARIX", "SKYWATCH", "SOLARIA", "STELLARIS", "COSMICON", "SPACEHAWK", "SKYSEEKER", "STARFINDER", "ORBITALSTAR", "GALAXAR", "LUNARION", "SKYGAZER", "SOLARSCAN", "STELLARIGHT", "COSMOVOYAGE", "SPACEBEAM", "SKYDOME", "STARSTREAM", "ORBITALBEAM", "GALAXYLIGHT", "LUNARVIEW", "SKYOBSERVER", "SOLARVIEW", "STELLARVIEW", "COSMOVISION", "SPACEPROBE", "SKYTRACKER", "STARNAVIGATOR", "ORBITALTRACKER", "GALAXYNAV", "LUNARPROBE", "SKYEXPLORER", "SOLARPROBE", "STELLARPROBE", "COSMOSEEKER", "SPACEGUARD", "SKYWATCHMAN", "STARPATROL", "ORBITALGUARD", "GALAXYSENTRY", "LUNARWATCH", "SKYMONITOR", "SOLARSENTRY", "STELLARWATCH", "COSMOGUARD", "SPACEHAWK", "SKYSEEKER", "STARFINDER", "ORBITALSTAR", "GALAXAR", "LUNARION", "SKYGAZER", "SOLARSCAN", "STELLARIGHT", "COSMOVOYAGE", "SPACEBEAM", "SKYDOME", "STARSTREAM", "ORBITALBEAM", "GALAXYLIGHT", "LUNARVIEW", "SKYOBSERVER", "SOLARVIEW", "STELLARVIEW", "COSMOVISION", "SPACEPROBE", "SKYTRACKER", "STARNAVIGATOR", "ORBITALTRACKER", "GALAXYNAV", "LUNARPROBE", "SKYEXPLORER", "SOLARPROBE", "STELLARPROBE", "COSMOSEEKER", "SPACEGUARD", "SKYWATCHMAN", "STARPATROL", "ORBITALGUARD", "GALAXYSENTRY", "LUNARWATCH", "SKYMONITOR", "SOLARSENTRY", "STELLARWATCH", "COSMOGUARD", "SPACEHAWK", "SKYSEEKER", "STARFINDER", "ORBITALSTAR", "GALAXAR", "LUNARION", "SKYGAZER", "SOLARSCAN", "STELLARIGHT", "COSMOVOYAGE" };
        return spaceShipNames[UnityEngine.Random.Range(0, spaceShipNames.Length)];
    }
}
