using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ProductPoint : MonoBehaviour
    {
        private Product _product;
        private bool _productReady;

        public Product Product => _product;
        public bool HasProduct => _product != null;
        public bool ProductReady => _productReady && HasProduct;

        #region PublicMethods
        public void AddProduct(Product product, float animationTime = 0)
        {
            product.transform.parent = transform;
            product.transform.localPosition = Vector3.zero;
            product.transform.localRotation = Quaternion.identity;
            product.gameObject.SetActive(true);

            _product = product;

            product.transform.DOScale(Vector3.one, animationTime).OnComplete(() => _productReady = true);
        }

        public void RemoveProduct()
        {
            _product = null;
        }
        #endregion
    }
}
