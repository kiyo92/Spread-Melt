using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    public GameObject meltObject;
    
    public GameObject sphere;
    public Vector3 currentTouchPos;
    public IEnumerator placeCoroutine;
    private Transform tube;
    private LineRenderer tubeLine;
    public float timeToStart = 1;
    public Image countdownImage;
    // Start is called before the first frame update
    void Start()
    {
        tube = gameController.tube;
        tubeLine = tube.gameObject.GetComponent<LineRenderer>();
        tubeLine.material = tube.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.meltPhase)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, 1000))
            {
                currentTouchPos = hitData.point;
                tube.position = new Vector3(currentTouchPos.x, 4, currentTouchPos.z + 0.9f);

            }
        }
        
    }

    void OnMouseDown()
    {
        //TODO Implementar criar offset
        GameObject cloneObject = Instantiate(meltObject, new Vector3(currentTouchPos.x, gameController.SelectedObject.meltFactor, currentTouchPos.z + 0.9f), Quaternion.identity);
        cloneObject.GetComponent<MeltObject>().GC = gameController;
        placeCoroutine = PlaceObject(cloneObject.transform);
        timeToStart = 1;
        if (!gameController.meltPhase) {
            
            StartCoroutine(placeCoroutine);
        }
        
    }

    void OnMouseUp()
    {
        if (!gameController.meltPhase)
        {
            countdownImage.fillAmount = 0;
            tubeLine.enabled = false;
            StopCoroutine(placeCoroutine);
            
        }
        
    }


    IEnumerator PlaceObject(Transform parent) {


        while (true)
        {
            countdownImage.transform.position = new Vector3(currentTouchPos.x, gameController.SelectedObject.meltFactor, currentTouchPos.z + 0.9f);
            if (timeToStart >= 0)
            {
                timeToStart -= 0.01f;
                countdownImage.fillAmount = timeToStart;
                Color tempColor = tube.GetComponent<MeshRenderer>().material.color;
                tempColor.a = 1f;
                countdownImage.color = tempColor;
                
            }
            if (timeToStart < 0)
            {
                tubeLine.enabled = true;
                tubeLine.SetPosition(0, tube.GetChild(0).position);
                tubeLine.SetPosition(1, new Vector3(currentTouchPos.x, gameController.SelectedObject.meltFactor, currentTouchPos.z + 0.9f));
                GameObject clone = Instantiate(sphere, new Vector3(currentTouchPos.x, gameController.SelectedObject.meltFactor, currentTouchPos.z + 0.9f), Quaternion.identity, parent);

                
                clone.GetComponent<MeshRenderer>().material.color = gameController.SelectedObject.objectColor;
                
            }
            yield return new WaitForSeconds(0.01f);
        }
        
        
    }

}
