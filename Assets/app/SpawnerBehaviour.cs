using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour player;
    [SerializeField] private Transform ennemies;
    [SerializeField] private GameObject ennemyPrefab;
    [SerializeField] public List<GameObject> spawnedEnnemies;
    [SerializeField] private Transform units;
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] public List<GameObject> spawnedUnits;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private TypedWordDetector keywordsDetector;
    [SerializeField] public static SpawnerBehaviour Instance;
    private static List<string> spaceShipNames = new List<string> { "Agila", "Akatsuki", "Alouette", "AnikA1", "ANS", "ANTELSat", "Apollo", "ApolloSoyouz", "APPLE", "ArabsatA", "Ariel", "Aryabhata", "Asterix", "AstraA", "ASTRO", "ATS", "Azur", "BADRA", "BeidouA", "Beresheet", "BoeingX", "Brazilsat", "CALIPSO", "CASB", "CassiniHuygens", "Chandra", "Chandrayaan", "Change", "CHEOPS", "Clementine", "CloudSat", "COBE", "Compton", "Copernicus", "CoRoT", "Cosmos", "COTS", "CourierB", "Curiosity", "Dawn", "DeepImpact", "DeepSpace", "DiademeD1C", "Diapason", "Discoverer", "DongFangHongI", "DoubleStarTC", "EarlyBird", "EchoA", "EFT", "Elektron", "ENVISAT", "EoleCasA", "ERG", "ExoMarsTraceGasOrbiter", "ExoMarsEDM", "EXOSAT", "Explorer", "Fermi", "Formosat", "Gaia", "Galileo", "GalileoPFM", "Gamma", "Gemini", "Genesis", "Ginga", "Giotto", "Glory", "GRAB", "GRACE", "GRAIL", "Granat", "Hakucho", "Hayabusa", "HEAO", "Helios", "Hermes", "Herschel", "Hipparcos", "Hiten", "HotBird", "Hubble", "HXMT", "Ibuki", "ICESat", "IKBulgaria", "IMAGE", "Insight", "INTASAT", "InternationalUltravioletExplorer", "IRAS", "ISISI", "ISS", "Jason", "JulesVerne", "Juno", "Kepler", "Kwangmyongsong", "LISAPathfinder", "Luna", "LunarOrbiter", "LunarProspector", "Lusat", "Magellan", "Magion", "Mariner", "mars", "MarsClimateOrbiter", "MarsExpress", "MarsGlobalSurveyor", "MarsObserver", "MarsOdyssey", "MarsOrbiterMission", "MarsPathfinder", "MarsPolarLander", "MarsReconnaissanceOrbiter", "MAVEN", "Measat", "MeghaTropiques", "Mercury", "MercuryAtlas", "MercuryRedstone", "MESSENGER", "Meteosat", "MIDAS", "Mir", "Molniya", "MorelosA", "MOST", "MSAT", "MVL", "Navstar", "NEARShoemaker", "NewHorizons", "Nilesat", "Nimbus", "Nimiq", "Nozomi", "OAO", "OCO", "Ofek", "OGO", "Omid", "Opportunity", "OrbcommOG2", "Origine", "Orsted", "OSCAR", "OSIRISREx", "OSO", "Osumi", "PalapaA1", "Pegasus", "Phobos", "PhobosGrunt", "Phoenix", "Pioneer", "PioneerVenus", "Planck", "Polyot", "POSAT", "ProsperoX", "Proton", "QB50p1", "Radarsat", "RadioAstron", "Ranger", "Ratsat", "Relay", "RocketLabLaunchComplex", "RohiniB", "Rosat", "Rosetta", "Sakigake", "Saliout", "SanMarco", "Satcom", "SBS", "SCORE", "SELENE", "SentinelleA", "Shenzhou", "SIRIO", "Skylab", "SMAP", "SMART", "SOHO", "SolarDynamicsObservatory", "SolarMaximumMission", "SoyouzMS", "Spirit", "SPOT", "Spoutnik", "Starlink", "STENTOR", "STS", "STSATC", "SUNSAT", "Surveyor", "SWIFT", "Symphonie", "Syncom", "Syracuse", "Telecom", "Telstar", "TESS", "Thaicom", "ThorA", "Thuraya", "Tiangong", "TigriSat", "TIROS", "Transit", "TRICOMR", "TurkmenAlem52E", "MonacoSAT", "TurksatB", "Uhuru", "Ulysses", "Uribyol", "Vanguard", "Vega", "Venera", "Viking", "VINASAT", "Voskhod", "Vostok", "Voyager", "Westar", "WISE", "WMAP", "WRESAT", "XMMNewton", "Yohkoh", "Zenit", "Zhangheng", "Zond" };
    [SerializeField] private float spawnPeriod = 2f;
    [SerializeField] private int spawnCount = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartGame()
    {
        StartCoroutine("SpawnEntities");
    }

    IEnumerator SpawnEntities()
    {
        for (; ; )
        {
            SpawnRadomlyOutsideOfView();
            yield return new WaitForSeconds(spawnPeriod);
            spawnCount++;

            if (spawnCount % 10 == 0 && GameManager.Instance.Level < 10)
            {
                GameManager.Instance.IncrementLevel();
            }
        }
    }

    private void SpawnRadomlyOutsideOfView()
    {
        var topRightPointPosition = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        var bottomLeftPointPosition = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        var width = topRightPointPosition.x - bottomLeftPointPosition.x;
        var height = topRightPointPosition.y - bottomLeftPointPosition.y;
        var perimeter = height + width + height;

        var randPerimeterValue = UnityEngine.Random.Range(0, perimeter);

        var xPositionOffset = width / 2;
        var yPositionOffset = height / 2;

        var spawnableSize = 1;

        var position = Vector2.zero;
        if (randPerimeterValue <= height) // is on border left
        {
            position = new Vector2(-(width / 2 + spawnableSize), randPerimeterValue);
        }
        else if (randPerimeterValue <= height + width) // is on border top
        {
            position = new Vector2(randPerimeterValue - height, height / 2 + spawnableSize);
        }
        else // is on border righ
        {
            position = new Vector2(width / 2 + spawnableSize, randPerimeterValue - height - width);
        }
        SpawnEntity(position);
    }

    private void SpawnEntity(Vector2 position)
    {
        if (GameManager.Instance.GetGameOverState()) return;

        var isEnnemy = UnityEngine.Random.Range(0, 2) == 0;
        
        if (isEnnemy)
        {
            var e = Instantiate(ennemyPrefab, ennemies);
            spawnedEnnemies.Add(e);
            e.transform.position = position;
            e.GetComponent<EnnemyBehaviour>().Init(player.gameObject);
        }
        else
        {
            var u = Instantiate(unitPrefab, units);
            u.transform.position = position;
            spawnedUnits.Add(u);
            u.name = GetRandomSpaceName();
            keywordsDetector.UnitBehaviourNames.Add(u.name); // TODO rm from list

            var unitBehaviour = u.GetComponent<UnitBehaviour>();
            unitBehaviour.Init(player.gameObject);
        }
    }

    private string GetRandomSpaceName()
    {
        var minLetterNumber = 0;
        var maxLetterNumber = 50;

        switch (GameManager.Instance.Level)
        {
            case 1:
                minLetterNumber = 0;
                maxLetterNumber = 5;
                break;
            case 2:
                minLetterNumber = 0;
                maxLetterNumber = 10;
                break;
            case 3:
                minLetterNumber = 0;
                maxLetterNumber = 15;
                break;
            case 4:
                minLetterNumber = 0;
                maxLetterNumber = 20;
                break;
            case 5:
                minLetterNumber = 0;
                maxLetterNumber = 50;
                break;
            case 6:
                minLetterNumber = 5;
                maxLetterNumber = 50;
                break;
            case 7:
                minLetterNumber = 10;
                maxLetterNumber = 50;
                break;
            case 8:
                minLetterNumber = 15;
                maxLetterNumber = 50;
                break;
            case 9:
                minLetterNumber = 20;
                maxLetterNumber = 50;
                break;
            case 10:
                minLetterNumber = 25;
                maxLetterNumber = 50;
                break;
        }

        var subSpaceShipNames = spaceShipNames.Where(n => n.Length >= minLetterNumber && n.Length <= maxLetterNumber).ToList();
        return subSpaceShipNames[UnityEngine.Random.Range(0, subSpaceShipNames.Count)];
    }

    public GameObject GetClosestEnnemyTo(UnitBehaviour unit) // TODO should move smwhere else
    {
        if (spawnedEnnemies.Count == 0) return null;

        GameObject closestEnnemy = null;
        var distance = 100f;

        foreach (var ennemy in spawnedEnnemies)
        {
            if (ennemy != null)
            {
                var distanceToCheck = Vector2.Distance(unit.transform.position, ennemy.transform.position);
                if (distanceToCheck < distance)
                {
                    distance = distanceToCheck;
                    closestEnnemy = ennemy;
                }
            }

        }

        return closestEnnemy;
    }

    public GameObject GetUnitByName(string unitName)
    {
        return spawnedUnits.Find(u => u.name.ToLower() == unitName.ToLower());
    }
}
