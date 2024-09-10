using UnityEngine;
using System.Collections;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public float wallSpeed = 5f;
    public float spawnXPosition = 10f;
    public float upperMinHeight = 1f;
    public float upperMaxHeight = 4f;
    public float lowerMinHeight = -4f;
    public float lowerMaxHeight = -1f;
    public float wallLifetime = 10f;
    public float minSpawnDelay = 4f;
    public float maxSpawnDelay = 4f;

    private bool spawnInUpperArea = true;
    private bool isGameOver = false;

    void Start()
    {
        StartCoroutine(SpawnWalls());
    }

    IEnumerator SpawnWalls()
    {
        while (!isGameOver)
        {
            float randomHeight;
            if (spawnInUpperArea)
            {
                randomHeight = Random.Range(upperMinHeight, upperMaxHeight);
            }
            else
            {
                randomHeight = Random.Range(lowerMinHeight, lowerMaxHeight);
            }

            Vector3 wallPosition = new Vector3(spawnXPosition, randomHeight, 0);
            GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.identity);

            Rigidbody2D rbWall = wall.GetComponent<Rigidbody2D>();
            rbWall.velocity = new Vector2(-wallSpeed, 0);
            Destroy(wall, wallLifetime);

            spawnInUpperArea = !spawnInUpperArea;

            float randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    public void StopSpawning()
    {
        isGameOver = true;
        StopAllWalls();
    }

    private void StopAllWalls()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        foreach (GameObject wall in walls)
        {
            Rigidbody2D rbWall = wall.GetComponent<Rigidbody2D>();
            if (rbWall != null)
            {
                rbWall.velocity = Vector2.zero;
                rbWall.isKinematic = true;
            }
        }
    }
}
