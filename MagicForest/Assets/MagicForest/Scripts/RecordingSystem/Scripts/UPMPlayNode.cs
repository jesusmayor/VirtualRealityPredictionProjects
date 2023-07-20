using System.Collections.Generic;
using UnityEngine;

public class UPMPlayNode : MonoBehaviour
{
    protected struct TransformTimeTruple
    {
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 scale;
        public float inTime;
        public TransformTimeTruple(Vector3 position, Quaternion rotation, Vector3 scale, float inTime)
        {
            this.pos = position;
            this.rot = rotation;
            this.scale = scale;
            this.inTime = inTime;
        }
    }

    protected Queue<TransformTimeTruple> queuedTransforms = new Queue<TransformTimeTruple>();
    protected TransformTimeTruple currentTransform;
    protected TransformTimeTruple lastTransform;

    protected bool bMove = false;
    protected float timeCounter = 0f;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (bMove)
        {
            timeCounter += Time.deltaTime;

            if (timeCounter > currentTransform.inTime)
            {
                timeCounter -= currentTransform.inTime;
                if (queuedTransforms.Count > 0)
                {
                    transform.position = currentTransform.pos;
                    transform.rotation = currentTransform.rot;
                    transform.localScale = currentTransform.scale;

                    lastTransform = currentTransform;

                    currentTransform = queuedTransforms.Dequeue();
                }
                else
                    bMove = false;

                return;
            }

            transform.position = CalculateNextFrame(currentTransform.pos, transform.position, timeCounter, currentTransform.inTime);
            transform.rotation = CalculateNextFrame(currentTransform.rot, transform.rotation, timeCounter, currentTransform.inTime); 
            transform.localScale = CalculateNextFrame(currentTransform.scale, transform.localScale, timeCounter, currentTransform.inTime);
        }
    }


    public virtual void PlayNextPosition(Vector3 nextPosition, Quaternion nextRotation, Vector3 nextScale, float nextPosInTime)
    {
        bMove = true;
        queuedTransforms.Enqueue(new TransformTimeTruple(nextPosition, nextRotation, nextScale, nextPosInTime));
    }

    public virtual void InitializePosition(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
        currentTransform = new TransformTimeTruple(position, rotation, scale, 0f);
        lastTransform = new TransformTimeTruple(position, rotation, scale, 0f);
    }

    protected Vector3 CalculateNextFrame(Vector3 objPosition, Vector3 lastPosition, float elapsedTime, float maxTimeToReach)
    {
        if (elapsedTime <= 0f)
            elapsedTime = float.Epsilon;

        return Vector3.Lerp(lastPosition, objPosition, elapsedTime / maxTimeToReach);
    }

    protected Quaternion CalculateNextFrame(Quaternion objRotation, Quaternion lastRotation, float elapsedTime, float maxTimeToReach)
    {
        if (elapsedTime <= 0f)
            elapsedTime = float.Epsilon;

        return Quaternion.Lerp(lastRotation, objRotation, elapsedTime / maxTimeToReach);
    }

}
