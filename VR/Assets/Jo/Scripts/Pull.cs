using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pull : XRBaseInteractable
{
    public static event Action<float> PullActionReleased;

    public Transform start, end;
    public GameObject notch;

    public float pullAmount { get; private set; } = 0.0f;

    private LineRenderer _lineRenderer;
    private IXRSelectInteractor pullingInteractor = null;

    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        pullingInteractor = args.interactorObject;
    }

    public void Release()
    {
        PullActionReleased?.Invoke(pullAmount);
        pullingInteractor = null;
        pullAmount = 0f;
        notch.transform.localPosition =
            new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, 0f);
        UpdateString();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPosition = pullingInteractor.transform.position;
                pullAmount = CalculatePull(pullPosition);

                UpdateString();
            }
        }
    }

    private float CalculatePull(Vector3 pullPosistion)
    {
        Vector3 pullDirection = pullPosistion - start.position;
        Vector3 targetDirecton = end.position - start.position;
        float maxLength = targetDirecton.magnitude;

        targetDirecton.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirecton) / maxLength;
        return Math.Clamp(pullValue, 0, 1);

    }

    private void UpdateString()
    {
        Vector3 linePosition = Vector3.forward *
                               Mathf.Lerp(start.transform.localPosition.z, end.transform.localPosition.z, pullAmount);
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y,
            linePosition.z + 0.2f);
        _lineRenderer.SetPosition(1,linePosition);
    }


}
