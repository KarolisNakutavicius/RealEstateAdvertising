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
        if (value !== components['password'][0].value) {

          return (
            <div className="alert alert-danger" role="alert">
                Passwords don't match
            </div>
        );
        }
      };
}
