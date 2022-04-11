import React from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';

export default function App() {
  return (
    <>
      <div className='flexbox-container-center'>
        <div style={{textAlign: 'center'}} className="center-children-margin">
          <h4> Please login or register to continue </h4>
          <input type="text" placeholder="Email" className="center-horizontal" style={{marginTop: '30px'}}></input>
          <input placeholder="Password" type="password" className="center-horizontal"></input>
          <label style={{color: 'red', display: 'none'}}>Erorr</label>
          <div>
            <button type="button" className="btn btn-primary" style={{marginRight: '20px'}}>Login</button>
            <button className="btn btn-secondary">Register</button>
          </div>
        </div>
      </div>
    </>
  )
}

