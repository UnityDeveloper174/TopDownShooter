using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public bool enable = false;
    
    public void AutoAimChangeState()
    {
        enable = !enable;
    }


}
