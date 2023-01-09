using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] float spawnSpeed;
    [SerializeField] float distanceSpawn;
    [SerializeField] float rayonPerimetre;
    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnSpeed < 0f) return;
        time += Time.deltaTime;
        //perimetre.transform.localScale = new Vector3(rayonPerimetre, 0.001f, rayonPerimetre);
        if (time >= 1 / spawnSpeed)
        {
            Vector3 worldPosition = ScreenPositionIntoWorld(
              // example screen center:
              new Vector2(Random.Range(0, Screen.width / 2), Random.Range(0, Screen.height / 2)),
              // distance into the world from the screen:
              10.0f
            );
            //Vector3 localPosition = Random.insideUnitSphere * rayonPerimetre;
            Vector3 objectPosition = transform.TransformPoint(worldPosition);
            SpawnRandomObject(objectPosition);
            time = 0f;
        }
    }
    public static Vector3 ScreenPositionIntoWorld(Vector2 screenPosition, float distance)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        return (ray.direction.normalized * distance);
    }
    void SpawnRandomObject(Vector3 objectPosition)
    {
        GameObject objectNew = Instantiate(objectToSpawn, new Vector3(objectPosition.x, objectPosition.y, distanceSpawn), Quaternion.identity) as GameObject;
        objectNew.GetComponent<Renderer>().material.color = Random.ColorHSV();

    }
}
