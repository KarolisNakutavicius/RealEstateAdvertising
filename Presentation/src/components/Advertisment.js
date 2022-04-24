import React, { Component } from 'react'
import Row from 'react-bootstrap/Row';

export default class Advertisment extends Component {
    render() {
        return (
            <div className="advertisement" style={{width:"18rem"}}>
                <img className="card-img-top" src={`data:image/png;base64,${this.props.ad.image}`} alt="Image cannot be loaded" />
                <div className="card-body">
                    <h5 className="card-title">{this.props.ad.name}</h5>

                    <label>{`${this.props.ad.street} ${this.props.ad.number}, ${this.props.ad.city}`}</label>
                    <label>{this.props.ad.size} m²</label>
                    
                    <label className='mt-5' style={{position:"relative", right:"0px"}}><b>{this.props.ad.price}</b> €</label>

                    <p className="card-text mt-3">{this.props.ad.description}</p>
                </div>
            </div>
        )
    }
}
