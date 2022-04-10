import React from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';

export default function App() {
  return (
    <>
      <div id="login-box">
        <input type="text" placeholder="Email"></input>
        <input placeholder="Password" type="password"></input>
        <button type="button" class="btn btn-primary">Login</button>
        <button class="btn btn-secondary">Register</button>
      </div>
    </>
  )
}

