using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 0.02f;
    public float spawnDelay = 0.5f;

    private bool movingRight = true;
    private float xmax;
    private float xmin;



	// Use this for initialization
	void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

        xmax = rightBoundary.x;
        xmin = leftBoundary.x;

        //we are going over each of the gizmos (Position Gizmo) we planted on the screen under 
        //EnemySpawn object heirarchy

        SpawnUntilFull();
	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (movingRight)
        {
            transform.position += Vector3.right * speed;
        }
        else
        {
            transform.position += Vector3.left * speed;
        }
        float rightEdgeOfFormation = transform.position.x + (0.5f*width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        } else if(rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }

        if (AllMembersDead())
        {
            SpawnUntilFull();
            
            Debug.Log("All Dead");
        }
	}

    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    Transform NextFreePosition()
    {
        foreach(Transform child in transform)
        {
            if(child.childCount == 0)
            {
                return child;
            }
        }
        
        return null;
    }


    // we don't use this, instead use SpawnUntilFull. Here for reference of original spawn logic
    void Respawn()
    {
        //we are going over each of the gizmos (Position Gizmo) we planted on the screen under 
        //EnemySpawn object heirarchy
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
            enemy.transform.parent = child; //we want the current object to be child of its Position gizmo
                                            // this is for later ease of use. Else they become sibling of position.
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();

        //defensive checking
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.transform.position, Quaternion.identity);
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition()) //prevent calling every `spawndelay`
        {
            //delay through recursive call instead of loop
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

}
