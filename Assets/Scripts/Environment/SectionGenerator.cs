using System.Collections;
using Managers;
using UnityEngine;

namespace Environment
{
    public class SectionGenerator : MonoBehaviour
    {
        private const int SectionZPosOffset = 50;
        private bool _shouldCreateSection = false;

        [SerializeField] private int _sectionNumber;
        [SerializeField] private GameObject[] _sections;
        [SerializeField] private int _zPos = SectionZPosOffset;
    
        void Update()
        {
            if (_shouldCreateSection == false && GameManager.Instance.IsPlaying) 
            {
                _shouldCreateSection = true;
                StartCoroutine(GenerateSectionRoutine());
            }
        }

        private IEnumerator GenerateSectionRoutine() {
            _sectionNumber = Random.Range(0, 6);
            GameObject generatedSection = Instantiate(_sections[_sectionNumber], new Vector3(0f, 0f, _zPos), Quaternion.identity);
            generatedSection.name = "GeneratedSection";
            _zPos += SectionZPosOffset;
            yield return new WaitForSeconds(2);
            _shouldCreateSection = false;
        }
    }
}
