import React, { Component } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';
import './styles/App.css'
import { Routes, Route, Link, Outlet } from "react-router-dom";

export default class App extends Component {
  render() {
    return (
      <div>
        <nav className="navbar navbar-expand navbar-dark bg-dark">
          <Link to={"/"} className="navbar-brand">
            Home
          </Link>
          <div className="navbar-nav mr-auto">
            <li className="nav-item">
              <Link to={"/home"} className="nav-link">
                Home
              </Link>
            </li>
          </div>
            <div className="navbar-nav ml-auto">
              <li className="nav-item">
                <Link to={"/login"} className="nav-link">
                  Login
                </Link>
              </li>
              <li className="nav-item">
                <Link to={"/register"} className="nav-link">
                  Sign Up
                </Link>
              </li>
            </div>
        </nav>

        <div className="container mt-3">
<Outlet/>
        </div>
      </div>
    )
  }
}
