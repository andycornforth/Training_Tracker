/*
This file has no dependencies
AJAX Requests
Project: https://github.com/Steve-Fenton/TypeScriptUtilities
Author: Steve Fenton
Example usage:
import Ajax = require('Ajax');
function ajaxLogger(response) {
alert(response.status + ' ' + response.responseText);
}
Ajax.httpGet('/test.txt', ajaxLogger, ajaxLogger);
// Add headers (you can supply any number of additional headers)
Ajax.httpGet('/test.txt', ajaxLogger, ajaxLogger, { name: 'Authorization', value: 'MYTOKEN' });
// Post data
Ajax.httpPost('/test.txt', { key: 'H12', type: 'some type' }, ajaxLogger, ajaxLogger);
*/
var Ajax;
(function (_Ajax) {
    function httpGet(url, successCallback, failureCallback) {
        var headers = [];
        for (var _i = 0; _i < (arguments.length - 3); _i++) {
            headers[_i] = arguments[_i + 3];
        }
        var ajax = new Ajax();
        ajax.send(url, HttpVerb.GET, null, successCallback, failureCallback, headers);
    }
    _Ajax.httpGet = httpGet;

    function httpPost(url, data, successCallback, failureCallback) {
        var headers = [];
        for (var _i = 0; _i < (arguments.length - 4); _i++) {
            headers[_i] = arguments[_i + 4];
        }
        var ajax = new Ajax();
        ajax.send(url, HttpVerb.POST, data, successCallback, failureCallback, headers);
    }
    _Ajax.httpPost = httpPost;

    var HttpVerb = (function () {
        function HttpVerb() {
        }
        HttpVerb.CONNECT = 'CONNECT';
        HttpVerb.DELETE = 'DELETE';
        HttpVerb.GET = 'GET';
        HttpVerb.HEAD = 'HEAD';
        HttpVerb.OPTIONS = 'OPTIONS';
        HttpVerb.POST = 'POST';
        HttpVerb.PUT = 'PUT';
        HttpVerb.TRACE = 'TRACE';
        return HttpVerb;
    })();

    var Ajax = (function () {
        function Ajax() {
        }
        Ajax.prototype.send = function (url, method, data, successCallback, failureCallback, headers) {
            var _this = this;
            var isComplete = false;
            var request = this.getRequestObject();
            var uniqueUrl = this.getCacheBusterUrl(url);

            request.open(method, url, true);
            request.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
            request.setRequestHeader('Accept', 'application/json');

            // Add headers
            if (data !== null) {
                request.setRequestHeader('Content-type', 'application/json');
            }
            for (var i = 0; i < headers.length; ++i) {
                request.setRequestHeader(headers[i].name, headers[i].value);
            }

            request.onreadystatechange = function () {
                if (request.readyState == 4 && !isComplete) {
                    isComplete = true;
                    if (_this.isResponseSuccess(request.status)) {
                        successCallback.call(request, request);
                    } else {
                        failureCallback.call(request, request);
                    }
                }
            };

            if (data !== null) {
                request.send(JSON.stringify(data));
            } else {
                request.send();
            }
        };

        Ajax.prototype.getRequestObject = function () {
            var requestObject;
            if (XMLHttpRequest) {
                requestObject = new XMLHttpRequest();
            } else {
                try  {
                    requestObject = new ActiveXObject('Msxml2.XMLHTTP');
                } catch (e) {
                    try  {
                        requestObject = new ActiveXObject('Microsoft.XMLHTTP');
                    } catch (e) {
                    }
                }
            }

            return requestObject;
        };

        Ajax.prototype.getCacheBusterUrl = function (url) {
            if (url.indexOf('?') > -1) {
                url += '&' + new Date().getTime();
            } else {
                url += '?' + new Date().getTime();
            }
            return url;
        };

        Ajax.prototype.isResponseSuccess = function (responseCode) {
            var firstDigit = responseCode.toString().substring(0, 1);
            switch (firstDigit) {
                case '1':
                case '2':
                case '3':
                    // Response code is in 100, 200 or 300 range :)
                    return true;
                default:
                    // Response code is is 400 or 500 range :(
                    return false;
            }
        };
        return Ajax;
    })();
})(Ajax || (Ajax = {}));
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
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var TrainingTracker;
(function (TrainingTracker) {
    var User = (function (_super) {
        __extends(User, _super);
        function User(baseUrl) {
            _super.call(this, baseUrl);
        }
        User.prototype.postNewUser = function (user, successCallback, errorCallback) {
            var url = this.baseUrl + '/AddPerson/';
            var wrappedCallback = function (response) {
                var typedResponse = JSON.parse(response.responseText);
                successCallback(typedResponse);
            };

            Ajax.httpPost(url, user, wrappedCallback, errorCallback, null);
        };
        return User;
    })(TrainingTracker.RequestBase);
    TrainingTracker.User = User;
})(TrainingTracker || (TrainingTracker = {}));
var TrainingTracker;
(function (TrainingTracker) {
    var CreateUserForm = (function () {
        function CreateUserForm() {
        }
        CreateUserForm.prototype.display = function () {
            var createUserFormTemplate = Handlebars.templates.CreateUserForm;

            //var createUserForm = this.stringToElement(createUserFormTemplate({}));
            var createUserForm = document.createElement('div');
            createUserForm.innerHTML = "Hello";

            return createUserForm;
        };

        CreateUserForm.prototype.stringToElement = function (html) {
            var temporaryContainer = document.createElement('div');
            temporaryContainer.innerHTML = html;
            return temporaryContainer.getElementsByTagName('div')[0];
        };
        return CreateUserForm;
    })();
    TrainingTracker.CreateUserForm = CreateUserForm;
})(TrainingTracker || (TrainingTracker = {}));
/// <reference path="handlebars.d.ts" />
/// <reference path="handlebarsTemplateInterface.ts" />
/// <reference path="ajax.ts" />
/// <reference path="customEvents.ts" />
/// <reference path="requestBase.ts" />
/// <reference path="userRequests.ts" />
/// <reference path="createUserForm.ts" />
var TrainingTracker;
(function (TrainingTracker) {
    // Master display function
    var display = function (elem) {
        var mainElement = document.getElementById('content');
        mainElement.innerHTML = '';
        mainElement.appendChild(elem);
    };

    // Generic error handler
    var errorHandler = function (response) {
        console.log(response.statusText);
        var message = document.createElement('div');
        message.id = 'dms-error';
        message.className = response.statusText;
        message.innerHTML = 'An error has ocurred.';
        display(message);
    };

    var Runner = (function () {
        function Runner() {
            this.userService = new TrainingTracker.User("UrlStr");
            //  this.registerListeners(defaultProjectId, defaultSearchTerm);
        }
        Runner.prototype.displayCreateUserScreen = function () {
            var createUserForm = new TrainingTracker.CreateUserForm();
            var output = createUserForm.display();
            display(output);
        };

        Runner.prototype.addUser = function (username, password, firstName, lastName, email, dob, genderId) {
            var person = {
                Username: username,
                Password: password,
                FirstName: firstName,
                LastName: lastName,
                Email: email,
                DOB: dob,
                GenderId: genderId
            };
            this.userService.postNewUser(person, function (response) {
                TrainingTracker.Trigger.customEvent(TrainingTracker.eventTypeName(0 /* USER_CREATED */), { detail: { userCreated: response } });
            }, errorHandler);
        };
        return Runner;
    })();
    TrainingTracker.Runner = Runner;

    var runner = new Runner();
    runner.displayCreateUserScreen();
})(TrainingTracker || (TrainingTracker = {}));
//# sourceMappingURL=trainingTracker.js.map
