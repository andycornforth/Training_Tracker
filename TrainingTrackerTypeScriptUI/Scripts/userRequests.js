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

            Ajax.httpPost(url, user, wrappedCallback, errorCallback, this.authorizationHeader);
        };
        return User;
    })(TrainingTracker.RequestBase);
    TrainingTracker.User = User;
})(TrainingTracker || (TrainingTracker = {}));
//# sourceMappingURL=userRequests.js.map
