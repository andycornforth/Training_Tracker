module TrainingTracker {

    export interface Person {
        Username: string;
        Password: string;
        FirstName: string;
        LastName: string;
        Email: string;
        DOB: Date;
        GenderId: number;
    }

    export class User extends RequestBase {

        constructor(baseUrl: string) {
            super(baseUrl);
        }

        postNewUser(user: Person, successCallback: (response: boolean) => any, errorCallback: (response: XMLHttpRequest) => any): void {
            var url = this.baseUrl + '/AddPerson/';
            var wrappedCallback = (response: XMLHttpRequest) => {
                var typedResponse = <boolean> JSON.parse(response.responseText);
                successCallback(typedResponse);
            };

            Ajax.httpPost(url, user, wrappedCallback, errorCallback, null);
        }
    }
}