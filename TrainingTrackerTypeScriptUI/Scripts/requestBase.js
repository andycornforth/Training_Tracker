var TrainingTracker;
(function (TrainingTracker) {
    var RequestBase = (function () {
        function RequestBase(baseUrl) {
            this.baseUrl = baseUrl;
        }
        return RequestBase;
    })();
    TrainingTracker.RequestBase = RequestBase;

    (function (EventType) {
        EventType[EventType["USER_CREATED"] = 0] = "USER_CREATED";
    })(TrainingTracker.EventType || (TrainingTracker.EventType = {}));
    var EventType = TrainingTracker.EventType;

    function eventTypeName(type) {
        return EventType[type];
    }
    TrainingTracker.eventTypeName = eventTypeName;

    var Trigger = (function () {
        function Trigger() {
        }
        Trigger.customEvent = function (name, detail) {
            var event = new StandardEvent(name, detail);
            document.dispatchEvent(event);
        };
        return Trigger;
    })();
    TrainingTracker.Trigger = Trigger;
})(TrainingTracker || (TrainingTracker = {}));
//# sourceMappingURL=requestBase.js.map
