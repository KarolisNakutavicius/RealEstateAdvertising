import React, { Component } from 'react'
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import ValidationHelper from '../Helpers/ValidationHelper'
import AuthService from '../Services/authService'

export default class Register extends Component {
  constructor(props) {
    super(props);

    // binding to context (need some more research here)
    // https://stackoverflow.com/questions/31045716/react-this-setstate-is-not-a-function
    this.handleRegister = this.handleRegister.bind(this);
    this.onChangeEmail = this.onChangeEmail.bind(this);
    this.onChangePassword = this.onChangePassword.bind(this);
    this.onChangeConfirmPassword = this.onChangeConfirmPassword.bind(this);
    this.state = {
      email: "",
      password: "",
      confirmPassword: "",
      successful: false,
      message: ""
    };
  }
  onChangeEmail(e) {
    this.setState({
      email: e.target.value
    });
  }
  onChangePassword(e) {
    this.setState({
      password: e.target.value
    });
  }
  onChangeConfirmPassword(e) {
    this.setState({
      confirmPassword: e.target.value
    });
  }
  handleRegister(e) {
    e.preventDefault();
    this.setState({
      message: "",
      successful: false
    });
    this.form.validateAll();
    if (this.checkBtn.context._errors.length !== 0) {
      return;
    }
    AuthService.register(
      this.state.email,
      this.state.password
    ).then(
      response => {
        this.setState({
          message: response,
          successful: true
        });
      },
      error => {
        this.setState({
          successful: false,
          message: error
        });
      }
    );
  }
  render() {
    return (
      <div className="col-md-12">
        <div className="card card-container">
          <img
            src="//ssl.gstatic.com/accounts/ui/avatar_2x.png"
            alt="profile-img"
            className="profile-img-card"
          />
          <Form
            onSubmit={this.handleRegister}
            ref={c => {
              this.form = c;
            }}
          >
            {!this.state.successful && (
              <div>
                <div className="form-group">
                  <label htmlFor="email">Email</label>
                  <Input
                    type="email"
                    className="form-control"
                    name="email"
                    value={this.state.email}
                    onChange={this.onChangeEmail}
                    validations={[ValidationHelper.required, ValidationHelper.correctEmail]}
                  />
                </div>
                <div className="form-group">
                  <label htmlFor="password">Password</label>
                  <Input
                    type="password"
                    className="form-control"
                    name="password"
                    value={this.state.password}
                    onChange={this.onChangePassword}
                    validations={[ValidationHelper.required, ValidationHelper.correctLengthPassword]}
                  />
                </div>
                <div className="form-group">
                  <label htmlFor="password">Confirm Password</label>
                  <Input
                    type="password"
                    className="form-control"
                    name="confirm"
                    value={this.state.confirmPassword}
                    onChange={this.onChangeConfirmPassword}
                    validations={[ValidationHelper.passwordMustMatch]}
                  />
                </div>
                <div className="form-group mt-3">
                  <CheckButton className="btn btn-primary btn-block"
                    ref={c => {
                      this.checkBtn = c;
                    }}
                  >Sign Up
                  </CheckButton>
                </div>
              </div>
            )}
            {this.state.message && (
              <div className="form-group mt-3">
                <div
                  className={
                    this.state.successful
                      ? "alert alert-success"
                      : "alert alert-danger"
                  }
                  role="alert"
                >
                  {this.state.message}
                </div>
              </div>
            )}

          </Form>
        </div>
      </div>
    );
  }
}