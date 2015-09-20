module TrainingTracker {

    export class RequestBase {
        constructor(public baseUrl: string) {
        }
    }

    export enum EventType {
        USER_CREATED
    }

    export function eventTypeName(type: EventType) {
        return EventType[type];
    }

    export class Trigger {
        static customEvent(name: string, detail: {}) {
            var event = new StandardEvent(name, detail);
            document.dispatchEvent(event);
        }
    }
}