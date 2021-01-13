using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionManager : MonoBehaviour
{
    public Vector3 endPos;
    public GameObject[] disappearOB;
    public GameObject anaMap;
    public GameObject defaultUI;
    public GameObject defaultButton;

    private Vector3 initial_position;
    private Vector3 target_position;
    public float duration = 1.5f;
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

    private bool process = true;
    private float position_t = 0f;

    private MotionController[] controllers;

    public void Start() {
        controllers = GetComponentsInChildren<MotionController>();
        initial_position = transform.localPosition;
    }

    public void PlayDefault()   { StartCoroutine(Default()); }

    public void PlaySetDef() { StartCoroutine(setDef()); }

    public void PlayUnfold()    { StartCoroutine(Unfold()); }

    public void DoAnatomize() { StartCoroutine(Anatomize()); }

    public void UnAnatomize() { StartCoroutine(unAnatomize()); }

    private IEnumerator Default() {
        StartCoroutine(RotationReset());


        gameObject.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(duration / 2f);

        for (int i = 0; i < controllers.Length; i++) {
            controllers[i].ProgressDefault();
            controllers[i].ProgressDragOff();
        }
    }

    private IEnumerator setDef()
    {
        StartCoroutine(RotationReset());


        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(duration / 2f);

        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].ProgressDefault();
            controllers[i].ProgressDragOn();
        }
    }

    private IEnumerator Unfold() {

        StartCoroutine(RotationReset());

        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(duration / 2f);

        for (int i = 0; i < controllers.Length; i++) {
            controllers[i].ProgressUnfold();
            controllers[i].ProgressDragOn();
        }
    }

    private IEnumerator Anatomize()
    {
        StartCoroutine(Default());

        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(duration / 2f);
        defaultUI.SetActive(false);
        defaultButton.SetActive(false);
        anaMap.SetActive(true);
        
        for (int i = 0; i < disappearOB.Length; i++)
        {
            disappearOB[i].GetComponent<MeshRenderer>().enabled = false;
        }

        for (int i = 0; i < controllers.Length; i++)
        {            
            controllers[i].ProgressDragOn();
        }
    }

    private IEnumerator unAnatomize()
    {
        StartCoroutine(Default());

        gameObject.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(duration / 2f);
        anaMap.SetActive(false);
        defaultUI.SetActive(true);
        defaultButton.SetActive(true);

        for (int i = 0; i < disappearOB.Length; i++)
        {
            disappearOB[i].GetComponent<MeshRenderer>().enabled = true;
        }

        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].ProgressDragOff();
        }
    }

    private IEnumerator RotationReset() {
        process = false;
        Quaternion start = transform.rotation;

        float entry_time = Time.time;
        while (Time.time - entry_time < duration) {

            float t = (Time.time - entry_time) / duration;

            t = curve.Evaluate(t);

            transform.rotation = Quaternion.Slerp(start, Quaternion.identity, t);

            yield return null;
        }

        transform.rotation = Quaternion.identity;
        process = true;
    }

    public void ResetPosition()
    {
        if (transform.localPosition == initial_position) return;

        target_position = transform.localPosition;

        if (process)
        {
            StartCoroutine(PositionReset(target_position, initial_position));
        }
    }

    public IEnumerator PositionReset(Vector3 start, Vector3 end)
    {
        process = false;
        // 중복 진입 방지
        float entry_time = Time.time;
        // 루프 시작 전에 최초 진입 시간 저장
        while (Time.time - entry_time < duration)
        {

            float t = (Time.time - entry_time) / duration;
            // (루프 진입 경과시간) / 애니메이션 길이

            t = curve.Evaluate(t);
            // 애니메이션 커브 반영

            transform.localPosition = Vector3.Lerp(start, end, t);
            // 선형 보간 값 position에 적용

            yield return null;
        }

        transform.localPosition = end;
        process = true;
    }

}
