module TrainingTracker {

    export class CreateUserForm {

        display(): HTMLDivElement {

            var createUserFormTemplate = Handlebars.templates.CreateUserForm;

            //var createUserForm = this.stringToElement(createUserFormTemplate({}));

            var createUserForm = document.createElement('div');
            createUserForm.innerHTML = "Hello";

            return createUserForm;
        }

        stringToElement(html: string) {
            var temporaryContainer = document.createElement('div');
            temporaryContainer.innerHTML = html;
            return temporaryContainer.getElementsByTagName('div')[0];
        }
    }
}