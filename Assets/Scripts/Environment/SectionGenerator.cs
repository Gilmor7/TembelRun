using System.Collections;
using UnityEngine;

namespace Environment
{
    public class SectionGenerator : MonoBehaviour
    {
        private const int SectionZPosOffset = 50;
        private bool _isCreateSection = false;

        [SerializeField] private int _sectionNumber;
        [SerializeField] private GameObject[] _sections;
        [SerializeField] private int _zPos = SectionZPosOffset;
    
        void Update()
        {
            if (!_isCreateSection) 
            {
                _isCreateSection = true;
                StartCoroutine(GenerateSection());
            }
        }

        private IEnumerator GenerateSection() {
            _sectionNumber = Random.Range(0, 3);
            Instantiate(_sections[_sectionNumber], new Vector3(0f, 0f, _zPos), Quaternion.identity);
            _zPos += SectionZPosOffset;
            yield return new WaitForSeconds(2);
            _isCreateSection = false;
        }
    }
}
