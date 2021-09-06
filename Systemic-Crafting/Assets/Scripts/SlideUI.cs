using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUI : MonoBehaviour
{
    [System.Serializable]
    public struct SlideData
    {
        public Transform startPoint;
        public Transform endPoint;
        public Transform slideObject;
    }

    [SerializeField] private string activationInput;
    [SerializeField] private List<SlideData> sliders = new List<SlideData>();
    [SerializeField] private float slideTime;
    [SerializeField] private AnimationCurve slideCurve;
    private float timer;
    private bool isActivated = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(activationInput))
        {
            isActivated = !isActivated;
        }

        if (isActivated && (timer < slideTime))
        {
            timer += Time.deltaTime;
        }
        else if (!isActivated && (timer > 0.0f))
        {
            timer -= Time.deltaTime;
        }

        Slide();
    }

    private void Slide()
    {
        foreach (SlideData slide in sliders)
        {
            slide.slideObject.position = Vector3.Lerp(slide.startPoint.position, slide.endPoint.position, slideCurve.Evaluate(timer / slideTime));
        }
    }
}
