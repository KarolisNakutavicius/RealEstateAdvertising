import React, { Component } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/index.css'
import '../styles/Layout.css'
import { Outlet } from "react-router-dom";
import Topbar from './Topbar';

export default class Layout extends Component {
  render() {
    return (
      <div>
        <Topbar />
        <div className="container mt-3">
          <Outlet />
        </div>
      </div>
    )
  }
}
