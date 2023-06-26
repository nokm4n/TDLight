using UnityEngine;

public class Grabber : MonoBehaviour {

    public LayerMask layerMask;
    public float raycastDistance = 10f;
    private Transform originTransform;

    private Node curNode;
    public Node prevNode;
    private Turret curTurret;

    public bool isActive = true;
    public float dist = 0.9f;



    public static Grabber instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Grabber in scene!");
            return;
        }
        instance = this;
    }
    private void Update() 
    {
        if (!isActive) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (curTurret != null) return;
            RaycastHit hit = CastRay();
            if (hit.collider != null)
            {
                prevNode = hit.collider.gameObject.GetComponent<Node>();
                originTransform = prevNode.transform;
                curTurret = prevNode.CmdTakeAndNullTurret();
            }
        }
        if(curTurret != null)
        {
            CheckSnap();
        }
      

        if (Input.GetMouseButtonUp(0))
        {
            if (curTurret == null) return;
            
            if (Vector3.Distance(curNode.transform.position, curTurret.transform.position) < dist && !curNode.GetEmpty() && curNode.turret.turretLvl == curTurret.turretLvl)
            {
                //CheckQuest();
                Destroy(curTurret.gameObject);
                LevelManager.instance.Upgrade(curNode);
            }
            else if (Vector3.Distance(curNode.transform.position, curTurret.transform.position) < dist && curNode.GetEmpty())
            {
                
                originTransform = curNode.transform;
                curNode.SetTurret(curTurret);
            }
            else
            {
                prevNode.SetTurret(curTurret);
            }
            curTurret.transform.position = originTransform.position;
            curTurret = null;
            curNode = null;
        }

        if (curTurret != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(curTurret.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            if (worldPosition.z > -2.2f)
            {
                worldPosition.z = -2.2f;
            }
            curTurret.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);

        }
    }

    private RaycastHit CastRay() {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, raycastDistance, layerMask);

        return hit;
    }

    private void CheckSnap()
    {
        Node node = LevelManager.instance.GetClosestNode(curTurret.transform);
        curNode = node;
    }

    /*private void CheckQuest()
    {
        int index = 0;
        if (QuestList.instance.CheckQuest(Quest.QuestType.MergeTowers, out index))
        {
            QuestList.instance.currentQuests[index].curAmount++;
            Debug.Log("+merge");
        }
        if (QuestList.instance.CheckQuest(Quest.QuestType.GetTowerLvl, out index))
        {
            // find max turret lvl
           // QuestList.instance.currentQuests[index].curAmount++;
            //Debug.Log("+enemy");
        }
        Debug.Log(index);
    }*/
}
