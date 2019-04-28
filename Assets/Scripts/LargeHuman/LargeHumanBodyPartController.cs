using System.Linq;
using UnityEngine;

public class LargeHumanBodyPartController : MonoBehaviour {
    public void Awake() {
        var allBodyParts = this.GetComponentsInChildren<BodyPart>();

        foreach (var bodyPart in allBodyParts) {
            bodyPart.partName = bodyPart.gameObject.name;

            var state = GlobalGameController
                .globalInstance
                .currentPlayerBodyState
                .FirstOrDefault(s => s.bodyPartName == bodyPart.partName);

            if (state.bodyPartName == default(BodyPartGlobalState).bodyPartName) {
                Debug.Log("Adding default body part global state for " + bodyPart.partName);
                state = new BodyPartGlobalState() {
                    bodyPartName = bodyPart.partName,
                    currentType = BodyPartType.Human
                };

                GlobalGameController
                    .globalInstance
                    .currentPlayerBodyState
                    .Add(state);
            }

            bodyPart.currentType = state.currentType;
            bodyPart.InitializeBasedOnCurrentType();
        }
    }
}
