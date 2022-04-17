import React, { Component } from 'react'


export default class Login extends Component {
  render() {
    return (
        <div className='flexbox-container-center'>
        <div style={{textAlign: 'center'}} className="center-children-margin">
          <h4> Please login or register to continue </h4>
          <input type="text" placeholder="Email" className="center-horizontal" style={{marginTop: '30px'}}></input>
          <input placeholder="Password" type="password" className="center-horizontal"></input>
          <label style={{color: 'red', display: 'none'}}>Erorr</label>
                  <div>
                      <button type="button" className="btn btn-primary" style={{ marginRight: '20px' }} onClick={handleLogin}>Login</button>
            <button className="btn btn-secondary">Register</button>
          </div>
        </div>
      </div>
    )
  }
}

var neededData;

async function handleLogin(e) {

    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: 'React POST Request Example' })
    };
    
    await fetch('/api/User/login', requestOptions)
        .then(response => {
            return response.json();
        })
        .then(data => {
            neededData = data
        });

    console.log(neededData.title);
  } 