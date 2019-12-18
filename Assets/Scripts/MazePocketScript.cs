using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;


public class MazePocketScript : MonoBehaviour
{
    [SerializeField]
    TextAsset csv;

    [SerializeField]
    private Light lightToFollow;
    [SerializeField]
    Camera minimapCamera;

    [SerializeField]
    private GameObject mazeObject;
    [SerializeField]
    private GameObject hallway;
    [SerializeField]
    private GameObject lJunction;
    [SerializeField]
    private GameObject tJunction;
    [SerializeField]
    private GameObject plusJunction;
    [SerializeField]
    private GameObject hallCap;
    [SerializeField]
    private GameObject endHallway;

    [SerializeField]
    private GameObject jeffrey;

    [SerializeField]
    private GameObject multiplayerManager;

    private GameObject player1;
    private GameObject player2;
    private bool player2Found;

    private bool[,] mazeArray;
    private List<Vector3> enemyList;

    private bool jeffreySpawned = false;

    void Start()
    {
        player2Found = false;
        enemyList = new List<Vector3>();

        if (PhotonNetwork.IsMasterClient)
        {
            generateMaze();
            GameObject lightGameObject = new GameObject("Light for Controller");
            Light lightComp = lightGameObject.AddComponent<Light>();
            lightComp.type = UnityEngine.LightType.Directional;
            lightComp.transform.rotation = Quaternion.Euler(90, 0, 0);
            lightToFollow.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!player2Found)
        {
            if (player2 = GameObject.Find("Player2"))
            {
                player1 = GameObject.Find("Player1");
                player1.AddComponent<MazePocketPlayerScript>();
                player2.AddComponent<MazePocketPlayerScript>();

                //Make it dark
                UnityEngine.RenderSettings.ambientIntensity = 0;
                UnityEngine.RenderSettings.reflectionIntensity = 0;

                //Add the ambient light to the player in the maze
                lightToFollow.transform.parent = player2.transform;

                //Darken the skybox for the player in the maze
                Camera player2Camera = player2.transform.GetChild(0).GetComponent<Camera>();
                player2Camera.clearFlags = CameraClearFlags.SolidColor;
                player2Camera.backgroundColor = Color.black;

                player2Found = true;
            }
        } else
        {
            if(lightToFollow.transform.parent == null)
            {
                if(player2 = GameObject.Find("Player2"))
                {
                    lightToFollow.transform.parent = player2.transform;

                    Camera player2Camera = player2.transform.GetChild(0).GetComponent<Camera>();
                    player2Camera.clearFlags = CameraClearFlags.SolidColor;
                    player2Camera.backgroundColor = Color.black;

                    player2.AddComponent<MazePocketPlayerScript>();
                }
            }
        }
    }

    private void generateMaze()
    {
        //Get the CSV text to generate maze
        string fullString;
        fullString = csv.text;

        Vector3 position = new Vector3(0, 0, 0);

        float maxX = 0, maxZ = 0;

        //Delimit CSV text
        string[] delimited = fullString.Split('\n');
        string[] row = delimited[0].Split(',');

        GameObject currentObject = new GameObject();

        mazeArray = new bool[row.GetLength(0), delimited.GetLength(0) - 1];

        for (int i = 0; i < delimited.GetLength(0) - 1;)
        {
            for (int j = 0; j < row.GetLength(0); j++)
            {
                //Horizontal Hallway
                if (row[j] == "_" || row[j] == "_\r")
                {
                    currentObject = PhotonNetwork.Instantiate(hallway.name, position, Quaternion.Euler(0, 90, 0));
                }
                //Vertical Hallway
                else if (row[j] == "|" || row[j] == "|\r")
                {
                    currentObject = PhotonNetwork.Instantiate(hallway.name, position, Quaternion.Euler(0, 0, 0));
                }
                //Up-Right L Junction
                else if (row[j] == "L" || row[j] == "L\r")
                {
                    currentObject = PhotonNetwork.Instantiate(lJunction.name, position, Quaternion.Euler(0, -90, 0));
                }
                //Up-Left L Junction
                else if (row[j] == ">" || row[j] == ">\r")
                {
                    currentObject = PhotonNetwork.Instantiate(lJunction.name, position, Quaternion.Euler(0, 180, 0));
                }
                //Down-Right L Junction
                else if (row[j] == "<" || row[j] == "<\r")
                {
                    currentObject = PhotonNetwork.Instantiate(lJunction.name, position, Quaternion.Euler(0, 0, 0));
                }
                //Down-Left L Junction
                else if (row[j] == "7" || row[j] == "7\r")
                {
                    currentObject = PhotonNetwork.Instantiate(lJunction.name, position, Quaternion.Euler(0, 90, 0));
                }
                //Left-Right-Up T Junction
                else if (row[j] == "1" || row[j] == "1\r")
                {
                    currentObject = PhotonNetwork.Instantiate(tJunction.name, position, Quaternion.Euler(0, -90, 0));
                }
                //Left-Right-Down T Junction
                else if (row[j] == "T" || row[j] == "T\r")
                {
                    currentObject = PhotonNetwork.Instantiate(tJunction.name, position, Quaternion.Euler(0, 90, 0));
                }
                //Right-Up-Down T Junction
                else if (row[j] == "E" || row[j] == "E\r")
                {
                    currentObject = PhotonNetwork.Instantiate(tJunction.name, position, Quaternion.Euler(0, 0, 0));
                }
                //Left-Up-Down T Junction
                else if (row[j] == "3" || row[j] == "3\r")
                {
                    currentObject = PhotonNetwork.Instantiate(tJunction.name, position, Quaternion.Euler(0, 180, 0));
                }
                //Plus Junction
                else if (row[j] == "+" || row[j] == "+\r")
                {
                    currentObject = PhotonNetwork.Instantiate(plusJunction.name, position, Quaternion.Euler(0, 0, 0));
                }
                //Right Facing Hallway Cap
                else if (row[j] == "\\" || row[j] == "\\\r")
                {
                    currentObject = PhotonNetwork.Instantiate(hallCap.name, position, Quaternion.Euler(0, 90, 0));
                    //If Jeffrey hasn't been spawned and we aren't at the first position, spawn Jeffrey
                    if (!jeffreySpawned)
                    {
                        if (position != new Vector3(0, 0, 0))
                        {
                            //Add the position to our enemyList to instantiate later
                            enemyList.Add(new Vector3(position.x, position.y + 4.5f, position.z));
                            jeffreySpawned = true;
                        }
                    }
                    //Otherwise, 30% chance to spawn another Jeffrey
                    else
                    {
                        int rand = Random.Range(0, 10);
                        if (rand <= 2)
                        {
                            //Add the position to our enemyList to instantiate later
                            enemyList.Add(new Vector3(position.x, position.y + 4.5f, position.z));
                        }
                    }
                }
                //Left Facing Hallway Cap
                else if (row[j] == "/" || row[j] == "/\r")
                {
                    currentObject = PhotonNetwork.Instantiate(hallCap.name, position, Quaternion.Euler(0, -90, 0));
                    //If Jeffrey hasn't been spawned and we aren't at the first position, spawn Jeffrey
                    if (!jeffreySpawned)
                    {
                        if(position != new Vector3(0, 0, 0))
                        {
                            //Add the position to our enemyList to instantiate later
                            enemyList.Add(new Vector3(position.x, position.y + 4.5f, position.z));
                            jeffreySpawned = true;
                        }
                    }
                    //Otherwise, 30% chance to spawn another Jeffrey
                    else
                    {
                        int rand = Random.Range(0, 10);
                        if (rand <= 2)
                        {
                            //Add the position to our enemyList to instantiate later
                            enemyList.Add(new Vector3(position.x, position.y + 4.5f, position.z));
                        }
                    }
                }
                //Top Facing Hallway Cap
                else if (row[j] == "=" || row[j] == "=\r")
                {
                    currentObject = PhotonNetwork.Instantiate(hallCap.name, position, Quaternion.Euler(0, 0, 0));
                    //If Jeffrey hasn't been spawned and we aren't at the first position, spawn Jeffrey
                    if (!jeffreySpawned)
                    {
                        if (position != new Vector3(0, 0, 0))
                        {
                            //Add the position to our enemyList to instantiate later
                            enemyList.Add(new Vector3(position.x, position.y + 4.5f, position.z));
                            jeffreySpawned = true;
                        }
                    }
                    //Otherwise, 30% chance to spawn another Jeffrey
                    else
                    {
                        int rand = Random.Range(0, 10);
                        if (rand <= 2)
                        {
                            //Add the position to our enemyList to instantiate later
                            enemyList.Add(new Vector3(position.x, position.y + 4.5f, position.z));
                        }
                    }
                }
                //Bottom Facing Hallway Cap
                else if (row[j] == ";" || row[j] == ";\r")
                {
                    currentObject = PhotonNetwork.Instantiate(hallCap.name, position, Quaternion.Euler(0, 180, 0));
                    //If Jeffrey hasn't been spawned and we aren't at the first position, spawn Jeffrey
                    if (!jeffreySpawned)
                    {
                        if (position != new Vector3(0, 0, 0))
                        {
                            //Add the position to our enemyList to instantiate later
                            enemyList.Add(new Vector3(position.x, position.y + 4.5f, position.z));
                            jeffreySpawned = true;
                        }
                    }
                    //Otherwise, 30% chance to spawn another Jeffrey
                    else
                    {
                        int rand = Random.Range(0, 10);
                        if (rand <= 2)
                        {
                            //Add the position to our enemyList to instantiate later
                            enemyList.Add(new Vector3(position.x, position.y + 4.5f, position.z));
                        }
                    }

                }
                //Ending Hallway
                else if(row[j] == "!" || row[j] == "!\r")
                {
                    currentObject = PhotonNetwork.Instantiate(endHallway.name, position, Quaternion.Euler(0, -90, 0));
                }
                currentObject.transform.parent = mazeObject.transform;
                position.x -= 9f;
            }
            row = delimited[++i].Split(',');
            maxZ = position.z;
            position.z += 9f;
            maxX = position.x;
            position.x = 0f;
        }
        //Spawn our list of Jeffreys
        spawnEnemies();

        //Set the camera size to be equal to either maxX*maxX or maxZ*maxZ
        minimapCamera.orthographicSize = maxX > maxZ ? maxX : maxZ;

        maxX /= 2;
        maxZ /= 2;

        //Set camera for minimap to be in the middle of the maze
        minimapCamera.transform.position = new Vector3(maxX, minimapCamera.transform.position.y, maxZ);
        GameObject p2 = GameObject.Find("Player2");
        if(p2 != null)
        {
            PhotonNetwork.Destroy(p2);
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Reinstantiating the player");
                PhotonNetwork.Instantiate("PlayerCharacter", new Vector3(0, 2f, 0), new Quaternion());
            }
        }
    }

    private void spawnEnemies()
    {
        foreach(Vector3 pos in enemyList)
        {
            PhotonNetwork.Instantiate(jeffrey.name, pos, Quaternion.Euler(0, 0, 0));
        }
    }

    public void loadNewLevel(string levelName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Changing Levels");
            player1 = GameObject.Find("Player1");
            player1.GetPhotonView().RPC("Destroy", RpcTarget.All);
            PhotonNetwork.LoadLevel(levelName);
        }
    }
}
