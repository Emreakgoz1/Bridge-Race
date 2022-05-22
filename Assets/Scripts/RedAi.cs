using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class RedAi: MonoBehaviour
{
    public GameObject targetsParent;
    public List<GameObject> targets = new List<GameObject>();
    public float radius;
    public Transform toplanacaklaranaobjesi;
    public GameObject prevObject;
    public List<GameObject> Cubes = new List<GameObject>();

    private Animator animator;
    private NavMeshAgent agent;
    private bool haveTarget = false;
    private Vector3 TargetTransform;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < targetsParent.transform.childCount; i++)
        {
            targets.Add(targetsParent.transform.GetChild(i).gameObject);
        }
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();



    }

    // Update is called once per frame
    void Update()
    {
        if (!haveTarget && targets.Count>0)
        {
            ChooseTarget();
        }
    }
    void ChooseTarget()
    {
        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, radius);
        List<Vector3> ourColors = new List<Vector3>();
        for (int i = 0; i < hitcolliders.Length; i++)
        {
            if (hitcolliders[i].gameObject.tag=="Red")
            {
                ourColors.Add(hitcolliders[i].transform.position);

            }
        }
        if (ourColors.Count>0)
        {
            TargetTransform = ourColors[0];
        }
        else
        {
            int random = Random.Range(0, targets.Count);
            TargetTransform = targets[random].transform.position;

        }
        agent.SetDestination(TargetTransform);
        if (animator.GetBool("running"))
        {
            animator.SetBool("running", true);

        }
        haveTarget = true;
    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag =="Red")
        {
            target.transform.SetParent(toplanacaklaranaobjesi);
            Vector3 pos = prevObject.transform.localPosition;
            pos.y += 0.1f;
            pos.z = 0;
            pos.x = 0;
            target.transform.localRotation = new Quaternion(0, 0, 0, 0);
            target.transform.DOLocalMove(pos, 0.2f);
            prevObject = target.gameObject;
            Cubes.Add(target.gameObject);
            targets.Remove(target.gameObject);
            target.tag = "Untagged";
            haveTarget = false;

        }
    }
}
