import { Behaviour, PointerEventData, serializable, PlayableDirector, IPointerClickHandler } from "@needle-tools/engine";
export class TimelineTester extends Behaviour implements IPointerClickHandler {


    @serializable(PlayableDirector)
    timeline: PlayableDirector = new PlayableDirector;

    start() {
        console.log("Test");
    }

    trigger: "tap" | "start" = "tap";
    onPointerClick(args: PointerEventData) {
        args.use();

        console.log("Test");
        // TODO this is currently quite annoying to use,
        // as for the web we use the Animator component and its states directly,
        // while in QuickLook we use explicit animations / states.
        if (this.timeline.isPlaying)
            this.timeline.stop();
        this.timeline.play();
    }

}
