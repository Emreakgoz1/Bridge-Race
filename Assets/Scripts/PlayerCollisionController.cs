using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerCollisionController : MonoBehaviour
{
    public GameObject targetsParent;
    public List<GameObject> targets = new List<GameObject>();
    public Transform collectingObject;
    public GameObject prevObject;
    public List<GameObject> Cubes = new List<GameObject>();
    public List<GameObject> BridgeBricks = new List<GameObject>();
    private Vector3 originalPos;


    // Start is called before the first frame update
    void Start()
    {
        
        originalPos = prevObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider target)
    
    {
        if (target.gameObject.tag == "Blue")
        {
            target.transform.SetParent(collectingObject);
            Vector3 pos = prevObject.transform.localPosition;
            pos.y += 0.1f;
            pos.z = 0;
            pos.x = 0;
            target.transform.localRotation = new Quaternion(0, 0, 0, 0);
            target.transform.DOLocalMove(pos, 0.2f);
            prevObject = target.gameObject;
            Cubes.Add(target.gameObject);

            target.gameObject.tag = "Untagged";
        }
        if (target.gameObject.tag == "Merdiven")
        {
            for (int i = 0; i < Cubes.Count; i++)
            {
                GameObject bricks = BridgeBricks[i];
                bricks.SetActive(true);
                GameObject destroyedbricks = Cubes[i];
                destroyedbricks.SetActive(false);

                
            }
            prevObject.transform.position = originalPos;

        }
        if (target.gameObject.tag=="DropChecker")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
        if (target.gameObject.tag =="LevelEnd")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }

    }
    

    
}
