// Polyfill for CustomEvent: // https://developer.mozilla.org/en/docs/Web/API/CustomEvent
(function () {
    function CustomEvent(event, params) {
        params = params || { bubbles: false, cancelable: false, detail: undefined };
        var evt = document.createEvent('CustomEvent');
        evt.initCustomEvent(event, params.bubbles, params.cancelable, params.detail);
        return evt;
    }
    ;
    CustomEvent.prototype = window.Event.prototype;
    window.CustomEvent = CustomEvent;
})();

var StandardEvent = CustomEvent;

var TrainingTracker;
(function (TrainingTracker) {
    TrainingTracker.addEvent = (function () {
        if (document.addEventListener) {
            return function (elem, eventName, callback) {
                if (elem && elem.addEventListener) {
                    // Handles a single element
                    elem.addEventListener(eventName, callback, false);
                } else if (elem && elem.length) {
                    for (var i = 0; i < elem.length; i++) {
                        TrainingTracker.addEvent(elem[i], eventName, callback);
                    }
                }
            };
        } else {
            return function (elem, eventName, callback) {
                if (elem && elem.attachEvent) {
                    // Handles a single element
                    elem.attachEvent('on' + eventName, function () {
                        return callback.call(elem, window.event);
                    });
                } else if (elem && elem.length) {
                    for (var i = 0; i < elem.length; i++) {
                        TrainingTracker.addEvent(elem[i], eventName, callback);
                    }
                }
            };
        }
    })();
})(TrainingTracker || (TrainingTracker = {}));
//# sourceMappingURL=customEvents.js.map
