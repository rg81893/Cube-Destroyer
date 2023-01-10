using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spawner : MonoBehaviour
{
    [SerializeField] int maxObjectsToSpawn;
    [SerializeField] List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] float spawnSpeed;
    [SerializeField] float distanceSpawn;
    [SerializeField] float cubeSpeed;
    float time = 0f;
    List<GameObject> inactiveGameObjectsList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxObjectsToSpawn; i++)
        {
            SpawnRandomObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnSpeed < 0f) return;
        time += Time.deltaTime;
        if (time >= 1 / spawnSpeed)
        {
            //SpawnRandomObject();
            //CubeActivate();
            if (inactiveGameObjectsList.Count > 0)
            {
                inactiveGameObjectsList[0].SetActive(true);
                inactiveGameObjectsList.RemoveAt(0);
            }
            time = 0f;
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject target = hit.transform.gameObject;
                //Destroy(target);
                for (int i = 0; i < maxObjectsToSpawn; i++)
                {
                    if (gameObjects[i] == target)
                    {
                        target.SetActive(false);
                        inactiveGameObjectsList.Add(target);
                    }
                }
            }
        }
    }

    void CubeActivate()
    {
        var inactive = gameObjects.Where(g => g.activeInHierarchy == false).FirstOrDefault();
        if(inactive != null)
            inactive.gameObject.SetActive(true);
    }

    void SetupObject(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = Random.ColorHSV();
        obj.GetComponent<Rigidbody>().AddForce(-transform.forward * cubeSpeed, ForceMode.Impulse);
        // ajouter un tag à l'objet
    }

    void SpawnRandomObject()
    {
        float x = Random.Range(0.05f, 0.95f);
        float y = Random.Range(0.05f, 0.95f);
        Vector3 pos = new Vector3(x, y, 10.0f);
        pos = Camera.main.ViewportToWorldPoint(pos);
        Vector3 objectPosition = transform.TransformPoint(pos);
        GameObject objectNew = Instantiate(objectToSpawn, new Vector3(objectPosition.x, objectPosition.y, distanceSpawn), Quaternion.identity) as GameObject;
        objectNew.GetComponent<Renderer>().material.color = Random.ColorHSV();
        objectNew.GetComponent<Rigidbody>().AddForce(-transform.forward * cubeSpeed, ForceMode.Impulse);
        SetupObject(objectNew);
        gameObjects.Add(objectNew);
    }
}
