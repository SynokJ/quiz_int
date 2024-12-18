using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const float _MAX_RAY_DISTANCE = 20.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 originPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(originPos, Vector2.zero);

            if (hit.collider != null)
            {
                IClickable clickable = hit.collider.gameObject.GetComponent<IClickable>();
                clickable.OnClicked();
            }
        }
    }
}
