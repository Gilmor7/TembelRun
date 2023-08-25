using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionGenerator : MonoBehaviour
{
    private const int SECTION_Z_POS_OFFSET = 50;
    private bool isCreateSection = false;

    [SerializeField] private int sectionNumber;
    [SerializeField] private GameObject[] sections;
    [SerializeField] private int zPos = SECTION_Z_POS_OFFSET;
    
    void Update()
    {
        if (!isCreateSection) {
            isCreateSection = true;
            StartCoroutine(generateSection());
        }
    }

    private IEnumerator generateSection() {
        sectionNumber = Random.Range(0, 3);
        Instantiate(sections[sectionNumber], new Vector3(0f, 0f, zPos), Quaternion.identity);
        zPos += SECTION_Z_POS_OFFSET;
        yield return new WaitForSeconds(2);
        isCreateSection = false;
    }
}
