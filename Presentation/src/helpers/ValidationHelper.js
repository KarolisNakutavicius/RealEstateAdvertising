import { isEmail } from "validator";

export default class ValidationHelper {
    static required = value => {
        if (!value) {
            return (
                <div className="alert alert-danger" role="alert">
                    This field is required!
                </div>
            );
        }
    };
    static correctEmail = value => {
        if (!isEmail(value)) {
            return (
                <div className="alert alert-danger" role="alert">
                    This is not a valid email.
                </div>
            );
        }
    };
    static correctLengthUserName = value => {
        if (value.length < 3 || value.length > 20) {
            return (
                <div className="alert alert-danger" role="alert">
                    The username must be between 3 and 20 characters.
                </div>
            );
        }
    };
    static correctLengthPassword = value => {
        if (value.length < 6 || value.length > 40) {
            return (
                <div className="alert alert-danger" role="alert">
                    The password must be between 6 and 40 characters.
                </div>
            );
        }
    };

    static passwordMustMatch = (value, props, components) => {
        // NOTE: Tricky place. The 'value' argument is always current component's value.
        // So in case we're 'changing' let's say 'password' component - we'll compare it's value with 'confirm' value.
        // But if we're changing 'confirm' component - the condition will always be true
        // If we need to always compare own values - replace 'value' with components.password[0].value and make some magic with error rendering.

        var x = components['password'][0].value;

        if (value !== components['password'][0].value) { // components['password'][0].value !== components['confirm'][0].value
          // 'confirm' - name of input
          // components['confirm'] - array of same-name components because of checkboxes and radios
          return (
            <div className="alert alert-danger" role="alert">
                Passwords are not equal.
            </div>
        );
        }
      };
}
