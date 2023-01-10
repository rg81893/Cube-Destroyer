using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] float spawnSpeed;
    [SerializeField] float distanceSpawn;
    [SerializeField] float cubeSpeed;
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
            float x = Random.Range(0.05f, 0.95f);
            float y = Random.Range(0.05f, 0.95f);
            Vector3 pos = new Vector3(x, y, 10.0f);
            pos = Camera.main.ViewportToWorldPoint(pos);

            //Vector3 worldPosition = ScreenPositionIntoWorld(
            //  // example screen center:
            //  new Vector2(x, y),
            //  // distance into the world from the screen:
            //  distanceSpawn
            //);
            //Vector3 localPosition = Random.insideUnitSphere * rayonPerimetre;
            Vector3 objectPosition = transform.TransformPoint(pos);
            SpawnRandomObject(objectPosition);
            time = 0f;
        }

        if(Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject target = hit.transform.gameObject;
                Destroy(target);
            }
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
        //objectNew.GetComponent<Rigidbody>().velocity = cubeSpeed * GetComponent<Rigidbody>().velocity.normalized;
        objectNew.GetComponent<Rigidbody>().AddForce(-transform.forward * cubeSpeed, ForceMode.Impulse);
    }
}
