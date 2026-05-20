using UnityEngine;

public class RoomTransitionControllerTest : MonoBehaviour
{
    public RoomTransitionController controller;

    [SerializeField, Range(0, 2)] private int test;
    [SerializeField, Range(0, 1)] private float testValue;
    [SerializeField] private float testDistance = 10;
    [SerializeField] private Transform origin;
    [SerializeField] private TilemapRoom testFrom;
    [SerializeField] private TilemapRoom testTo;

    private void OnValidate()
    {
        if (test == 1) controller.SetDissolve(testValue);
        if (test == 2) controller.SetRadial(testValue, testDistance);
        controller.SetOrigin(origin.position);
    }

    [ContextMenu("TestTransition")]
    public void TestTransitionAB() => controller.PlayTransition(testFrom, testTo, origin.position);

    [ContextMenu("TestTransition")]
    public void TestTransitionBA() => controller.PlayTransition(testTo, testFrom, origin.position);
}