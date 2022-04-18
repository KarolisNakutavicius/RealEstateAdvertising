import React, { Component } from 'react'
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import ValidationHelper from '../Helpers/ValidationHelper'
import AuthService from '../Services/AuthService'

export default class Login extends Component {
  constructor(props) {
    super(props);
    this.handleLogin = this.handleLogin.bind(this);
    this.onChangeEmail = this.onChangeEmail.bind(this);
    this.onChangePassword = this.onChangePassword.bind(this);
    this.state = {
      email: "",
      password: "",
      loading: false,
      message: "",
      successful: false
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
  handleLogin(e) {
    e.preventDefault();
    this.setState({
      message: "",
      loading: true
    });
    this.form.validateAll();
    if (this.checkBtn.context._errors.length === 0) {
      AuthService.login(this.state.email, this.state.password).then(
        response => {
          this.props.authenticationUpdated();
          this.setState({
            loading: false,
            message: response,
            successful: true
          });
        },
        error => {
          this.setState({
            loading: false,
            message: error
          });
        }
      );
    } else {
      this.setState({
        loading: false
      });
    }
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
            onSubmit={this.handleLogin}
            ref={c => {
              this.form = c;
            }}
          >
            <div className="form-group">
              <label htmlFor="Email">Email</label>
              <Input
                type="text"
                className="form-control"
                name="Email"
                value={this.state.email}
                onChange={this.onChangeEmail}
                validations={[ValidationHelper.required]}
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
                validations={[ValidationHelper.required]}
              />
            </div>
            <div className="form-group">
              <CheckButton
                className="btn btn-primary btn-block mt-3"
                ref={c => {this.checkBtn = c;}}>Log in</CheckButton>
            </div>
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