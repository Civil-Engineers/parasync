using UnityEngine;
using UnityEngine.UI;

namespace Parasync.Runtime.UI
{
    public class ActionSlot : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        public void OnTick(float progressFilled)
        {
            progressBar.fillAmount = progressFilled;
        }

        public void ResetProgress()
        {
            progressBar.fillAmount = 0;
        }
    }
}
