import React, { Component } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/index.css'
import '../styles/Layout.css'
import Topbar from './Topbar';
import Login from "./Login";
import Register from "./Register";
import Home from "./Home";
import { Routes, Route, Navigate } from "react-router-dom";
import AuthService from '../Services/AuthService';

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
    return (

      <div>
        <Topbar authenticationUpdated={this.handleAuthenticationUpdated} />
        <div className="container mt-3">
          <Routes>
            <Route index element={<Home />} />
            <Route path={"/home"} element={<Home />} />
            <Route path="*" element={<Navigate to="/home" replace />} />
            <Route path="/login" element={
              !this.state.currentUser
                ? (<Login authenticationUpdated={this.handleAuthenticationUpdated} />)
                : (<Navigate to="/home" replace />)} />
            <Route path="/register" element={
              !this.state.currentUser
                ? (<Register authenticationUpdated={this.handleAuthenticationUpdated} />)
                : (<Navigate to="/home" replace />)} />
          </Routes>
        </div>
      </div>
    )
  }
}
