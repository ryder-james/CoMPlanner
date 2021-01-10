using CasePlanner.Data.Notes;
using UnityEngine;

namespace CasePlanner.UI {
    public class StringCutter : MonoBehaviour {
        [SerializeField]
        private PinConnector connector = null;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (connector.A == null && collision.CompareTag("Yarn")) {
                Yarn yarn = collision.GetComponentInParent<Yarn>();
                yarn.A.Remove(yarn);
                yarn.B.Remove(yarn);
                Destroy(yarn.gameObject);
            }
        }
    }
}