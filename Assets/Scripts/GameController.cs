using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private ObjectProperties _selectedObject;
    public Slider timeSlider;
    [SerializeField]
    private GameObject buttonsContainer;
    public ObjectProperties SelectedObject { get { return _selectedObject; } }
    public bool meltPhase;
    public Transform tube;
    // Start is called before the first frame update

    public void SelectObject(ObjectProperties selectedObject) {
        _selectedObject = selectedObject;
        tube.GetComponent<MeshRenderer>().material.color = selectedObject.objectColor;
    }

    public void SetMeltPhase() {
        timeSlider.gameObject.SetActive(true);
        buttonsContainer.SetActive(false);
        Destroy(tube.gameObject);
        meltPhase = true;
    }
}
