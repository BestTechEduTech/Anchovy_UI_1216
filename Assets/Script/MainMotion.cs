using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMotion : MonoBehaviour
{
    public Vector3 endPos;


    private Vector3 initial_position;
    private Vector3 target_position;


    public float duration = 3f;
    public AnimationCurve curve;

    private float position_t = 0f;

    private bool process = true;

    private void Start() {
        gameObject.transform.localPosition = initial_position;
    }

    public void ResetPosition() {
        if (transform.localPosition == initial_position) return;

        target_position = transform.localPosition;

        if (process) {            
            StartCoroutine(PositionReset(target_position, initial_position));
        }
    }

    public IEnumerator PositionReset(Vector3 start, Vector3 end) {
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
   
}
