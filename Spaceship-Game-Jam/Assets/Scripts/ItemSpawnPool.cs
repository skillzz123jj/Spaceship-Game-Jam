using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One-shot spawner: places each configured item prefab at (up to) one unique spawn point
/// when the scene starts. No respawn, no weighting, no runtime updates.
/// Usage:
/// 1. Add this component to an empty GameObject (e.g. "ItemSpawner").
/// 2. Create child empty transforms (will auto‑register as spawn points) OR assign spawnPoints manually.
/// 3. Populate the Entries list with up to as many items (prefab + Item ScriptableObject).
/// 4. Optionally enable randomization.
/// </summary>
[DisallowMultipleComponent]
public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnEntry
    {
        [Header("References")]
        public Item item;             // ScriptableObject describing the item
        public GameObject prefab;     // Prefab that contains ItemPickUp + collider (isTrigger) + visuals

        [Header("Optional")]
        public Vector3 positionOffset;

        [HideInInspector] public GameObject spawnedInstance; // For debug / inspection
    }

    [Header("Spawn Points")]
    [Tooltip("If left empty, all direct children (and nested) except this root become spawn points.")]
    public Transform[] spawnPoints;

    [Header("Randomization")]
    [Tooltip("Randomize order of spawn points before assignment.")]
    public bool randomizeSpawnPoints = true;
    [Tooltip("Randomize order of items before spawning.")]
    public bool randomizeItemsOrder = false;

    [Header("Entries")]
    public SpawnEntry[] entries;

    [Header("Validation / Logging")]
    public bool logOnSpawn = true;

    private bool _spawned = false;

    private void Awake()
    {
        AutoCollectSpawnPointsIfEmpty();
    }

    private void Start()
    {
        SpawnAllOnce();
    }

    private void AutoCollectSpawnPointsIfEmpty()
    {
        if (spawnPoints != null && spawnPoints.Length > 0) return;

        var list = new List<Transform>();
        foreach (Transform t in GetComponentsInChildren<Transform>(true))
        {
            if (t != transform)
                list.Add(t);
        }
        spawnPoints = list.ToArray();
    }

    private void SpawnAllOnce()
    {
        if (_spawned) return;
        _spawned = true;

        if (entries == null || entries.Length == 0)
        {
            if (logOnSpawn) Debug.LogWarning("ItemSpawner: No entries to spawn.", this);
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            if (logOnSpawn) Debug.LogWarning("ItemSpawner: No spawn points found.", this);
            return;
        }

        // Build working lists
        var points = new List<Transform>(spawnPoints);
        var validEntries = new List<SpawnEntry>();
        foreach (var e in entries)
        {
            if (e == null || e.prefab == null)
            {
                if (logOnSpawn) Debug.LogWarning("ItemSpawner: Skipping entry with missing prefab.", this);
                continue;
            }
            validEntries.Add(e);
        }

        if (validEntries.Count == 0)
        {
            if (logOnSpawn) Debug.LogWarning("ItemSpawner: No valid entries after filtering.", this);
            return;
        }

        // Randomize orders if requested
        if (randomizeSpawnPoints) Shuffle(points);
        if (randomizeItemsOrder) Shuffle(validEntries);

        int spawnCount = Mathf.Min(validEntries.Count, points.Count);
        for (int i = 0; i < spawnCount; i++)
        {
            var entry = validEntries[i];
            var point = points[i];
            Vector3 pos = point.position + entry.positionOffset;
            Quaternion rot = point.rotation;

            GameObject go = Instantiate(entry.prefab, pos, rot);
            entry.spawnedInstance = go;

            // Ensure ItemPickUp has the correct Item assigned
            var pickup = go.GetComponent<ItemPickUp>();
            if (pickup != null && entry.item != null)
            {
                pickup.item = entry.item;
            }

            if (logOnSpawn)
            {
                string itemName = entry.item != null ? entry.item.itemName : "(No Item SO)";
                Debug.Log($"ItemSpawner: Spawned {itemName} at {point.name}", go);
            }
        }

        if (spawnCount < validEntries.Count && logOnSpawn)
        {
            Debug.LogWarning($"ItemSpawner: Not enough spawn points ({points.Count}) for all items ({validEntries.Count}). Extra items skipped.", this);
        }
        else if (spawnCount < points.Count && logOnSpawn)
        {
            Debug.Log($"ItemSpawner: {points.Count - spawnCount} spawn points remained unused.", this);
        }
    }

    private static void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int k = Random.Range(0, i + 1);
            (list[i], list[k]) = (list[k], list[i]);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // Show spawn points as wire spheres
        Gizmos.color = Color.cyan;
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            foreach (var t in spawnPoints)
            {
                if (t == null) continue;
                Gizmos.DrawWireSphere(t.position, 0.35f);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (child == transform) continue;
                Gizmos.DrawWireSphere(child.position, 0.35f);
            }
        }
    }
#endif
}
