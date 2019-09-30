using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MirrorTest : MonoBehaviour
{
	public float breakDuration, shakeDuration = 0.7f;
	public Transform firstCam, mirrorParent;

	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < mirrorParent.childCount; i++)
		{
			mirrorParent.GetChild(i).DOLocalRotate(new Vector3(Random.Range(0, 30), 0, Random.Range(0, 25)), breakDuration);
			mirrorParent.GetChild(i).DOScale(mirrorParent.GetChild(i).localScale / 1.1f, breakDuration);
		}

		firstCam.DOShakePosition(shakeDuration, 0.5f, 20, 90, false, true);

		Invoke("BackToNormal", 5.0f);
	}

	void BackToNormal()
	{
		//yield return new WaitForSeconds(5.0f);

		for (int i = 0; i < mirrorParent.childCount; i++)
		{
			mirrorParent.GetChild(i).DOLocalRotate(new Vector3(0, 0, 0), breakDuration);
			mirrorParent.GetChild(i).DOScale(mirrorParent.GetChild(i).localScale * 1.1f, breakDuration);
		}
	}
}
