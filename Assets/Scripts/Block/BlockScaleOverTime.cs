using UnityEngine;

public class BlockScaleOverTime : MonoBehaviour
{
    [SerializeField] private bool verticalScaling = false;
    [SerializeField] private bool horizontalScaling = false;
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float maxScale = 1f;
    [SerializeField] private float scaleSpeed = 1f;
    private float time;

    void Update()
    {
        ChangeScale();
    }

    private void ChangeScale()
    {
        time += Time.deltaTime * scaleSpeed;
        float scaleTime = Mathf.PingPong(time, 1f);
        float currentScale = Mathf.Lerp(minScale, maxScale, scaleTime);
        Vector3 currentScaleVector = transform.localScale;
        if (verticalScaling)
        {
            currentScaleVector.y = currentScale;
        }
        if (horizontalScaling)
        {
            currentScaleVector.x = currentScale;
        }
        transform.localScale = currentScaleVector;
    }
}
