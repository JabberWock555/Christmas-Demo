import { Behaviour, PointerEventData, serializable, PlayableDirector, IPointerClickHandler } from "@needle-tools/engine";
export class Test extends Behaviour implements IPointerClickHandler {


    @serializable(PlayableDirector) public timeline;

    start() {
        console.log("Test");
    }

    trigger: "tap" | "start" = "tap";
    onPointerClick(args: PointerEventData) {
        args.use();
        // TODO this is currently quite annoying to use,
        // as for the web we use the Animator component and its states directly,
        // while in QuickLook we use explicit animations / states.
        this.timeline?.Play();
    }


}
