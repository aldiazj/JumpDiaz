using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LimitsManager
{
    public static bool HoleInsideLine(Camera camera, Vector3 pos)
    {
        Vector3 insideCamCoordinates = camera.WorldToViewportPoint(pos);
        if (insideCamCoordinates.x >= 0 && insideCamCoordinates.x <= 1 && insideCamCoordinates.y >= 0 && insideCamCoordinates.y <= 1)
            return true;
        return false;
    }

    public static bool PlayerInsideLine(Vector3 pos)
    {
        if (pos.x >= -5.2f && pos.x <= 5.2f)
            return true;
        return false;
    }
}
