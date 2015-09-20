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
        }
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
})(TrainingTracker || (TrainingTracker = {}));
//# sourceMappingURL=app.js.map
