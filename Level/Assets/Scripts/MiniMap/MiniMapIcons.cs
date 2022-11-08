using UnityEngine;

public class MiniMapIcons : MonoBehaviour
{
    [SerializeField] Transform character;

    private void LateUpdate()
    {
        if (character != null)
        {
            Vector3 newPos = character.position;
            newPos.y = transform.position.y;
            transform.position = newPos;

            transform.rotation = Quaternion.Euler(90, character.eulerAngles.y + 270, 0);

        }
    }
}
