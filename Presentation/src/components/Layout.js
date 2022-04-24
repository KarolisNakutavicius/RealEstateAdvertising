import React, { Component } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/index.css'
import '../styles/Layout.css'
import Topbar from './Topbar';
import Login from "./Login";
import Register from "./Register";
import Home from "./Home";
import AddAdvertisement from './AddAdvertisement';
import MyAdvertisments from './MyAdvertisments';
import { Routes, Route, Navigate } from "react-router-dom";
import AuthService from '../Services/AuthService';
import { ErrorBoundary } from 'react-error-boundary'

export default class Layout extends Component {

  constructor(props) {
    super(props)
    this.handleAuthenticationUpdated = this.handleAuthenticationUpdated.bind(this);
    this.state =
    {
      currentUser: undefined
    }
  }

  handleAuthenticationUpdated() {
    this.setState({
      currentUser: AuthService.getCurrentUser()
    });
  }

  componentDidMount() {
    this.handleAuthenticationUpdated();
  }


  render() {

    function ErrorFallback({ error, resetErrorBoundary }) {
      return (
        <div role="alert">
          <p>Something went wrong:</p>
          <pre>{error.message}</pre>
          <button onClick={resetErrorBoundary}>Try again</button>
        </div>
      )
    }
    return (

      <div>
        <Topbar authenticationUpdated={this.handleAuthenticationUpdated} />
        <div className="container mt-3">
          <Routes>
            <Route index element={<Home />} />
            <Route path={"/home"} element={<Home />} />
            <Route path={"/my-advertisements"} element={
              this.state.currentUser
                ? (
                  <ErrorBoundary FallbackComponent={ErrorFallback}>
                    <MyAdvertisments />
                  </ErrorBoundary>
                )
                : (<Navigate to="/home" replace />)} />
            <Route path={"/add-advertisement"} element={
              this.state.currentUser
                ? (
                  <ErrorBoundary FallbackComponent={ErrorFallback}>
                    <AddAdvertisement />
                  </ErrorBoundary>
                )
                : (<Navigate to="/home" replace />)} />
            <Route path="*" element={<Navigate to="/home" replace />} />
            <Route path="/login" element={
              !this.state.currentUser
                ? (
                  <ErrorBoundary FallbackComponent={ErrorFallback}>
                    <Login authenticationUpdated={this.handleAuthenticationUpdated} />
                  </ErrorBoundary>
                )
                : (<Navigate to="/home" replace />)} />
            <Route path="/register" element={
              !this.state.currentUser
                ? (
                  <ErrorBoundary FallbackComponent={ErrorFallback}>
                    <Register authenticationUpdated={this.handleAuthenticationUpdated} />
                  </ErrorBoundary>
                )
                : (<Navigate to="/home" replace />)} />
          </Routes>
        </div>
      </div>
    )
  }
}
