using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericNS { 
    public class ScaleUpSelect : MonoBehaviour {

        public float notSelectedSize = 0.5f; //starts always at this size
        public float selectedSize = 1.0f;
        public float scaleUpSpeed = 2f;
        public float scaleDownSpeed = 2f;

        float _currentScale = 1f;
        bool _isSelected = false;
        Vector3 _currentScaleV3 = Vector3.zero;

        private void Awake()
        {
            _currentScale = notSelectedSize;
        }

        public void SelectElement()
        {
            _isSelected = true;
        }


        public void DeselectElement()
        {
            _isSelected = false;
        }


        private void Update()
        {
            if (_isSelected)
            {
                _currentScale = Mathf.Lerp(_currentScale, selectedSize, Time.deltaTime * scaleUpSpeed);
            }
            else
            {
                _currentScale = Mathf.Lerp(_currentScale, notSelectedSize, Time.deltaTime * scaleDownSpeed);
            }
            //Debug.Log(_currentScale * ((RectTransform)this.transform).sizeDelta.x);

            _currentScale = Mathf.Clamp(_currentScale, notSelectedSize, selectedSize);
            _currentScaleV3.x = _currentScaleV3.y = _currentScaleV3.z = _currentScale;
            this.transform.localScale = _currentScaleV3;
        }

    }
}
