using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour
{
    public Vector3 endPos;


    private Vector3 initial_position;
    private Vector3 target_position;


    public float duration = 3f;
    public AnimationCurve curve;

    private float position_t = 0f;

    private bool process = true;
    
    public void Start() {
        initial_position = transform.localPosition;
    }

    public void ProgressUnfold() {
        if (transform.localPosition != initial_position) return;


        target_position = initial_position + endPos;

        if (process) {
            StartCoroutine(RotationReset());
            StartCoroutine(PositionSlide(initial_position, target_position));

        }
    }

    public void ProgressDefault() {
        if (transform.localPosition == initial_position) return;

        target_position = transform.localPosition;

        if (process) {
            StartCoroutine(RotationReset());
            StartCoroutine(PositionSlide(target_position, initial_position));
        }
    }



    public void ProgressDragOn() {
        gameObject.GetComponent<MeshCollider>().enabled=true;
    }

    public void ProgressDragOff()
    {
        gameObject.GetComponent<MeshCollider>().enabled = false;
    }

    public IEnumerator PositionSlide(Vector3 start, Vector3 end) {
        process = false;
        // 중복 진입 방지

        float entry_time = Time.time;
        // 루프 시작 전에 최초 진입 시간 저장
        while (Time.time - entry_time < duration) {

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

    private void OnDrawGizmos() {
        if (Application.isPlaying == false) {
            Vector3 start = transform.position;
            Vector3 end = start + (transform.rotation * endPos);

            Gizmos.DrawLine(start, end);
            Gizmos.DrawWireSphere(end, 0.1f);
        }
    }

    private IEnumerator RotationReset()
    {
        process = false;
        Quaternion start = transform.rotation;

        float entry_time = Time.time;
        while (Time.time - entry_time < duration)
        {

            float t = (Time.time - entry_time) / duration;

            t = curve.Evaluate(t);

            transform.rotation = Quaternion.Slerp(start, Quaternion.identity, t);

            yield return null;
        }

        transform.rotation = Quaternion.identity;
        process = true;
    }



}
