using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MirrorZoom : MonoBehaviour
{
    [Header("References")]
    public Transform mirrorParent;
    public Transform cam;
    [Header("Multi-References")]
    // As peças que quero mexer
    public Transform[] selectedPieces; 
    // Peças separadas, sao ref para as selectedPieces
    public Transform[] desiredPositions;

    public float charPieceScale = 1.3f;

    void Start()
    {
        // Criou a sequencia das animações, para dar join(ao mesmo tempo) ou append(no final)
        Sequence mirrorZoom = DOTween.Sequence();
        mirrorZoom.Append(cam.transform.DOPunchPosition(Vector3.up / 2, 0.2f, 20, 1, false)); // 1ª sequencia, punch pq lembra um soco essa sequencia

        //for (int i = 0; i < selectedPieces.Length; i++)
        //{
        //    selectedPieces[i].parent = null; // Para o parent nao interferir nas animações, ignorar o Parent
        //}

        for (int i = 0; i < selectedPieces.Length; i++)
        {
            Transform charPiece = selectedPieces[i]; // charPiece, as peçasque vao ter as artes dos personagens
            //charPiece.GetChild(0).gameObject.SetActive(true); // O sprite Mask que vai ser ativado para as artes dos personagens

            mirrorZoom.Join(charPiece.DOScale(charPiece.localScale * charPieceScale, .15f));
            mirrorZoom.Join(charPiece.DOMove(desiredPositions[i].position, .15f));
            mirrorZoom.Join(charPiece.DORotate(desiredPositions[i].eulerAngles, .15f));
            mirrorZoom.Join(charPiece.DOBlendableLocalRotateBy(new Vector3(Random.Range(0, 10), 0, 0), 2));

            //Fazer as peças padrão girar mais
            for (int j = 0; j < mirrorParent.childCount; j++)
            {
                // O parent procurou os childs para mexer tudo junto, menos as peças que ele Separou
                Transform mirrorPiece = mirrorParent.GetChild(j); // Todas do mirror parent, como no MirrorBreak

                mirrorZoom.Join(mirrorPiece.DOLocalRotate(new Vector3(Random.Range(0, 15), Random.Range(0, 20), Random.Range(0, 30)), 1.4f));
                mirrorZoom.Join(mirrorPiece.DOScale(mirrorPiece.localScale / 1.1f, .2f));
            }

        }

        #region MovePiecesToCam
        // mover todas as peças padrão para frente
        mirrorZoom.Append(mirrorParent.DOMoveZ(-50, .3f));
        mirrorZoom.Join(mirrorParent.DOBlendableRotateBy(Vector3.right * 30, .3f));

        // mover todas as partes selecionaveis para frente tambem, ja que estão separadas
        for (int i = 0; i < selectedPieces.Length; i++)
        {
            mirrorZoom.Join(selectedPieces[i].DOMoveZ(-50, .8f));
            mirrorZoom.Join(selectedPieces[i].DOBlendableLocalRotateBy(Vector3.right * 90, .3f));
        }
        #endregion
    }
}
