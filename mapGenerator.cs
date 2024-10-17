using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class mapGenerator : MonoBehaviour
{
    public mapGenModule[] rooms;
    public mapGenModule entrance;
    public int Iterations = 3;
    public List<GameObject> placedObjects;
    // Start is called before the first frame update
    void Start()
    {
        mapGenModule startModule = Instantiate(entrance, transform.position, transform.rotation);
        startModule.startingRoom = true;
        placedObjects.Add(startModule.gameObject);
        var toConnect = new List<ModuleConnector>(startModule.getConnectors());
        generateMap(toConnect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateMap(List<ModuleConnector> toConnect){
        for(int i = 0; i< Iterations; i++){
            
            var newConnectors = new List<ModuleConnector>();

            foreach( var connector in toConnect){
                var connectTag = getRandom(connector.tags);
                mapGenModule newPrefab = getRandomWithTag(rooms, connectTag);
                mapGenModule newRoom = Instantiate(newPrefab);
                ModuleConnector[] roomConnectors = newRoom.getConnectors();
                var connectorsToMatch = roomConnectors.FirstOrDefault( x =>x.IsDefault) ?? getRandom(roomConnectors);
                matchConnectors(connector, connectorsToMatch);
                newConnectors.AddRange(roomConnectors.Where(e=> e != connectorsToMatch));
            }
            toConnect = newConnectors;
        }
    }

    private static mapGenModule getRandomWithTag(IEnumerable<mapGenModule> modules, string tagToMatch){
        var matchingModules = modules.Where(m => m.tags.Contains(tagToMatch)).ToArray();
        return getRandom(matchingModules); 
    }
    private static TItem getRandom<TItem>(TItem[] array){
        return array[Random.Range(0, array.Length)];
    }

    private void matchConnectors(ModuleConnector currentConnection, ModuleConnector newConnection){
        var newModule = newConnection.transform.parent;               
        var forwardVectorToMatch = -currentConnection.transform.forward;  
        var correctiveRotation = signedRotationAngle(forwardVectorToMatch) - signedRotationAngle(newConnection.transform.forward);
        newModule.RotateAround(newConnection.transform.position, Vector3.up, correctiveRotation);
        var correctiveTranslation = currentConnection.transform.position - newConnection.transform.position;
        newModule.transform.position += correctiveTranslation;


    }
    static float signedRotationAngle(Vector3 vector){
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
}
