/// <reference path="handlebars.d.ts" />
/// <reference path="handlebarsTemplateInterface.ts" />
/// <reference path="ajax.ts" />
/// <reference path="customEvents.ts" />
/// <reference path="requestBase.ts" />
/// <reference path="userRequests.ts" />

/// <reference path="createUserForm.ts" />

module TrainingTracker {

    // Master display function
    var display = function (elem: HTMLDivElement) {
        var mainElement = document.getElementById('content');
        mainElement.innerHTML = '';
        mainElement.appendChild(elem);
    };

    // Generic error handler
    var errorHandler = (response: XMLHttpRequest) => {
        console.log(response.statusText);
        var message = document.createElement('div');
        message.id = 'dms-error';
        message.className = response.statusText;
        message.innerHTML = 'An error has ocurred.';
        display(message);
    };

    export class Runner {

        private userService = new User("UrlStr");

        constructor() {
            //  this.registerListeners(defaultProjectId, defaultSearchTerm);
        }

        displayCreateUserScreen() {
            var createUserForm = new CreateUserForm();
            var output = createUserForm.display();
            display(output);
        }

        addUser(username: string, password: string, firstName: string, lastName: string, email: string, dob: Date, genderId: number) {
            var person: Person = {
                Username: username,
                Password: password,
                FirstName: firstName,
                LastName: lastName,
                Email: email,
                DOB: dob,
                GenderId: genderId
            };
            this.userService.postNewUser(person, (response) => {
                Trigger.customEvent(eventTypeName(EventType.USER_CREATED), { detail: { userCreated: response } });
            }, errorHandler);
        }

    }

    var runner = new Runner();
    runner.displayCreateUserScreen();

}