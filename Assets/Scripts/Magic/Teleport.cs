using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destinationGate; // Cổng đích (cổng mục tiêu)
    public LayerMask allowedLayers; // Layer mask cho các layer được phép dịch chuyển

    private bool isTeleporting = false; // Biến kiểm tra xem đang trong quá trình dịch chuyển không

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsLayerAllowed(other.gameObject.layer) && !isTeleporting) // Kiểm tra va chạm là đối tượng có layer được phép và không trong quá trình dịch chuyển
        {
            isTeleporting = true; // Đánh dấu đang trong quá trình dịch chuyển

            // Di chuyển đối tượng va chạm đến cổng đích nếu cổng đích được xác định
            if (destinationGate != null)
            {
                other.transform.position = destinationGate.transform.position;
                Teleport destinationGateScript = destinationGate.GetComponent<Teleport>();
                if (destinationGateScript != null)
                {
                    destinationGateScript.isTeleporting = true; // Đánh dấu đang trong quá trình dịch chuyển ở cổng đích
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsLayerAllowed(other.gameObject.layer)) // Kiểm tra nếu người chơi rời khỏi cổng
        {
            isTeleporting = false; // Đánh dấu không trong quá trình dịch chuyển
        }
    }
    private bool IsLayerAllowed(int layer)
    {
        return ((allowedLayers.value & (1 << layer)) > 0); // Kiểm tra xem layer có trong Layer mask được phép không
    }
}