using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Aicontroller : MonoBehaviour
{
    [SerializeField] GameObject BrickParent;
    public List<GameObject> Bricks = new List<GameObject>();
    [SerializeField] float radius;
    [SerializeField] Transform CollecteableMainParent;
    [SerializeField] GameObject PrevObject;
    public List<GameObject> CollectedBricks = new List<GameObject>();

    private Animator animator;
    private NavMeshAgent agent;
    private bool HaveBrick = false;
    private Vector3 Agenttargettransform;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < BrickParent.transform.childCount; i++)
        {
            Bricks.Add(BrickParent.transform.GetChild(i).gameObject);
        }
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!HaveBrick&& Bricks.Count>0)
        {

        }
    }
    void PickBricks()
    {
        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, radius);
        List<Vector3> ourcolors = new List<Vector3>();
        for (int i = 0; i < hitcolliders.Length; i++)
        {
            if (hitcolliders[i].gameObject.tag == "Green")
            {
                ourcolors.Add(hitcolliders[i].transform.position);
            }
        }
        if (ourcolors.Count>0)
        {
            Agenttargettransform = ourcolors[0];
        }
        else
        {
            int random = Random.Range(0, Bricks.Count);
            Agenttargettransform = Bricks[random].transform.position;
        }
        agent.SetDestination(Agenttargettransform);
        if (animator.GetBool("running"))
        {
            animator.SetBool("running", true);
        }
        HaveBrick = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Green")
        {
            other.transform.SetParent(CollecteableMainParent);
            Vector3 pos = PrevObject.transform.localPosition;
            pos.y += 0.08719585f;
            pos.z = 0;
            pos.x = 0;
            other.transform.localRotation = new Quaternion(0, 0, 0,0);
            other.transform.DOLocalMove(pos, 0.2f);
            PrevObject = other.gameObject;
            CollectedBricks.Add(other.gameObject);
            Bricks.Remove(other.gameObject);
            other.tag = "Untagged";
            HaveBrick = false;

        }
    }
}
