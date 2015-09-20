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
//# sourceMappingURL=ajax.js.map
