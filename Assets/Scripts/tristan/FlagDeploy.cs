using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDeploy : MonoBehaviour
{
    private void FinishDeployFlag() {
        gameObject.GetComponent<Animator>().SetTrigger("finishDeployFlag");
    }
}
