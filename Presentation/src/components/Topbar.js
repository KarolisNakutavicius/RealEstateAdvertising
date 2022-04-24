import React, { Component } from 'react'
import { Link } from "react-router-dom";
import AuthService from "../Services/AuthService"

export default class Topbar extends Component {

    constructor(props) {
        super(props);
        this.logOut = this.logOut.bind(this);
    }

    logOut() {
        AuthService.logout();
        this.props.authenticationUpdated();
    }

    render() {
        var currentUser = AuthService.getCurrentUser()
        return (
            <nav className="navbar navbar-expand navbar-dark bg-dark">
                <div className="navbar-nav mr-auto">
                    <li className="nav-item">
                        <Link to={"/home"} className="nav-link">
                            Home
                        </Link>
                    </li>
                    {currentUser && (
                        <>
                    <li className="nav-item">
                        <Link to={"/my-advertisements"} className="nav-link">
                            My advertisements
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to={"/add-advertisement"} className="nav-link">
                            Sell/Rent
                        </Link>
                    </li>
                    </>)}
                </div>

                <div style={{ position: "absolute", right: "40px" }}>
                    {currentUser ?
                        (<div className="navbar-nav ml-auto">
                            <li className="nav-item">
                                <a className="nav-link" role="button" onClick={this.logOut}>
                                    Sign out
                                </a>
                            </li>
                        </div>)
                        : (
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
                        )
                    }
                </div>
            </nav >
        )
    }
}
