// Polyfill for CustomEvent: // https://developer.mozilla.org/en/docs/Web/API/CustomEvent
(function () {
    function CustomEvent(event, params) {
        params = params || { bubbles: false, cancelable: false, detail: undefined };
        var evt = <any>document.createEvent('CustomEvent');
        evt.initCustomEvent(event, params.bubbles, params.cancelable, params.detail);
        return evt;
    };
    CustomEvent.prototype = (<any>window).Event.prototype;
    (<any>window).CustomEvent = CustomEvent;
})();

// Fix for lib.d.ts
interface StandardEvent {
    new (name: string, obj: {}): CustomEvent;
}
var StandardEvent = <StandardEvent><any> CustomEvent;

module TrainingTracker {
    export var addEvent: (elem: any, eventName: string, callback: Function) => void = (function () {
        if (document.addEventListener) {
            return function (elem, eventName, callback) {
                if (elem && elem.addEventListener) {
                    // Handles a single element
                    elem.addEventListener(eventName, callback, false);
                } else if (elem && elem.length) {
                    // Handles a collection of elements (recursively)
                    for (var i = 0; i < elem.length; i++) {
                        addEvent(elem[i], eventName, callback);
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
                    // Handles a collection of elements (recursively)
                    for (var i = 0; i < elem.length; i++) {
                        addEvent(elem[i], eventName, callback);
                    }
                }
            };
        }
    })();
} 